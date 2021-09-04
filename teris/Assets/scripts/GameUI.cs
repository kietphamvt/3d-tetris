using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    // Start is called before the first frame update
    public RawImage queue1, queue2, queue3, hold;
    public Text current_score;
    public Texture[] myTextures = new Texture[10];
    
    public void HoldBlock(int block)
    {
        hold.texture = myTextures[block - 1];
    }

    public void Display(int block1, int block2, int block3)
    {
        queue1.texture = myTextures[block1 - 1];
        queue2.texture = myTextures[block2 - 1];
        queue3.texture = myTextures[block3 - 1];
    }

    public void DisplayScore(int x)
    {
        current_score.text = (x).ToString();
    }
}
