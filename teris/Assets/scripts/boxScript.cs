using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class boxScript : MonoBehaviour
{
    //Current block & its childs
    int RandomBlock;
    public GameObject blocc;
    List<GameObject> ChildBloccs = new List<GameObject>();

    //Calculate time
    public Stopwatch watchu = new Stopwatch();

    //All non-default layers for collision checking
    public LayerMask notDefault;

    //Contains all old blocks (seperated) as child
    public GameObject OldBloccFolder;
    List<GameObject> OldBloccs = new List<GameObject>();

    int tienem500k = 500000;

    bool failed = false;

    //How much a block drops after <iterate> seconds
    public float increm = 2f;
    int iterate = 200;

    void Start()
    {
        //Spawn block and start time
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

        //Clone new random block & move to top
        RandomBlock = Random.Range(1, 10);
        blocc = Instantiate(Resources.Load("New-Block/" + (RandomBlock).ToString())) as GameObject;
        blocc.transform.position = new Vector3(0.8f, 40, -0.99f);

        //Add child to ChildBloccs for faster runtime
        int n = blocc.transform.childCount;
        Transform child = blocc.transform.Find("Cube");
        ChildBloccs.Add(child.gameObject);
        for (int i = 1; i < n; ++i)
        {
            child = blocc.transform.Find("Cube.00" + i.ToString());
            ChildBloccs.Add(child.gameObject);
        }
    }
    int tienem500k2 = 500000;
    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

        //<iterate> time passed
        if (watchu.ElapsedMilliseconds > iterate && !failed)
        {
            //Reset watch
            watchu.Stop(); watchu = new Stopwatch(); watchu.Start();
            
            //Moving block down
            layerChange();
        }
    }
    int tienem500k3 = 500000;
    public void layerChange()
    {
        //If block moving down = collide
        if (FallDownCollide(blocc))
        {
            do
            {
                if (blocc.transform.position == new Vector3(0.8f, 40, -0.99f)) { GameOver(); break; }
                //Get child, save child, spawn new block
                foreach (GameObject child in ChildBloccs)
                {
                    //Change parent, layer of child
                    child.transform.parent = OldBloccFolder.transform;
                    child.layer = 6;
                    //Add collider for collision check
                    child.AddComponent(typeof(BoxCollider));
                    //Add child to <OldBloccs>
                    OldBloccs.Add(child);
                }
                ChildBloccs = new List<GameObject>();
                spawnBlock();
            } while (false);
            
        }
        else
        {
            //No collision --> move down
            blocc.transform.position -= new Vector3(0, increm, 0);
        }
    }
    int tienem500k4 = 500000;
    void GameOver()
    {
        //lam cl j di ne
        failed = true;
    }

    public bool FallDownCollide(GameObject blocc)
    {
        //Check collision when moving down
        return CheckCollision(blocc, new Vector3(0, -increm, 0));
    }

    public bool CheckCollision(GameObject blocc, Vector3 delta)
    {
        //get child
        foreach (GameObject child in ChildBloccs)
        {
            //Create overlapbox surrounding the child, shift <delta> units (delta is type vector3)
            //If overlapbox have > 0 collisions
            if (Physics.OverlapBox(child.transform.position + delta, child.transform.localScale / 2, Quaternion.identity).Length > 0) 
                return true;
        }
        //no collision detected
        return false;
    }
}
