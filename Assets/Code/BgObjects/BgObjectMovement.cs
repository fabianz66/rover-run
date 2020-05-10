using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgObjectMovement : MonoBehaviour
{
    private float cameraStartPos, objectStartPos;
    private GameObject mainCamera;
    public float parallaxEffect = 0.7f;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraStartPos = mainCamera.transform.position.x;
        objectStartPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = (mainCamera.transform.position.x - cameraStartPos) * parallaxEffect;
        transform.position = new Vector3(objectStartPos + dist, transform.position.y, transform.position.z);
    }
}
