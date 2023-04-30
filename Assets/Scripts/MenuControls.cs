using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuControls : MonoBehaviour
{
    public void PlayPressed()
    {
        //SceneManager.LoadScene("SampleScene");
        SceneManager.LoadScene("MainScene");
    }

    public void ExitPressed()
    {
        Debug.Log("Exit pressed!");
        Application.Quit();
    }
}