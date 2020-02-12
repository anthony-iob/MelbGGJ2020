using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressYtoRegret : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       // GameManager.instance.MAX_BLOOD_LEVEL = 10000;
    }

    // Update is called once per frame
    void Update()
    {
        if (MusicManager.instance.loop2.isPlaying)
        {
            if (Input.GetButtonUp("AddSlime") && GameManager.instance.currentBloodLevel < 10000)
            {
                GameManager.instance.currentBloodLevel += 500;
               // Debug.Log(GameManager.instance.currentBloodLevel);
            }
        }
        if (Input.GetButtonUp("RemoveSlime") && GameManager.instance.currentBloodLevel < 10000) 
        {
            GameManager.instance.currentBloodLevel -= 500;
            Debug.Log(GameManager.instance.currentBloodLevel);
        }
    }
}
