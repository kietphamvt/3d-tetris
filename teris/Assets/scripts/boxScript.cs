using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class boxScript : MonoBehaviour
{
    GameObject blocc;
    Stopwatch watchu = new Stopwatch();
    public LayerMask playahmask;
    int ren;
    float increm = 2f;
    int runtiem = 1000;
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
        ren = Random.Range(1,12);
        blocc = Instantiate(Resources.Load("blocks/" + (ren).ToString())) as GameObject;
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

            blocc.transform.position -= new Vector3(0, increm, 0);

            if (Physics.OverlapSphere(blocc.transform.position, 0.25f).Length > 0)
            {
                blocc.AddComponent(typeof(BoxCollider));
                BoxCollider hehe = blocc.GetComponent(typeof(BoxCollider)) as BoxCollider;
                hehe.isTrigger = true;
                spawnBlock();
            }
        }
    }
}
