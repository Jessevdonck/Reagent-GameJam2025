using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject settingsScreen;
    public GameObject menuScreen;
    public GameObject levelSelectionScreen;

    public void Play()
    {
        menuScreen.SetActive(false);
        levelSelectionScreen.SetActive(true);
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

    public void StartGame()
    {
        SceneManager.LoadScene("Jesse");
    }

    public void Casino()
    {
        SceneManager.LoadScene("CasinoScene");
    }

    public void CloseSettings()
    {
        settingsScreen.SetActive(false);
        menuScreen.SetActive(true);
    }
}