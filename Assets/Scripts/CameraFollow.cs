using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    [SerializeField] private float lerpRate;

    // Update is called once per frame
    void FixedUpdate () {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + new Vector3(0,0,-10) ,lerpRate * Time.deltaTime);
    }
}
