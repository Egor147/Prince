using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1;
    }
    
    public void Exit()
    {
        Application.Quit();
    }
}
