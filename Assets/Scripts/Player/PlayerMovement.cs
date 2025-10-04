using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveForce;
    [SerializeField] private float maxVelocity;
    
    private PlayerInput playerInput;
    private Rigidbody2D rb;

    private InputActionMap actionMap;
    private InputAction movementAction;

    private Vector2 movementInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        actionMap = playerInput.actions.FindActionMap("Player");
        movementAction = actionMap.FindAction("Move");
    }

    private void Update()
    {
        movementInput = movementAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            movementInput = movementInput.normalized; 
            rb.AddForce(movementInput * moveForce);
        }
    }
}
