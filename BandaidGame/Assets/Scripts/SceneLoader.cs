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
        SceneManager.LoadScene(SceneManager.GetSceneByName("MainMenu").buildIndex);
    }

    public void LoadLevel1() { 
        SceneManager.LoadScene(SceneManager.GetSceneByName("Level1").buildIndex); 
    } 

    public void QuitGame() {
        Application.Quit();
    }
}
