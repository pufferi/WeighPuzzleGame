using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    private InputAction moveAction;

    void Awake()
    {
        // Create a 2DVector composite for WASD movement using the new Input System
        moveAction = new InputAction(name: "Move", type: InputActionType.Value, expectedControlType: "Vector2");
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
    }

    void OnEnable()
    {
        moveAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        if (input.sqrMagnitude > 0f)
        {
            // Move along camera's XZ plane based on current facing
            Vector3 forward = transform.forward;
            forward.y = 0f;
            forward.Normalize();

            Vector3 right = transform.right;
            right.y = 0f;
            right.Normalize();

            Vector3 move = right * input.x + forward * input.y;
            transform.position += move * moveSpeed * Time.deltaTime;
        }
    }
}