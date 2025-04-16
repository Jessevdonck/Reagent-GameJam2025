using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject settingsScreen;
    public GameObject menuScreen;

    public void PlayGame()
    {
        SceneManager.LoadScene("Jesse");
    }

    public void ShowSettings()
    {
        menuScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Game wordt gesloten!");
        Application.Quit();
    }

    public void CloseSettings()
    {
        settingsScreen.SetActive(false);
        menuScreen.SetActive(true);
    }
}