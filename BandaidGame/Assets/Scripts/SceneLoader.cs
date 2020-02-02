using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
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

    public void QuitGame() {
        Application.Quit();
    }
}
