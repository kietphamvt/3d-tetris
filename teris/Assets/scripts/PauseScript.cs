using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    // Start is called before the first frame update
    public boxScript boxScript;
    public bool Paused = false;
    public void Setup()
    {
        gameObject.SetActive(true);
        boxScript.failed = true;
        Paused = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            boxScript.failed = false;
            Paused = false;
            gameObject.SetActive(false);
        }    
    }
}
