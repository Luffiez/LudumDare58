using Assets.Scripts.Ghost;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    const string GHOST_TAG = "Ghost";

    [Header("Attack Settings")]
    [SerializeField] private float attackForce = 500f;
    [SerializeField] private float attackRate = 0.5f;
    [SerializeField] private int attackDamage = 1;

    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] SpriteRenderer WeaponSprite;
    [SerializeField] SpriteRenderer WeaponSpriteIdle;
    [SerializeField] float altarCheckDistance = 3f;

    [Header("References")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject suckHitboxParent;
    [SerializeField] private Collider2D suckHitbox;
    [SerializeField] private LayerMask ghostLayerMask;
    [SerializeField] private LayerMask obstacleLayerMask;
    [SerializeField] private GameObject fullUI;
    [SerializeField] private GameObject tooCloseUI;

    [SerializeField] private PlayerMovement playerMovement;

    private InputActionMap actionMap;
    private InputAction attackDirectionAction;

    private ContactFilter2D ghostFilter;
    private List<Collider2D> overlapedColliders = new();

    private Vector2 attackDirectionInput;
    private float attackTimer = 0f;

    private bool nearbyAltar = false;

    Altar altar;

    public bool Attacking => suckHitboxParent.gameObject.activeSelf;

    void Start()
    {
        actionMap = playerInput.actions.FindActionMap("Player");
        attackDirectionAction = actionMap.FindAction("AttackDirection");
        ghostFilter = new ContactFilter2D()
        {
            layerMask = ghostLayerMask,
            useTriggers = true,
            useLayerMask = true
        };
        altar = FindAnyObjectByType<Altar>();
    }

    private void Update()
    {
        attackDirectionInput = attackDirectionAction.ReadValue<Vector2>();
        nearbyAltar = Vector2.Distance(transform.position, altar.transform.position) < altarCheckDistance;

        bool previousState = suckHitboxParent.gameObject.activeSelf;
        bool nextStateActive = attackDirectionInput != Vector2.zero;
        fullUI.SetActive(false);
        tooCloseUI.SetActive(false);

        if (nextStateActive && nearbyAltar)
        {
            suckHitboxParent.gameObject.SetActive(false);
            tooCloseUI.SetActive(true);
            return;
        }

        if (nextStateActive && GameManager.Instance.IsAtMaxCapacity())
        {
            suckHitboxParent.gameObject.SetActive(false);
            fullUI.SetActive(true);
            return;
        }

        if (previousState != nextStateActive)
        {
            if (nextStateActive)
            {
                WeaponSpriteIdle.enabled = false;
                SoundManager.Instance.PlayBeamSfx();
            }
            else
            {
                WeaponSpriteIdle.enabled = true;
                SoundManager.Instance.StopBeamSfx();
            }
            suckHitboxParent.gameObject.SetActive(nextStateActive);
        }

        playerMovement.Attacking = attackDirectionInput != Vector2.zero;
        if (attackTimer < attackRate)
            attackTimer += Time.deltaTime;
    }


    void FixedUpdate()
    {
        if (GhostExtensions.GhostsBeingAttacked.Count > 0)
            GhostExtensions.GhostsBeingAttacked.Clear();

        if (!Attacking)
            return;

        SetHitboxRotation(attackDirectionInput, suckHitboxParent.transform.rotation);
        GetOverlappingGhosts();
        TryAttack();
    }

    private void GetOverlappingGhosts()
    {
        overlapedColliders.Clear();
        Physics2D.OverlapCollider(suckHitbox, ghostFilter, overlapedColliders);
    }

    private void TryAttack()
    {
        if (overlapedColliders.Count == 0)
            return;
        if (attackTimer < attackRate)
            return;

        attackTimer = 0;

        Vector2 playerPosition = transform.position;
        for (int i = overlapedColliders.Count - 1; i >= 0; i--)
        {
            Collider2D target = overlapedColliders[i];
            Vector2 targetPosition = target.transform.position;
            Vector2 targetPlayerDelta = playerPosition - targetPosition;
            RaycastHit2D hit = Physics2D.Raycast(playerPosition, targetPlayerDelta.normalized, targetPlayerDelta.magnitude, obstacleLayerMask);

            // If there's an obstacle between player and ghost, skip this ghost
            if (hit)
                continue;

            if (!target.gameObject.TryGetComponent(out GhostBehaviour ghostBehaviour))
                continue;

            Attack(ghostBehaviour);
        }
    }

    private void Attack(GhostBehaviour ghostBehaviour)
    {
        GhostExtensions.GhostsBeingAttacked.Add(ghostBehaviour);
        float distance = Vector2.Distance(transform.position, ghostBehaviour.transform.position);
        float lenght = suckHitbox.bounds.size.magnitude;
        float percentage = Mathf.Clamp01(1 - (distance / lenght));
        ghostBehaviour.TakeDamage(transform.position, attackForce, Mathf.RoundToInt(attackDamage * percentage));
    }

    // Directions for snapping the attack direction to 8 directions
    readonly Vector2[] directions = new Vector2[]
    {
        new(1, 0),    // East
        new(1, 1),    // NorthEast
        new(0, 1),    // North
        new(-1, 1),   // NorthWest
        new(-1, 0),   // West
        new(-1, -1),  // SouthWest
        new(0, -1),   // South
        new(1, -1)    // SouthEast
    };

    private void SetHitboxRotation(Vector2 attackDirection, Quaternion parentRotation)
    {
        if (attackDirection == Vector2.zero)
            return;

        // Find the closest direction
        Vector2 snapped = directions[0];
        float maxDot = Vector2.Dot(attackDirection.normalized, directions[0].normalized);
        for (int i = 1; i < directions.Length; i++)
        {
            float dot = Vector2.Dot(attackDirection.normalized, directions[i].normalized);
            if (dot > maxDot)
            {
                maxDot = dot;
                snapped = directions[i];
            }
        }

        // Calculate angle in degrees
        float angle = Mathf.Atan2(snapped.y, snapped.x) * Mathf.Rad2Deg + 90f;
        suckHitboxParent.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void OnDisable()
    {
        suckHitboxParent.gameObject.SetActive(false);
    }
}
