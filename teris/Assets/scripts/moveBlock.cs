using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class moveBlock : MonoBehaviour
{
    public float increm = 2f;
    GameObject Target;
    public boxScript box;
    bool blockFall = false;
    bool[] blockMove = new bool[] { false, false, false, false, false, false, false, false }; //"up", "down", "left", "right", "w", "s", "a", "d"
    Vector3[] move;
    string[] name_move = new string[] { "up", "down", "left", "right", "w", "s", "a", "d" };
    int delay = -1;
    
    Vector3 Up_Down, Left_Right;
    Stopwatch time = new Stopwatch();

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("a bokkusu");
        box = Target.GetComponent<boxScript>();
        Up_Down = new Vector3(0, 0, increm);
        Left_Right = new Vector3(increm, 0, 0);
        move = new Vector3[] { new Vector3(0, 0, increm), new Vector3(0, 0, -increm), new Vector3(-increm, 0, 0), new Vector3(increm, 0, 0),
                               new Vector3(1, 0, 0), new Vector3(-1, 0, 0), new Vector3(0, 0, 1), new Vector3(0, 0, -1)};
    }

// Update is called once per frame
void Update()
    {
        if (Input.GetKey(KeyCode.Space) && time.ElapsedMilliseconds > delay)
        {
            time.Stop(); time.Reset();
            delay = 150; 
            blockFall = true;
            box.watchu.Stop(); box.watchu = new Stopwatch(); box.watchu.Start();
        }
        for (int i=0;i< name_move.Length;++i)
        {
            if (Input.GetKeyDown(name_move[i]))
            {
                blockMove[i] = true;
            }
        }
    }
    private void FixedUpdate()
    {
        if (blockFall == true)
        {
            //box.blocc.transform.position -= new Vector3(0, increm, 0);
            time.Start();
            box.layerChange();
            blockFall = false;
        }
        for (int i=0;i<4;++i)
        {
            if (blockMove[i])
            {
                if (!box.CheckCollision(box.blocc, move[i]))
                    box.blocc.transform.position += move[i];
                blockMove[i] = false;
            }
        }
        for (int i=4;i<blockMove.Length;++i)
        {
            if (blockMove[i])
            {
                GameObject t = Instantiate(box.blocc);
                Vector3 position = t.GetComponentInChildren<Renderer>().bounds.center;
                t.transform.RotateAround(position, move[i], 90);
                //t.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
                if (!box.CheckCollision(t, new Vector3(0, 0, 0)))
                {
                    Vector3 p = box.blocc.GetComponentInChildren<Renderer>().bounds.center;
                    box.blocc.transform.RotateAround(p, move[i], 90);
                }
                //box.blocc.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
                blockMove[i] = false;
                Destroy(t);
            }
        }
    }
}
