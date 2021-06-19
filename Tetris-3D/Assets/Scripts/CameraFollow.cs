using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 cameraFollowPosition;

    public void Setup(Vector3 cameraFollowPosition)
    {
        this.cameraFollowPosition = cameraFollowPosition;
    }

    void Update()
    {
        cameraFollowPosition = new Vector3(-42.97f, 68.83f, 38.69f);
        cameraFollowPosition.z = transform.position.z;
        transform.position = cameraFollowPosition;
    }
}
