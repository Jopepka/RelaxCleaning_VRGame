using System.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;

    private float currTime;

    private IEnumerator StartTimer()
    {
        while (!SettingsData.gameOnPause)
        {
            currTime += Time.deltaTime;
            UpdateTimerText();
            yield return null;
        }
    }
    void Start()
    {
        StartCoroutine(StartTimer());
    }

    void UpdateTimerText()
    {
        float minutes = Mathf.FloorToInt(currTime / 60);
        float seconds = Mathf.FloorToInt(currTime % 60);
        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
