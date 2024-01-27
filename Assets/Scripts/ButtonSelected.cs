using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSelected : MonoBehaviour
{
    // Script used to highlight the button currently selected. When using a gamepad, it is not as
    // obvious which button is selected as it is when using a cursor. Thus, I created this script
    // to slightly increase the size of currently selected button.

    EventSystem eventSystem;

    void Start()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }
    
    void Update()
    {
        if (gameObject == eventSystem.currentSelectedGameObject)
        {
            gameObject.transform.localScale = new Vector3(1.15f, 1.15f, 1);
        }
        else
        {
            gameObject.transform.localScale = Vector3.one;
        }
    }    

}
