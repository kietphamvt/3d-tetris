using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandle : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private Vector3 previousPosition;
    void Start()
    {
        cam.transform.localPosition = new Vector3(0f, 25f, -31.8f);
        cam.transform.localEulerAngles = new Vector3(20f, 0f, 0f);
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);
            cam.transform.RotateAround(new Vector3(0, 25, 0), new Vector3(0, 1, 0), -direction.x * 180);    //Xoay theo Oy
            Vector3 tmp = cam.transform.localEulerAngles;
            tmp.x = (float)tmp.x + direction.y * 180;
            if (tmp.x < 0) tmp.x = 0f;
            else if (tmp.x > 40f) tmp.x = 40f;
            cam.transform.localEulerAngles = tmp;                                                           //Xoay theo Ox
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }    
    }
}
