using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class PlayerAttack : MonoBehaviour
{


     [SerializeField]
    PlayerInput playerInput;

    InputActionMap actionMap;

    InputAction attackDirectionAction;


    [SerializeField]
    GameObject shootHitboxParent;

    [SerializeField]
    Collider2D shootHitbox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        actionMap = playerInput.actions.FindActionMap("Player");
        attackDirectionAction = actionMap.FindAction("AttackDirection");
    }


    void FixedUpdate()
    {
        Vector2 attackDirection = attackDirectionAction.ReadValue<Vector2>();
        if (attackDirection != Vector2.zero)
        {
            Debug.Log(attackDirection);

            Quaternion parentRotation = shootHitboxParent.transform.rotation;

            if (attackDirection.x > 0.5f && attackDirection.y > 0.5f)
            { 
                shootHitboxParent.transform.rotation = quaternion.Euler(new float3(parentRotation.x, parentRotation.y, 135.0f * Mathf.Deg2Rad));
            }
            else   if (attackDirection.x > 0.5f && attackDirection.y < -0.5f)
            { 
                shootHitboxParent.transform.rotation = quaternion.Euler(new float3(parentRotation.x, parentRotation.y, 45.0f * Mathf.Deg2Rad));
            }
            else   if (attackDirection.x < -0.5f && attackDirection.y > 0.5f)
            { 
                shootHitboxParent.transform.rotation = quaternion.Euler(new float3(parentRotation.x, parentRotation.y, 225.0f * Mathf.Deg2Rad));
            }
            else   if (attackDirection.x < -0.5f && attackDirection.y < -0.5f)
            { 
                shootHitboxParent.transform.rotation = quaternion.Euler(new float3(parentRotation.x, parentRotation.y, 315.0f * Mathf.Deg2Rad));
            }
            else if (attackDirection.x > 0.5f)
            {
                shootHitboxParent.transform.rotation = quaternion.Euler(new float3(parentRotation.x, parentRotation.y, 90.0f * Mathf.Deg2Rad));
            }
            else if (attackDirection.x < -0.5f)
            {
                shootHitboxParent.transform.rotation = quaternion.Euler(new float3(parentRotation.x, parentRotation.y, 270.0f * Mathf.Deg2Rad));
            }
            else if (attackDirection.y > 0.5f)
            {
                shootHitboxParent.transform.rotation = quaternion.Euler(new float3(parentRotation.x, parentRotation.y, 180.0f * Mathf.Deg2Rad));
            }
            else if (attackDirection.y < -0.5f)
            {
                shootHitboxParent.transform.rotation = quaternion.Euler(new float3(parentRotation.x, parentRotation.y, 0.0f * Mathf.Deg2Rad));
            }
            

        }
        // 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
