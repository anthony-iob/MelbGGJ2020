using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;

public class InputControlManager : MonoBehaviour
{
    public Button defaultButton;
    public GameObject defaultCanvas;
    public Button alternateDefaultButton;
    public GameObject alternateCanvas;
    public Button pauseDefaultButton;
    public GameObject pauseCanvas;
    public Button gameOverDefaultButton;
    public GameObject gameOverCanvas;
    public GameObject myEventSystem;
    private bool reconnected;
    private bool defaultSelect;
    private bool alternateSelect;
    private bool pauseSelect;
    private bool gameOverSelect;

    // Start is called before the first frame update
    void Start()
    {
        reconnected = false;
        defaultSelect = false;
        alternateSelect = false;
        pauseSelect = false;
        gameOverSelect = false;
    }

    void Update()
    {
        //This code checks if a joystick is connected or not through its name every update. When a joystick is disconnected, the EventManager will deselect all buttons in favour of mouse control.
        //Alternatively when it does detect one it will set an active button depending on which canvas is currently active. This is currently best used with only two canvases.

        string[] inputs = Input.GetJoystickNames();

		if (inputs.Length > 0)
		{
			if (inputs[0] == "")
			{
				GameObject myEventSystem = GameObject.Find("EventSystem");
				myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
				reconnected = false;
                defaultSelect = false;
                alternateSelect = false;
                pauseSelect = false;
                gameOverSelect = false;
			}
			else
			{
                if (defaultCanvas && defaultCanvas.activeInHierarchy)
                {
                    if (!defaultSelect)
                    {
                        defaultButton.Select();
                        defaultSelect = true;
                        alternateSelect = false;
                        pauseSelect = false;
                        gameOverSelect = false;
                    }
                }
                else if (alternateCanvas && alternateCanvas.activeInHierarchy)
                {
                    if (!alternateSelect)
                    {
                        alternateDefaultButton.Select();
                        defaultSelect = false;
                        alternateSelect = true;
                        pauseSelect = false;
                        gameOverSelect = false;
                    }
                }
                else if (pauseCanvas && pauseCanvas.activeInHierarchy)
                {
                    if (!pauseSelect)
                    {
                        pauseDefaultButton.Select();
                        defaultSelect = false;
                        alternateSelect = false;
                        pauseSelect = true;
                        gameOverSelect = false;
                    }
                }
                else if (gameOverCanvas && gameOverCanvas.activeInHierarchy)
                {
                    if (!gameOverSelect)
                    {
                        gameOverDefaultButton.Select();
                        defaultSelect = false;
                        alternateSelect = false;
                        pauseSelect = false;
                        gameOverSelect = true;
                    }
                }
            }
		}
        else
        {
            GameObject myEventSystem = GameObject.Find("EventSystem");
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
            reconnected = false;
            defaultSelect = false;
            alternateSelect = false;
            pauseSelect = false;
            gameOverSelect = false;
        }
    }
}
