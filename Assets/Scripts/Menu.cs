﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void StartGame()
    {
        SceneManager.LoadScene("Scene1");
    }
}
