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
    [SerializeField] private float suctionModifier = 1f;
    [SerializeField] private float wallDistanceCheck = 1.5f;
    [SerializeField] private ParticleSystem suckParticles;

    public Vector2 TargetPosition { get; internal set; }
    public Transform Target { get; private set; }
    
    
    public IGhostState State { get; private set; } = GhostStateManager.States[0];
    public Rigidbody2D RigidBody => rigidBody;
    public float Speed => baseSpeed;
    public float ChaseSpeed => baseSpeed * chaseSpeedModifier;
    public float FleeSpeed => baseSpeed * fleeSpeedModifier;
    public float detectionRange => playerDetectionRange;
    public float WallDistanceCheck => wallDistanceCheck;

    private Rigidbody2D rigidBody;
    private Animator animator;
 
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
        animator = GetComponent<Animator>();
    }

    internal void UpdateState(IGhostState state) => 
        State = state;

    internal void PlayAnimation(string stateName) => 
        animator.Play(stateName);

    internal void GetSuckedTowards(Vector2 position, float force)
    {
        Vector2 direction = (position - (Vector2)transform.position).normalized;
        rigidBody.AddForce(force * suctionModifier * Time.deltaTime * direction);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    internal void PlaySuctionParticles()
    {
        if (!suckParticles.isPlaying)
            suckParticles.Play();
    }

    internal void StopSuctionParticles()
    {
        if (suckParticles.isPlaying)
            suckParticles.Stop();
    }
}
