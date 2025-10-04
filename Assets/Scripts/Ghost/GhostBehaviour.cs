using Assets.Scripts.Ghost;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{
    // These could be moved out into a ScriptableObject for better tuning and easier variation
    [Header("Ghost Settings")]
    [SerializeField] private float baseSpeed = 3.0f;
    [SerializeField] private float chaseSpeedModifier = 1.5f;
    [SerializeField] private float fleeSpeedModifier = 1.25f;
    [SerializeField] private float playerDetectionRange = 3;

    public Vector2 TargetPosition { get; internal set; }
    public Transform Target { get; private set; }
    
    
    public IGhostState State { get; private set; } = GhostStateManager.States[0];
    public Rigidbody2D RigidBody => rigidBody;
    public float Speed => baseSpeed;
    public float ChaseSpeed => baseSpeed * chaseSpeedModifier;
    public float FleeSpeed => baseSpeed * fleeSpeedModifier;
    public float detectionRange => playerDetectionRange;

    private Rigidbody2D rigidBody;


    internal void UpdateState(IGhostState newState)
    {
        State = newState;
    }

    private void OnEnable()
    {
        StateManager.Instance.RegisterGhost(this);
    }

    private void OnDisable()
    {
        StateManager.Instance.UnregisterGhost(this);
    }

    void Awake()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
