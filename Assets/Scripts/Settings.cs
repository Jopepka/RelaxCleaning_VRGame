using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider volume;
    public Dropdown quality;

    public AudioSource stepVol;
    public AudioSource runVol;
    public AudioSource landVol;
    public AudioSource jumpVol;
    public AudioSource crouchVol;
    public AudioSource crouchSVol;
    public AudioSource crouchEVol;

    public void AudioVolume()
    {
        stepVol.volume = volume.value;
        runVol.volume = volume.value;
        landVol.volume = volume.value;
        jumpVol.volume = volume.value;
        crouchVol.volume = volume.value * 0.3f;
        crouchSVol.volume = volume.value;
        crouchEVol.volume = volume.value;
    }
    public void Quality()
    {
        QualitySettings.SetQualityLevel(5 - quality.value);
    }
}
