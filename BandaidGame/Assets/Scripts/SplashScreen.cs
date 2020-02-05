using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{

    private float delay = 0;
    public float loadTime = 4;
    public string sceneToLoad;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (delay < loadTime)
        {
            delay += Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad);
        }

    }
}
