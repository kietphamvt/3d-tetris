using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text FinalScore;
    public void Setup()
    {
        gameObject.SetActive(true);
    }

    public void DisplayScore(int x)
    {
        FinalScore.text = "Score : " + (x).ToString();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //"Game"
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
