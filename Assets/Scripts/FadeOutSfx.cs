using UnityEngine;

[System.Serializable]
public class FadeOutSfx
{
    [SerializeField] AudioSource sfx;
    bool fadingOut = false;
    float startVolume = -1;

    public void Play()
    {
        if (startVolume == -1)
            startVolume = sfx.volume;

        sfx.volume = startVolume;
        fadingOut = false;
        sfx.Play();
    }

    public void Stop()
    {
        fadingOut = true;
    }

    public void Update()
    {
        if (!fadingOut)
            return;
        
        if (sfx.volume > 0)
        {
            sfx.volume -= Time.deltaTime;
            return;
        }
        fadingOut = false;
        sfx.Stop();
    }
}
