using System;
using UnityEngine;
using UnityEngine.Rendering;

public class Altar : MonoBehaviour
{
    [SerializeField] float interval;
    [SerializeField] float percentage;
    [SerializeField] int baseAmount;
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

        if (GameManager.Instance.PendingScore < 1 || !playerAttack || playerAttack.Attacking)
        {
            if (playing)
                StopPlaying();
            return;
        }

        if (!playing)
            StartPlaying();

        if (timer >= interval)
        {
            float percAmount = Mathf.Clamp(GameManager.Instance.GetMaxCapacity() * percentage, baseAmount, GameManager.Instance.PendingScore);
            int amount = Mathf.RoundToInt(percAmount);
            GameManager.Instance.AddScore(amount);
            SoundManager.Instance.PlaySfx(SoundManager.Instance.ScoreClip);

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
        SoundManager.Instance.PlayAltarSfx();
    }

    private void StopPlaying()
    {
        playing = false;
        anim.Play("Idle");
        ps.Stop();
        SoundManager.Instance.StopAltarSfx();
    }
}
