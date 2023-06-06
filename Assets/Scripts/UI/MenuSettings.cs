using UnityEngine.UI;
using UnityEngine;

public class MenuSettings : MonoBehaviour
{
    public Slider volume;
    public Dropdown quality;
    public Toggle speedrun;

    private void Start()
    {
        
    }

    public void Volume()
    {
        SettingsData.volume = volume.value;
    }

    public void Quality()
    {
        SettingsData.qualityLevel = 5 - quality.value;
    }

    public void Speedrun()
    {
        SettingsData.withTimer = speedrun.enabled;
    }
}