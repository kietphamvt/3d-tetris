using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public GameOverScreen GameOverScreen;
    public void GameOver()
    {
        GameOverScreen.Setup();
    }
}
