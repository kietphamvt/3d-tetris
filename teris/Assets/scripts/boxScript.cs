using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class boxScript : MonoBehaviour
{
    GameObject blocc;

    int so69 = 69;
    string nulo = "toi biet toi nuq ma";

    //Calculate how much time has passed
    Stopwatch watchu = new Stopwatch();

    //Instantiated GameObjects are spawned on default so we dont check collide on default but check on every other layers
    public LayerMask notDefault;
    //An empty GameObject containing all blocks that have been placed; A list of old blocks
    public GameObject OldBloccFolder;
    List<GameObject> OldBloccs = new List<GameObject>();

    int RandomBlock;

    //The height of which a block drops every runtiem milisecond; the increm is (probably) the dimentions of each 
    //child block 
    float increm = 2f;
    int iterate = 250;

    void Start()
    {
        //Spawn block and start counting time
        spawnBlock();
        watchu.Start();
    }

    void spawnBlock()
    {
        /*2 story block y = 3.12; x = 8.9, z = 6.91; spawn top y=38.87
        1 story block y = 0.98; 
        --> 2.14^3
        Height khung = 38.19 --> 18 cuc 
        */

        RandomBlock = Random.Range(1, 10);

        //clone new block into blocc from folder Assets/Resources/New-Block & move to top
        blocc = Instantiate(Resources.Load("New-Block/" + (RandomBlock).ToString())) as GameObject;
        blocc.transform.position = new Vector3(0.8f, 40, -0.99f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

        //Move the block down after iterate seconds
        if (watchu.ElapsedMilliseconds > iterate)
        {
            //Reset watch
            watchu.Stop(); watchu = new Stopwatch(); watchu.Start();

            //If this block when moving down collides with another block
            //then get all child of this block:
            //move to OldBloccFolder, change the layer, add Boxcollider, and add to OldBloccs
            //After that, spawn new block
            if (FallDownCollide(blocc))
            {
                int n = blocc.transform.childCount;
                Transform child = blocc.transform.Find("Cube");
                child.parent = OldBloccFolder.transform;
                child.gameObject.layer = 6;
                child.gameObject.AddComponent(typeof(BoxCollider));
                OldBloccs.Add(child.gameObject);

                for (int i = 1; i < n; ++i)
                {
                    child = blocc.transform.Find("Cube.00" + i.ToString());
                    child.parent = OldBloccFolder.transform;
                    child.gameObject.layer = 6;
                    child.gameObject.AddComponent(typeof(BoxCollider));
                    OldBloccs.Add(child.gameObject);
                }
                spawnBlock();
            }
            else
            {
                //If this block doesnt collide, move it down
                blocc.transform.position -= new Vector3(0, increm, 0);
            }
        }
    }

    private bool FallDownCollide(GameObject blocc)
    {
        //GameObject tblocc = Instantiate(blocc) as GameObject;
        //tblocc.transform.position -= new Vector3(0, increm, 0);

        //print("fall down collide");
        //bool res = CheckCollision(tblocc);
        //Destroy(tblocc);
        return CheckCollision(blocc, new Vector3(0, -increm, 0));
    }

    bool CheckCollision(GameObject blocc, Vector3 delta)
    {
        //Get number of child in block, check all child: position + delta (eg. When moving down delta = -increm)
        //for each position + delta add an OverlapBox to check if it overlaps another block :)))
        int n = blocc.transform.childCount;
        Transform child = blocc.transform.Find("Cube");
        if (Physics.OverlapBox(child.position + delta, child.localScale / 2, Quaternion.identity).Length > 0) return true;

        for (int i = 1; i < n; ++i)
        {
            child = blocc.transform.Find("Cube.00" + i.ToString());
            if (Physics.OverlapBox(child.position + delta, child.localScale / 2, Quaternion.identity).Length > 0) return true;
        }

        return false;
    }
}
