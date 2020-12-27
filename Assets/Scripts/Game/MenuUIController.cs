using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// This class control a main menu in start of game
/// </summary>
public class MenuUIController : MonoBehaviour
{
    // Start is called before the first frame update
    public  void PlayButtonClicked() {
        SceneManager.LoadScene("NormalGame");
    }
    public  void ExitButtonClicked( ) {
        Application.Quit();
    }
}
