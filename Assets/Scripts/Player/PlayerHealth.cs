using UnityEngine;

public class PlayerHealth : MonoBehaviour
{


    const string GHOST_TAG = "Ghost";

    [SerializeField] int StartHealth;

    [SerializeField] PlayerMovement PlayerMovement;

    [SerializeField] PlayerAttack PlayerAttack;

    [SerializeField] SpriteRenderer PlayerSprite;
    [SerializeField] SpriteColorChanger PlayerColorChanger;

    [SerializeField] float HitInvincibletime;
    float HitInvincibletimer;

    bool Dead;

    float OpacityStartValue;

    int CurrentHealth;

    void Start()
    {
        CurrentHealth = StartHealth;
        OpacityStartValue = PlayerSprite.color.a;
        HitInvincibletimer = HitInvincibletime;
        PlayerColorChanger.enabled = false; 
    }



    void Update()
    {
        if (Dead  || HitInvincibletimer >= HitInvincibletime) return;

        HitInvincibletimer += Time.deltaTime;
        if (HitInvincibletimer < HitInvincibletime)
        { 
            PlayerColorChanger.enabled = false;
        }



    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GHOST_TAG))
        {
            Damage();

        }
    }


    public void Damage()
    {
        if (CurrentHealth < 0)
            return;

        CurrentHealth--;

        if (CurrentHealth <= 0)
        {
            PlayerMovement.enabled = false;
            PlayerAttack.enabled = false;
            PlayerSprite.flipY = true;
            PlayerColorChanger.enabled = false;
            //call 
        }
        else
        {
            HitInvincibletimer = 0;
            PlayerColorChanger.enabled = true;
        }
    }
}
