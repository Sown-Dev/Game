using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour{

    [SerializeField] float forwardVelocity;
    [SerializeField] float turnSpeed;

    [SerializeField] private TMP_Text speedText;

    private float y;
    private float rotation;

    private float rotationMult;

    Rigidbody2D rb;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        if (Input.GetAxis("Vertical") != 0){
            y = Input.GetAxis("Vertical");
        }

        if (Input.GetAxis("Horizontal") != 0){
            rotation = -Input.GetAxis("Horizontal");
        }

        speedText.text = Mathf.Round(rb.velocity.magnitude) + "u/s";
    }

    void FixedUpdate(){
        rotationMult = turnSpeed;

        if (y > 0){
            rb.AddForce(transform.up * (y * forwardVelocity *1.8f));
            rotationMult = turnSpeed * 1.0f;
        }
        if (y < 0){
            rb.AddForce(transform.up * (y * forwardVelocity ));
            rotationMult = turnSpeed * 0.7f;
        }
        
        //do rotation
        if (Mathf.Abs(rotation) > 0.05f){
            rb.angularDrag = 0.7f;
            rb.AddTorque(rotation * turnSpeed * (rb.velocity.magnitude +55) * Time.deltaTime );
        }
        else{
            rb.angularDrag = 5f;
        }
        
        
    }
    
   
    
    
}