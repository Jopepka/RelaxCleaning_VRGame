using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    public GameObject menuPage;
    public GameObject continueButton;

    void Start()
    {
        continueButton.SetActive(SettingsData.inGame? true : false);
    }

    public void LoadScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum); //при сливании приписать номера сцен в билде!!!
    }

    public void ContinueButton()
    {
        if (SettingsData.inGame)
            menuPage.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
