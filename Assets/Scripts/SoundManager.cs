using System.Drawing;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    [SerializeField] AudioSource sfx;
    [SerializeField] AudioSource beamSfx;

    [SerializeField] AudioClip ghostHitClip;

    public AudioClip GhostHitClip => ghostHitClip;

    bool fadeOutBeam = false;
    float beamStartVolume;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        beamStartVolume = beamSfx.volume;
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

    public void PlayBeamSfx()
    {
        beamSfx.volume = beamStartVolume;
        beamSfx.Play();
        fadeOutBeam = false;
    }

    public void StopBeamSfx() =>
        fadeOutBeam = true;

    private void Update()
    {
        if (fadeOutBeam)
        {
            if (beamSfx.volume == 0)
            {
                fadeOutBeam = false;
                beamSfx.Stop();
            }
            else
            {
                beamSfx.volume -= Time.deltaTime;
            }
        }
    }

}
