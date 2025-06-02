using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player speed")]
    public float moveSpeed = 5f;

    private Transform cameraTransform; // arrástrala en el inspector

    private bool canMove = true;
    private CharacterController controller;
    private PlayerInput playerInput;
    private InputAction moveAction;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void OnEnable()
    {
        moveAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
    }

    public void HabilitateMov()
    {
        canMove = !canMove;
    }
    void Update()
    {
        if (!canMove) return;
        Vector2 input = moveAction.ReadValue<Vector2>();

        if (input.sqrMagnitude < 0.01f)
            return;


        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 move = (camRight * input.x + camForward * input.y);

        if (move.magnitude > 1f)
            move.Normalize();

        controller.Move(move * moveSpeed * Time.deltaTime);
    }
}
