using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class moveBlock : MonoBehaviour
{
    GameObject Target;
    public boxScript box;
    bool blockFall = false;
    bool[] blockMove = new bool[] { false, false, false, false, false, false, false, false }; //Up, Down, Left, Right x2
    string[] name_move = new string[] { "up", "down", "left", "right", "w", "s", "a", "d" };
    int delay = -1;
    public float increm = 2f;
    Vector3 Up_Down, Left_Right;
    Stopwatch time = new Stopwatch();

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("a bokkusu");
        box = Target.GetComponent<boxScript>();
        Up_Down = new Vector3(0, 0, increm);
        Left_Right = new Vector3(increm, 0, 0);
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
        if (blockMove[0])
        {
            if (!box.CheckCollision(box.blocc, new Vector3(0, 0, increm)))
                box.blocc.transform.position += new Vector3(0, 0, increm);
            blockMove[0] = false;
        }
        if (blockMove[1])
        {
            if (!box.CheckCollision(box.blocc, new Vector3(0, 0, -increm)))
                box.blocc.transform.position -= new Vector3(0, 0, increm);
            blockMove[1] = false;
        }
        if (blockMove[2])
        {
            if (!box.CheckCollision(box.blocc, new Vector3(-increm, 0, 0)))
                box.blocc.transform.position -= new Vector3(increm, 0, 0);           
            blockMove[2] = false;
        }
        if (blockMove[3])
        {
            if (!box.CheckCollision(box.blocc, new Vector3(increm, 0, 0)))
                box.blocc.transform.position += new Vector3(increm, 0, 0);
            blockMove[3] = false;
        }
        if (blockMove[4])
        {
            GameObject t = Instantiate(box.blocc); 
            t.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
            if (!box.CheckCollision(t, new Vector3(0, 0, 0)))
                box.blocc.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
            blockMove[4] = false;
            Destroy(t);
        }
        if (blockMove[5])
        {
            GameObject t = Instantiate(box.blocc);
            t.transform.Rotate(-90.0f, 0.0f, 0.0f, Space.Self);
            if (!box.CheckCollision(t, new Vector3(0, 0, 0)))
                box.blocc.transform.Rotate(-90.0f, 0.0f, 0.0f, Space.Self);
            blockMove[5] = false;
            Destroy(t);
        }
        if (blockMove[6])
        {
            GameObject t = Instantiate(box.blocc);
            t.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
            if (!box.CheckCollision(t, new Vector3(0, 0, 0)))
                box.blocc.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
            blockMove[6] = false;
            Destroy(t);
        }
        if (blockMove[7])
        {
            GameObject t = Instantiate(box.blocc);
            t.transform.Rotate(0, 0, -90.0f, Space.Self);
            if (!box.CheckCollision(t, new Vector3(0, 0, 0)))
                box.blocc.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
            blockMove[7] = false;
            Destroy(t);
        }
    }
}
