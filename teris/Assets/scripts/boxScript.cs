using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class boxScript : MonoBehaviour
{
    GameObject blocc;
    List<GameObject> OldBloccs = new List<GameObject>();
    Stopwatch watchu = new Stopwatch();
    
    //Instantiated GameObjects are spawned on default so we dont check collide on default but check on every other layers
    public LayerMask notDefault;
    public GameObject OldBloccFolder;

    int ren;
    float increm = 2.1f;
    int runtiem = 250;
    void Start()
    {
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
        ren = Random.Range(1,10);
        blocc = Instantiate(Resources.Load("New-Block/" + (ren).ToString())) as GameObject;
        blocc.transform.position = new Vector3(0.8f, 40, -0.99f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (watchu.ElapsedMilliseconds > runtiem)
        {
            watchu.Stop(); watchu = new Stopwatch(); watchu.Start();


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
                print("new blocks");
                spawnBlock();
            }
            else
            {
                blocc.transform.position -= new Vector3(0, increm, 0);
            }
        }
    }

    private bool FallDownCollide(GameObject blocc)
    {
        GameObject tblocc = Instantiate(blocc) as GameObject;
        tblocc.transform.position -= new Vector3(0, increm, 0);

        print("fall down collide");
        bool res = CheckCollision(tblocc);
        Destroy(tblocc);
        return res;
    }

    bool CheckCollision(GameObject blocc)
    {
        int n = blocc.transform.childCount;
        Transform child = blocc.transform.Find("Cube");
        if (Physics.OverlapBox(child.position, child.localScale, Quaternion.identity).Length > 0) return true;

        for (int i = 1; i<n; ++i)
        {
            child = blocc.transform.Find("Cube.00" + i.ToString());
            if (Physics.OverlapBox(child.position, child.localScale, Quaternion.identity).Length > 0) return true;
        }

        return false;
    }
}
