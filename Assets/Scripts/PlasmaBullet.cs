using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBullet : MonoBehaviour, Bullet
{
    private Rigidbody2D rb;
    private Collider2D myCollider;
    private int damage = 10;

    public Color col;

    void Awake(){
        myCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start(){
        col.a = 160;
        float intensity = (col.r + col.g + col.b) / 3f;
        float factor = 6/intensity;
        Color fincol = new Vector4(col.r * factor, col.g * factor, col.b * factor);
        
        gameObject.GetComponent<SpriteRenderer>().material.SetColor("_Color", fincol);
        gameObject.GetComponent<SpriteRenderer>().color = col;
        

        GameObject player = GameObject.FindGameObjectWithTag("Player");     
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        
        rb.AddForce(transform.up * 40f);
        
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Bullets"){
            Physics2D.IgnoreCollision(collision.collider, myCollider, false);
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
        col = color;
        float scale = 0.45f+ 0.4f *(dmg / 50);
        transform.localScale = new Vector3(scale, scale, scale);

    }

}
