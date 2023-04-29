using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    bool isFullScreen = false;
    public Slider volume;
    public Dropdown quality;
    public AudioMixer am;

    public void FullScreenToggle()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }
    public void AudioVolume()
    {
        am.SetFloat("masterVolume", volume.value);
    }
    public void Quality()
    {
        QualitySettings.SetQualityLevel(quality.value);
    }
}
