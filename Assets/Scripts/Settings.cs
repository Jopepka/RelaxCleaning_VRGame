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

    public void FullScreenToggle()
    {
        isFullScreen = !isFullScreen;
        Data.fullscreen = isFullScreen;
        Screen.fullScreen = isFullScreen;
    }
    public void AudioVolume()
    {
        Data.volume = volume.value;
    }
    public void Quality()
    {
        QualitySettings.SetQualityLevel(quality.value);
        Data.quality = quality.value;
    }
}
