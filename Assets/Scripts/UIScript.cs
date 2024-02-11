using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public GameObject endMenu;
    public GameObject StartMenu;
    public GameObject TutorialMenu;

    private void Start() {
        Time.timeScale = 0f;
        TutorialMenu = GameObject.Find("UI").transform.Find("Tutorial").gameObject; 
        StartMenu = GameObject.Find("UI").transform.Find("StartOfGame").gameObject;  
    }

    public void Play()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartMenu.SetActive(false);

    }

    public void Quit () {
        Application.Quit();
    }

    public void Tutorial () {
        StartMenu.SetActive(false);
        TutorialMenu.SetActive(true);
    }

    public void ReturnToMenu () {
        StartMenu.SetActive(true);
        endMenu.SetActive(false);
    }

}
