using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    private Camera MainCamera;
    private float CameraHalfWidth;
    private float ObjectWidth;

    private void Start()
    {
        ObjectWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        MainCamera = Camera.main;
        float halfHeight = MainCamera.orthographicSize;
        CameraHalfWidth = MainCamera.aspect * halfHeight;
    }

    void Update()
    {
        float objectX = (this.transform.position.x + ObjectWidth);
        // MainCamera.transform.position.x gives the center of the camera
        float cameraStartX = MainCamera.transform.position.x - CameraHalfWidth;
        if (objectX < cameraStartX) {
            Destroy(this.gameObject);
        }
    }
}
