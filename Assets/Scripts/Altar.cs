using System;
using UnityEngine;
using UnityEngine.Rendering;

public class Altar : MonoBehaviour
{
    [SerializeField] float interval;
    [SerializeField] float percentage;
    [SerializeField] ParticleSystem ps;

    float timer = 0;
    bool playerInside = false;
    PlayerAttack playerAttack;
    Animator anim;

    bool playing = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerAttack == null)
                collision.gameObject.TryGetComponent(out playerAttack);
            playerInside = true;    
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInside = false;
            timer = 0;

            if (playing)
                StopPlaying();
        }
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!playerInside)
            return;

        if (GameManager.Instance.PendingScore <= 0 || !playerAttack || playerAttack.Attacking)
        {
            if (playing)
                StopPlaying();

            return;
        }

        if (!playing)
            StartPlaying();

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

    private void StartPlaying()
    {
        playing = true;
        anim.Play("Active");
        ps.Play();
    }

    private void StopPlaying()
    {
        playing = false;
        anim.Play("Idle");
        ps.Stop();
    }
}
