using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private CameraFollow cameraFollow;

    private Vector3 cameraFollowPosition;
    
    void Start()
    {
        cameraFollow.Setup(() => cameraFollowPosition, () => 80f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
