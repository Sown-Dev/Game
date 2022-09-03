using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour{
    private Camera cam;

    private void Start(){
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.mouseScrollDelta.y < 0){
            cam.orthographicSize+=0.5f;
        }
        if (Input.mouseScrollDelta.y > 0){
            cam.orthographicSize-= 0.5f;
        }
        cam.orthographicSize=  Mathf.Clamp(cam.orthographicSize, 4, 10);
    }
}
