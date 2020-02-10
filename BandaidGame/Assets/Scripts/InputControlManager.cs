using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;

public class InputControlManager : MonoBehaviour
{
    public Button defaultButton;
    public Button alternateDefaultButton;
    public GameObject alternateCanvas;
    public GameObject myEventSystem;
    private bool reconnected;

    // Start is called before the first frame update
    void Start()
    {
        reconnected = false;
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
            }
            else
            {
                if (alternateCanvas)
                {
                    if (alternateCanvas.activeInHierarchy)
                    {
                        if (!reconnected)
                        {
                            alternateDefaultButton.Select();
                            reconnected = true;
                        }
                    }
                    else
                    {
                        if (!reconnected)
                        {
                            defaultButton.Select();
                            reconnected = true;
                        }
                    }
                }
                else
                {
                    if (!reconnected)
                    {
                        defaultButton.Select();
                        reconnected = true;
                    }
                }
            }
        }
    }
}
