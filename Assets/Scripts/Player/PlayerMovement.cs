using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    PlayerInput playerInput;

    InputActionMap actionMap;

    InputAction movementAction;

    Vector2 movementInput = Vector2.zero;


 


    void Awake()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        actionMap = playerInput.actions.FindActionMap("Player");
        movementAction = actionMap.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = movementAction.ReadValue<Vector2>();
        Debug.Log(movement);
        GetComponent<Rigidbody2D>().AddForce(movement * 10.0f);
        
    }


    void OnDestroy()
    {
        
    }
}
