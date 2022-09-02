using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour, Bullet{ 
    private Rigidbody2D rb;
    private Collider2D myCollider;
    private int damage = 10;

    void Awake(){
        myCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start(){
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");     
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        
        rb.AddForce(transform.up * 80f);
        
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Bullets"){
            Physics2D.IgnoreCollision(myCollider, collision.collider);
            return;
        }
        
        if (collision.collider.gameObject.GetComponent<IDamageble>() != null){
            collision.collider.gameObject.GetComponent<IDamageble>().takeDamage(damage);
        }
        
        Destroy(gameObject);
    }

    public void Init(int dmg, float velocity, Color color){
        damage += dmg;
        rb.AddForce(transform.up * velocity);
    }
    
    
}
