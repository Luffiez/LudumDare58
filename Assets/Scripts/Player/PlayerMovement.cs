using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    float moveForce;
    [SerializeField]
    float maxVelocity;

    [SerializeField]
    PlayerInput playerInput;

    InputActionMap actionMap;

    InputAction movementAction;

    Vector2 movementInput = Vector2.zero;


    [SerializeField]
    Rigidbody2D rigidbody2D;


 


    void Awake()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        actionMap = playerInput.actions.FindActionMap("Player");
        movementAction = actionMap.FindAction("Move");
    }

    void FixedUpdate()
    {
        Vector2 movement = movementAction.ReadValue<Vector2>();
        Debug.Log(movement);
        rigidbody2D.AddForce(movement * moveForce);
    }



    void OnDestroy()
    {
        
    }
}
