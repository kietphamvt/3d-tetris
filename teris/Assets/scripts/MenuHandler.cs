using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public HelpScreen HelpScreen;
    public HelpScreen CreditScreen;
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Help()
    {
        HelpScreen.Setup();
    }

    public void Credit()
    {
        CreditScreen.Setup();
    }
}
