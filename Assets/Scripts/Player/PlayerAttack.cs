using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class PlayerAttack : MonoBehaviour
{

    const string ghostTag = "Ghost";

    [SerializeField]
    PlayerInput playerInput;

    InputActionMap actionMap;

    InputAction attackDirectionAction;


    [SerializeField]
    GameObject suckHitboxParent;

    [SerializeField]
    Collider2D suckHitbox;

    [SerializeField]
    LayerMask ghostLayerMask;

    [SerializeField]
    LayerMask ghostAndWallLayerMask;

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

            Quaternion parentRotation = suckHitboxParent.transform.rotation;

            #region  set attack hitbox rotation

            if (attackDirection.x > 0.5f && attackDirection.y > 0.5f)
            {
                suckHitboxParent.transform.rotation = quaternion.Euler(new float3(parentRotation.x, parentRotation.y, 135.0f * Mathf.Deg2Rad));
            }
            else if (attackDirection.x > 0.5f && attackDirection.y < -0.5f)
            {
                suckHitboxParent.transform.rotation = quaternion.Euler(new float3(parentRotation.x, parentRotation.y, 45.0f * Mathf.Deg2Rad));
            }
            else if (attackDirection.x < -0.5f && attackDirection.y > 0.5f)
            {
                suckHitboxParent.transform.rotation = quaternion.Euler(new float3(parentRotation.x, parentRotation.y, 225.0f * Mathf.Deg2Rad));
            }
            else if (attackDirection.x < -0.5f && attackDirection.y < -0.5f)
            {
                suckHitboxParent.transform.rotation = quaternion.Euler(new float3(parentRotation.x, parentRotation.y, 315.0f * Mathf.Deg2Rad));
            }
            else if (attackDirection.x > 0.5f)
            {
                suckHitboxParent.transform.rotation = quaternion.Euler(new float3(parentRotation.x, parentRotation.y, 90.0f * Mathf.Deg2Rad));
            }
            else if (attackDirection.x < -0.5f)
            {
                suckHitboxParent.transform.rotation = quaternion.Euler(new float3(parentRotation.x, parentRotation.y, 270.0f * Mathf.Deg2Rad));
            }
            else if (attackDirection.y > 0.5f)
            {
                suckHitboxParent.transform.rotation = quaternion.Euler(new float3(parentRotation.x, parentRotation.y, 180.0f * Mathf.Deg2Rad));
            }
            else if (attackDirection.y < -0.5f)
            {
                suckHitboxParent.transform.rotation = quaternion.Euler(new float3(parentRotation.x, parentRotation.y, 0.0f * Mathf.Deg2Rad));
            }

            #endregion
            List<Collider2D> overlapedColliders = new List<Collider2D>();

            ContactFilter2D ghostFilter = new ContactFilter2D();
            ghostFilter.layerMask = ghostLayerMask;
            int hits = Physics2D.OverlapCollider(suckHitbox, ghostFilter,overlapedColliders);
            if (hits > 0)
            {
                ContactFilter2D ghostAndWallFilter = new ContactFilter2D();
                ghostAndWallFilter.layerMask = ghostAndWallLayerMask;
                Vector2 playerPosition = transform.position;
                foreach (Collider2D overlapedCollider in overlapedColliders)
                {
                    Vector2 enemyPosition = overlapedCollider.transform.position;
                    Vector2 enemyPlayerDelta = playerPosition - enemyPosition;
                    RaycastHit2D raycastHit2D = Physics2D.Raycast(playerPosition, enemyPlayerDelta.normalized, enemyPlayerDelta.magnitude);
                    if (raycastHit2D)
                    {
                        bool isGhost = raycastHit2D.collider.gameObject.CompareTag(ghostTag);
                        if (isGhost)
                        { 
                            //attack here
                        }
                    }

                }
            }
        }
        // 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
