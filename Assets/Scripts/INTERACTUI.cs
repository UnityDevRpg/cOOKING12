using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INTERACTUI : MonoBehaviour
{

    public GameObject StartMenu;
    public GameObject TutorialMenu;

    private void Start() {
        Time.timeScale = 0f;
        StartMenu = GameObject.Find("UI").transform.Find("StartOfGame").gameObject;  
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartMenu.SetActive(false);
            gameObject.SetActive(false);
            Time.timeScale = 1f;
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
