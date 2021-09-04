using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class boxScript : MonoBehaviour
{
    public GameHandler GameHandler;
    //Current block & its childs
    int block1, block2, block3;
    public GameObject blocc;
    public List<GameObject> ChildBloccs = new List<GameObject>();
    public bool isHold = false, newHold = true;
    public int block_in_hold = -1, cur = -1;
    public int score = 0;

    //Calculate time
    public Stopwatch watchu = new Stopwatch();

    //All non-default layers for collision checking
    public LayerMask notDefault;

    //Contains all old blocks (seperated) as child
    public GameObject OLDBLOCCFOLDER;
    public GameUI Queue;
    public GameUI Hold;
    public GameUI Score;
    List<GameObject>[] OLDBLOCCS = new List<GameObject>[24];
    //cái này là cái ông cần nè, ông có thể dùng theo kiểu lấy từ list (nếu tạo script khác thì làm giống khoa) hoặc là lấy từ cái gameobject (lấy child)

    public bool failed = false;

    //How much a block drops after <iterate> seconds
    public float increm = 2f;
    int iterate = 1000;

    void Start()
    {
        Score.DisplayScore(0);
        //Spawn block and start time
        block1 = Random.Range(1, 10);
        block2 = Random.Range(1, 10);
        block3 = Random.Range(1, 10);
        spawnBlock(block1);
        queue_work();
        watchu.Start();
        for (int i = 0; i < OLDBLOCCS.Length; i++)
            OLDBLOCCS[i] = new List<GameObject>();
    }

    void queue_work()
    {
        block1 = block2;
        block2 = block3;
        block3 = Random.Range(1, 10);
        Queue.Display(block1, block2, block3);
    }

    void spawnBlock(int block1)
    {

        //Clone new random block & move to top
        blocc = Instantiate(Resources.Load("New-Block/" + (block1).ToString())) as GameObject;
        cur = block1;
        //queue_work();
        blocc.transform.position = new Vector3(1f, 40, -1f);

        //Add child to ChildBloccs for faster runtime
        ChildBloccs = new List<GameObject>();
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
            newHold = true;
            do
            {
                if (blocc.transform.position == new Vector3(1f, 40, -1f)) 
                {
                    failed = true;
                    FindObjectOfType<GameHandler>().GameOver(score);
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
                spawnBlock(block1);
                queue_work();
            } while (false);
            
        }
        else
        {
            //No collision --> move down
            blocc.transform.position -= new Vector3(0, increm, 0);
            if (isHold)
            {
                isHold = false;
                newHold = false;
                if (block_in_hold == -1)
                {
                    block_in_hold = cur;
                    Hold.HoldBlock(block_in_hold);
                    Destroy(blocc);
                    spawnBlock(block1);
                    queue_work();
                }
                else
                {
                    int tmp = block_in_hold;
                    block_in_hold = cur;
                    cur = tmp;
                    Hold.HoldBlock(block_in_hold);
                    Destroy(blocc);
                    spawnBlock(cur);
                }
            }
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
        int cnt = 0;
        for (int i = 0; i <= 20; ++i)
        {
            if (OLDBLOCCS[i].Count == 36)
            {
                ++score;
                ++cnt;
                foreach(GameObject child in OLDBLOCCS[i])
                {
                    Destroy(child);
                }
                OLDBLOCCS[i].Clear();
            }
            else
            {
                if (cnt > 0)
                {
                    foreach (GameObject child in OLDBLOCCS[i])
                    {
                        child.transform.position -= new Vector3(0, 2 * cnt, 0);
                        OLDBLOCCS[i - cnt].Add(child);
                    }
                    OLDBLOCCS[i].Clear();
                }
            }
        }
        Score.DisplayScore(score);
    }

}
