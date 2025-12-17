using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private InputActionAsset inputActions;
    private InputAction moveAction;



    void OnEnable()
    {
        moveAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
    }


    void Awake()
    {
        moveAction = inputActions.FindActionMap("Player").FindAction("Move");
    }

    void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        if (input.sqrMagnitude > 0f)
        {
            Vector3 move = Vector3.right * input.x + Vector3.forward * input.y;
            transform.position += move * moveSpeed * Time.deltaTime;
        }
    }
}