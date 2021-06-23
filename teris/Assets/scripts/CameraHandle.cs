using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandle : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private Vector3 previousPosition, previousPosition2;
    float height;
    float sensitivity = 30f;
    void Start()
    {
        cam.transform.localPosition = new Vector3(0f, 25f, -31.8f);
        cam.transform.localEulerAngles = new Vector3(20f, 0f, 0f);
        height = 25f;
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
            cam.transform.RotateAround(new Vector3(0, height, 0), new Vector3(0, 1, 0), -direction.x * 180);    //Xoay theo Oy
            Vector3 tmp = cam.transform.localEulerAngles;
            tmp.x = (float)tmp.x + direction.y * 180;
            tmp.x = Mathf.Clamp(tmp.x, 0f, 40f);
            cam.transform.localEulerAngles = tmp;                                                           //Xoay theo Ox
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)                                                                                    //Zoom
        {
            float fov = cam.fieldOfView;
            fov -= scroll * sensitivity;
            fov = Mathf.Clamp(fov, 20f, 80f);
            cam.fieldOfView = fov;
        }

        if (Input.GetMouseButtonDown(1))
        {
            previousPosition2 = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 direction = previousPosition2 - cam.ScreenToViewportPoint(Input.mousePosition);
            height += direction.y * sensitivity ;
            height = Mathf.Clamp(height, 2.3f, 41.2f);
            Vector3 newPos = cam.transform.localPosition;
            newPos.y = height;
            cam.transform.localPosition = newPos;
            previousPosition2 = cam.ScreenToViewportPoint(Input.mousePosition);
        }
    }
}
