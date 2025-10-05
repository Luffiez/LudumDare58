using System.Drawing;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    [SerializeField] AudioSource sfx;
    [SerializeField] AudioSource bgm;

    [SerializeField] AudioClip ghostHitClip;

    public AudioClip GhostHitClip => ghostHitClip;

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
}
