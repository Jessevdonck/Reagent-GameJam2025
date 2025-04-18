using UnityEngine;
using UnityEngine.SceneManagement; 

public class UiLevelManager : MonoBehaviour
{
    public GameObject uiMenuPanel;
    
    public void OpenMenu()
    {
        uiMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("StartScreen");
    }
    
    public void CloseMenu()
    {
        uiMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}