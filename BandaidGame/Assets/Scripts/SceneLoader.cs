using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    
    //public Button backButton;
    //public Button creditsButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();   
        }
    }

    public void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void LoadLevel1() { 
        SceneManager.LoadScene("Scenes/Level1"); 
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene("Scenes/Settings");
    }

    /*public void selectBack()
    {
        backButton.Select();
    }

    public void selectCredits()
    {
        creditsButton.Select();
    }*/

    public void QuitGame() {
        Application.Quit();
    }
}
