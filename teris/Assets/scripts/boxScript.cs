using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class boxScript : MonoBehaviour
{
    public GameHandler GameHandler;
    //Current block & its childs
    int RandomBlock;
    public GameObject blocc;
    public List<GameObject> ChildBloccs = new List<GameObject>();

    //Calculate time
    public Stopwatch watchu = new Stopwatch();

    //All non-default layers for collision checking
    public LayerMask notDefault;

    //Contains all old blocks (seperated) as child
    public GameObject OLDBLOCCFOLDER;
    List<GameObject>[] OLDBLOCCS = new List<GameObject>[21];
    //cái này là cái ông cần nè, ông có thể dùng theo kiểu lấy từ list (nếu tạo script khác thì làm giống khoa) hoặc là lấy từ cái gameobject (lấy child)

    bool failed = false;

    //How much a block drops after <iterate> seconds
    public float increm = 2f;
    int iterate = 500;

    void Start()
    {
        //Spawn block and start time
        spawnBlock();
        watchu.Start();
        for (int i = 0; i < OLDBLOCCS.Length; i++)
            OLDBLOCCS[i] = new List<GameObject>();
    }

    void spawnBlock()
    {

        //Clone new random block & move to top
        RandomBlock = Random.Range(6,6);
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

    private void FixedUpdate()
    {

        //<iterate> time passed
        if (watchu.ElapsedMilliseconds > iterate && !failed)
        {
            //Reset watch
            watchu.Stop(); watchu = new Stopwatch(); watchu.Start();
            
            //move block down
            fall();
        }
    }
    public void fall()
    {
        //If block moving down = collide
        if (FallDownCollide(blocc))
        {
            do
            {
                if (blocc.transform.position == new Vector3(0.8f, 40, -0.99f)) 
                {
                    failed = true;
                    FindObjectOfType<GameHandler>().GameOver();
                    break;
                }
                //Get child, save child, spawn new block
                foreach (GameObject child in ChildBloccs)
                {
                    //Change parent, layer of child
                    child.transform.parent = OLDBLOCCFOLDER.transform;
                    child.layer = 6;
                    //Add collider for collision check
                    child.AddComponent(typeof(BoxCollider));
                    //Add child to <OLDBLOCCS>
                    int id = (int)(child.transform.position.y / 2);
                    OLDBLOCCS[id].Add(child);
                }
                ClearRow();
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

    public bool FallDownCollide(GameObject blocc)
    {
        //Check collision when moving down
        return CheckCollision(ChildBloccs, new Vector3(0, -increm, 0));
    }

    public bool CheckCollision(List<GameObject> ChildBloccs, Vector3 delta)
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

    void ClearRow()
    {
        for (int i = 0; i <= 20; ++i)
        {
            if (OLDBLOCCS[i].Count == 6)
            {
                foreach(GameObject child in OLDBLOCCS[i])
                {
                    Destroy(child);
                }
                OLDBLOCCS[i].Clear();
            }
        }
    }

}
