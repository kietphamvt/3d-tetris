using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class moveBlock : MonoBehaviour
{
    public float increm = 2f;
    public GameUI Hold;
    GameObject Target;
    public boxScript box;
    bool instantDrop = false;
    bool blockFall = false;
    bool[] blockMove = new bool[] { false, false, false, false, false, false, false, false }; //"up", "down", "left", "right", "w", "s", "a", "d"
    public Vector3[] move;
    public Vector3[] spin;
    string[] name_move = new string[] { "up", "down", "left", "right", "w", "s", "a", "d" };
    int delay = -1;
    
    Stopwatch time = new Stopwatch();

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("a bokkusu");
        box = Target.GetComponent<boxScript>();
        move = new Vector3[] { new Vector3(0, 0, increm), new Vector3(0, 0, -increm), new Vector3(-increm, 0, 0), new Vector3(increm, 0, 0) };
        spin = new Vector3[] { new Vector3(1, 0, 0), new Vector3(-1, 0, 0), new Vector3(0, 0, 1), new Vector3(0, 0, -1)};
    }

// Update is called once per frame
    void Update()
    {
        if (!box.failed)
        {
            if (Input.GetKey(KeyCode.Space) && time.ElapsedMilliseconds > delay)
            {
                time.Stop(); time.Reset();
                delay = 150;
                blockFall = true;
                box.watchu.Stop(); box.watchu = new Stopwatch(); box.watchu.Start();
            }
            for (int i = 0; i < name_move.Length; ++i)
            {
                if (Input.GetKeyDown(name_move[i]))
                {
                    blockMove[i] = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                instantDrop = true;
            }
            if (Input.GetKeyDown(KeyCode.C) && box.newHold)
            {
                box.isHold = true;
            }
        }
    }
    private void FixedUpdate()
    {
        if (blockFall == true)
        {
            //box.blocc.transform.position -= new Vector3(0, increm, 0);
            time.Start();
            box.fall();
            blockFall = false;
        }
        for (int i=0;i<4;++i)
        {
            if (blockMove[i])
            {
                if (!box.CheckCollision(box.ChildBloccs, move[i]))
                    box.blocc.transform.position += move[i];
                blockMove[i] = false;
            }
        }
        for (int i=4;i<blockMove.Length;++i)
        {
            if (blockMove[i])
            {
                GameObject t = Instantiate(box.blocc) as GameObject;
                Vector3 position = t.GetComponentInChildren<Renderer>().bounds.center;
                t.transform.RotateAround(position, spin[i - 4], 90);

                //code der
                List<GameObject> ChildBloccs = new List<GameObject>();
                int n = t.transform.childCount;
                Transform child = t.transform.Find("Cube");
                ChildBloccs.Add(child.gameObject);
                for (int buc = 1; buc < n; ++buc)
                {
                    child = t.transform.Find("Cube.00" + buc.ToString());
                    ChildBloccs.Add(child.gameObject);
                }
                //code der

                //t.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
                if (!box.CheckCollision(ChildBloccs, new Vector3(0, 0, 0)))
                {
                    Vector3 p = box.blocc.GetComponentInChildren<Renderer>().bounds.center;
                    box.blocc.transform.RotateAround(p, spin[i - 4], 90);
                }
                //box.blocc.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
                blockMove[i] = false;
                Destroy(t);
            }
        }
        if (instantDrop)
        {
            //GameObject t = Instantiate(box.blocc);
            Vector3 go = new Vector3(0, -increm, 0);
            while (box.CheckCollision(box.ChildBloccs, go) == false)
            {
                box.blocc.transform.position += go;
            }
            //box.blocc.transform.position = t.transform.position;
            instantDrop = false;
        }
    }
}
