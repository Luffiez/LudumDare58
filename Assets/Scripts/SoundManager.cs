using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    [SerializeField] AudioSource sfx;
    [SerializeField] AudioClip ghostHitClip;
    [SerializeField] AudioClip scoreClip;
    [SerializeField] AudioClip damageClip;
    [SerializeField] AudioClip dieClip;

    [Header("Fade Sfx")]
    [SerializeField] FadeOutSfx beam;
    [SerializeField] FadeOutSfx altar;

    public AudioClip GhostHitClip => ghostHitClip;
    public AudioClip ScoreClip => scoreClip;
    public AudioClip DamageClip => damageClip;
    public AudioClip DieClip => dieClip;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySfx(AudioClip clip, bool modifyPitch = false)
    {
        float pitch = sfx.pitch;
        if (modifyPitch)
        {
            float modifier = 0.5f;
            sfx.pitch = Random.Range(sfx.pitch - modifier, sfx.pitch + modifier);
        }
        sfx.PlayOneShot(clip);

        if (modifyPitch)
            sfx.pitch = pitch;
    }

    public void PlayBeamSfx() => beam.Play();
    public void StopBeamSfx() => beam.Stop();

    public void PlayAltarSfx() => altar.Play();
    public void StopAltarSfx() => altar.Stop();

    private void Update()
    {
        altar.Update();
        beam.Update();
    }
}
