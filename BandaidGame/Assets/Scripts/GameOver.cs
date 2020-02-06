using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void FixedUpdate()
    {
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}
}
