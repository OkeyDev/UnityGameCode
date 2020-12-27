using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Delete it
public class ImageController : MonoBehaviour
{
    public GameObject KeyboardObj;
    public GameObject JoystickObj;
    
    void Start() {
        if (Input.GetJoystickNames().Length > 0) {
            KeyboardObj.SetActive(false);
            JoystickObj.SetActive(true);
        } else if (Input.GetJoystickNames().Length == 0) {
            KeyboardObj.SetActive(true);
            JoystickObj.SetActive(false);
        }
    }

}
