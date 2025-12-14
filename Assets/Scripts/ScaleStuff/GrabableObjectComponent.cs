using UnityEngine;
using UnityEngine.InputSystem;

public class GrabableObjectComponent : MonoBehaviour
{
    private InputAction grabAction;

    private bool isGrabbed = false;
    private Camera mainCamera;
    private Rigidbody rb;
    private bool wasGravityOn;
    private bool wasKinematic;
    private CollisionDetectionMode previousCollisionDetection;
    private float grabDistance;
    private Vector3 currentTargetPosition;


    private float scrollSensitivity = 0.005f; // How much distance changes per scroll unit
  
    private float minGrabDistance = 3f;

    private float maxGrabDistance = 20f;

    // Force-follow configuration
    [SerializeField]
    private float maxSpeed = 10f;           // cap desired speed
    [SerializeField]
    private float maxAcceleration = 80f;    // cap applied acceleration (mass-independent)
    [SerializeField]
    private float stopDistance = 0.01f;     // consider reached when within this distance

    void Awake()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        grabAction = new InputAction(binding: "<Mouse>/leftButton");
        grabAction.started += _ => Grab();
        grabAction.canceled += _ => Release();
        currentTargetPosition = transform.position;
    }

    void OnEnable()
    {
        grabAction.Enable();
    }

    void OnDisable()
    {
        grabAction.Disable();
    }

    void Grab()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject == gameObject)
        {
            isGrabbed = true;
            if (rb != null)
            {
                wasGravityOn = rb.useGravity;
                wasKinematic = rb.isKinematic;
                previousCollisionDetection = rb.collisionDetectionMode;

                rb.useGravity = false;
                rb.isKinematic = false; // use forces, so must be non-kinematic
                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; // reduce tunneling
            }
            grabDistance = Vector3.Distance(mainCamera.transform.position, transform.position);
            currentTargetPosition = transform.position;
            Debug.Log("Object grabbed!");
        }
    }

    void Release()
    {
        if (isGrabbed)
        {
            isGrabbed = false;
            if (rb != null)
            {
                rb.useGravity = wasGravityOn;
                rb.isKinematic = wasKinematic;
                rb.collisionDetectionMode = previousCollisionDetection;
            }
            Debug.Log("Object released!");
        }
    }

    void Update()
    {
        if (isGrabbed)
        {
            // Adjust grab distance with scroll wheel while grabbed
            Vector2 scroll = Mouse.current != null ? Mouse.current.scroll.ReadValue() : Vector2.zero;
            if (scroll.y != 0f)
            {
                grabDistance = Mathf.Clamp(grabDistance + scroll.y * scrollSensitivity, minGrabDistance, maxGrabDistance);
            }

            // Update target position along camera ray
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            currentTargetPosition = ray.GetPoint(grabDistance);

            // If no rigidbody (fallback), move transform directly
            if (rb == null)
            {
                transform.position = currentTargetPosition;
            }
        }
    }

    void FixedUpdate()
    {
        if (isGrabbed && rb != null)
        {
            Vector3 toTarget = currentTargetPosition - rb.position;
            float distance = toTarget.magnitude;

            if (distance < stopDistance)
            {
                // Close enough: gently slow down
                rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 0.5f);
                return;
            }

            // Desired velocity towards target (cap by maxSpeed and by how far the target is within this physics step)
            Vector3 desiredVel = toTarget.normalized * Mathf.Min(maxSpeed, distance / Time.fixedDeltaTime);

            // Acceleration needed to reach desired velocity within this step
            Vector3 velError = desiredVel - rb.velocity;
            Vector3 accel = Vector3.ClampMagnitude(velError / Time.fixedDeltaTime, maxAcceleration);

            // Apply mass-independent acceleration so tuning isn't mass-sensitive
            rb.AddForce(accel, ForceMode.Acceleration);
        }
    }
}