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
        [SerializeField] private float playerDetectionRange = 3;
        [SerializeField] private float suctionModifier = 1f;
        [SerializeField] private float wallDistanceCheck = 1.5f;
        [SerializeField] private int maxHealth = 10;
        [SerializeField] private int scoreValue;
        [SerializeField] private LayerMask boundsLayer;
        [SerializeField] private bool canChase = true;

        [SerializeField]
        float GhostAttackTime = 1;
        float GhostAttackTimer = 0;

        [Header("References")]
        [SerializeField] private ParticleSystem suckParticles;
        [SerializeField] private SpriteRenderer spriteRenderer;

        public Vector2 TargetPosition { get; internal set; }
        public Transform Target { get; private set; }
        public bool CanChase => canChase;

        public IGhostState State { get; private set; } = GhostStateManager.States[0];
        public Rigidbody2D RigidBody => rigidBody;
        public LayerMask BoundsLayer => boundsLayer;
        public float Speed => GetModifiedBaseSpeed();

        private float GetModifiedBaseSpeed() =>
            baseSpeed * GetSpeedPercentage();

        public float ChaseSpeed => Speed * chaseSpeedModifier;
        public float FleeSpeed => Speed * -fleeSpeedModifier;
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

        internal void UpdateState(IGhostState state)
        {
            State = state;
            GhostAttackTimer += Time.deltaTime;

        }

        internal void PlayAnimation(string stateName) =>
            animator.Play(stateName);

        internal void TakeDamage(Vector2 position, float force, int amount)
        {
            if (amount > currentHealth)
                amount = currentHealth;

            currentHealth -= amount;

            GameManager.Instance.AddPendingScore(scoreValue * ((float)amount / maxHealth));
            float percentage = GetHealthPercentage();
            UpdateSpriteOpacity(percentage);
            UpdateSize(percentage);
            if (currentHealth <= 0)
            {
                this.RunAt(new GhostDie());
                return;
            }

            Vector2 direction = (position - (Vector2)transform.position).normalized;
            rigidBody.AddForce(force * suctionModifier * Time.deltaTime * direction);
        }

        Color spriteColor = new Color(1f, 1f, 1f, 1f);
        private void UpdateSpriteOpacity(float percentage)
        {
            spriteColor.a = Mathf.Lerp(0.2f, 1f, percentage);
            spriteRenderer.color = spriteColor;
        }

        private void UpdateSize(float percentage)
        {
            float size = Mathf.Lerp(0.6f, 1f, percentage);
            transform.localScale = Vector3.one * size;
        }

        private float GetSpeedPercentage()
        {
            float percentage = GetHealthPercentage();
            return Mathf.Lerp(0.2f, 1f, percentage);
        }

        float GetHealthPercentage() =>
             Mathf.Clamp01((float)currentHealth / maxHealth);

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

        PlayerHealth health; // Cache it for each ghost
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                GhostAttackTimer = 0;
                if (health || other.TryGetComponent(out health))
                    health.TakeDamage();
            }
        }

        void OnTriggerStay2D(Collider2D collision)
        {

            if (GhostAttackTimer > GhostAttackTime)
            {

                if (collision.gameObject.CompareTag("Player"))
                {
                    GhostAttackTimer = 0;
                    if (health || collision.TryGetComponent(out health))
                        health.TakeDamage();
                }
            }

        }


    }
}
