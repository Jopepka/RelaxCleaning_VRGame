using UnityEngine;

public class SetSettings : MonoBehaviour
{
    public AudioSource sceneVol;

    public void SetVolume()
    {
        sceneVol.volume = SettingsData.volume;
    }

    public void SetQuality()
    {
        QualitySettings.SetQualityLevel(SettingsData.qualityLevel);
    }
}
