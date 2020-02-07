using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void CursorShow()
    {
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}
}
