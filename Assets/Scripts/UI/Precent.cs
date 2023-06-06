using System.Collections;
using TMPro;
using UnityEngine;

public class Precent : MonoBehaviour
{
    [SerializeField] private TotalDirtCounter totalDirtCounter;
    [SerializeField] private TMP_Text timerText;

    private IEnumerator PrecentCalc()
    {
        while (!SettingsData.gameOnPause)
        {
            timerText.text = string.Format("{0.00%}", totalDirtCounter.GetDirtAmout());
            yield return null;
        }
    }

    void Start()
    {
        StartCoroutine(PrecentCalc());
    }
}
