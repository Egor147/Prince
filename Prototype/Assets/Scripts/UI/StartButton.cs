﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public int level;
    public void GameLoading()
    {
        SceneManager.LoadScene(level);
    }
}
