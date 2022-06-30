using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 1;
    }
    
    public GameObject menu;
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(!menu.activeSelf);
        }
        if(menu.activeSelf)
            Time.timeScale = 0.2f;
        else
            Time.timeScale = 1;
    }

    public void MenuOff()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
    }
}
