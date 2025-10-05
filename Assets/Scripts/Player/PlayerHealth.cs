using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] PlayerMovement PlayerMovement;
    [SerializeField] PlayerAttack PlayerAttack;
    [SerializeField] SpriteRenderer PlayerSprite;
    [SerializeField] SpriteColorChanger PlayerColorChanger;

    [SerializeField] float HitInvincibletime;
    float HitInvincibletimer;
    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    bool isDead;
    int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
        HitInvincibletimer = HitInvincibletime;
        PlayerColorChanger.enabled = false; 
    }

    void Update()
    {
        if (isDead  || HitInvincibletimer >= HitInvincibletime) return;

        HitInvincibletimer += Time.deltaTime;
        if (HitInvincibletimer < HitInvincibletime)
        { 
            PlayerColorChanger.enabled = false;
        }
    }

    public void TakeDamage(int damage = 1)
    {
        if (currentHealth <= 0)
            return;

        currentHealth-= damage;

        if (currentHealth <= 0)
        {
            PlayerMovement.enabled = false;
            PlayerAttack.enabled = false;
            PlayerSprite.flipY = true;
            PlayerColorChanger.enabled = false;
            //call 
            // more death effects?
            SoundManager.Instance.PlaySfx(SoundManager.Instance.DieClip);
            Invoke("Restart", 3f);
        }
        else
        {
            SoundManager.Instance.PlaySfx(SoundManager.Instance.DamageClip);
            HitInvincibletimer = 0;
            PlayerColorChanger.enabled = true;
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
