using UnityEngine;

public class Altar : MonoBehaviour
{
    [SerializeField] float interval;
    [SerializeField] float percentage;

    float timer = 0;
    bool playerInside = false;
    PlayerAttack playerAttack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerAttack == null)
                collision.gameObject.TryGetComponent(out playerAttack);
            playerInside = true;    
            // disable attack?
            // Play certain animation?
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInside = false;
            timer = 0;
            //enable attack?
        }
    }

    private void Update()
    {
        if (!playerInside)
            return;

        if (GameManager.Instance.PendingScore <= 0)
            return;

        if (!playerAttack || playerAttack.Attacking)
            return;

        if (timer >= interval)
        {
            float percAmount = Mathf.Clamp(GameManager.Instance.PendingScore * percentage, 1, GameManager.Instance.PendingScore);
            int amount = Mathf.RoundToInt(percAmount);
            GameManager.Instance.AddScore(amount);
            GameManager.Instance.DecreasePendingScore(amount);

            timer = 0;
        }
        else
            timer += Time.deltaTime;
    }

}
