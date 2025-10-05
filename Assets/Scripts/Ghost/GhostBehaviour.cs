using UnityEngine;

namespace Assets.Scripts.Ghost
{
    public class GhostBehaviour : MonoBehaviour
    {
        // These could be moved out into a ScriptableObject for better tuning and easier variation
        [Header("Ghost Settings")]
        [SerializeField] private float baseSpeed = 3.0f;
        [SerializeField] private float chaseSpeedModifier = 1.5f;
        [SerializeField] private float fleeSpeedModifier = 1.25f;
        [SerializeField] private float fleeBurstStrength = 50f;
        [SerializeField] private float playerDetectionRange = 3;
        [SerializeField] private float suctionModifier = 1f;
        [SerializeField] private float wallDistanceCheck = 1.5f;
        [SerializeField] private int maxHealth = 10;

        [Header("References")]
        [SerializeField] private ParticleSystem suckParticles;
        [SerializeField] private SpriteRenderer spriteRenderer;

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
        private int currentHealth;

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
            currentHealth = maxHealth;
        }

        internal void UpdateState(IGhostState state) => 
            State = state;

        internal void PlayAnimation(string stateName) => 
            animator.Play(stateName);

        internal void TakeDamage(Vector2 position, float force, int amount)
        {
            currentHealth -= amount;
            UpdateSpriteOpacity();
            if(currentHealth <= 0)
            {
                StateManager.Instance.UnregisterGhost(this);
                Destroy(gameObject);
                return;
            }

            Vector2 direction = (position - (Vector2)transform.position).normalized;
            rigidBody.AddForce(force * suctionModifier * Time.deltaTime * direction);
        }

        internal void MoveBurst(Vector2 direction)
        {
            rigidBody.AddForce(fleeBurstStrength * Time.deltaTime * direction, ForceMode2D.Impulse);
        }

        Color spriteColor = new Color(1f, 1f, 1f, 1f);
        private void UpdateSpriteOpacity()
        {
            float opacity = Mathf.Clamp01((float)currentHealth / maxHealth);
            spriteColor.a = opacity;
            spriteRenderer.color = spriteColor;
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
}
