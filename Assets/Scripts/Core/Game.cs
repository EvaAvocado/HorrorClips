﻿using System;
using Intro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class Game
    {
        public void ResetScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}