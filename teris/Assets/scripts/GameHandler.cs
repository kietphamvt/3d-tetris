using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public GameOverScreen GameOverScreen;
    public PauseScript PauseScript;
    public void GameOver(int x)
    {
        GameOverScreen.DisplayScore(x);
        GameOverScreen.Setup();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !PauseScript.Paused) PauseScript.Setup();
    }
}
