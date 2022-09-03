using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlakGun : GunClassMain
{
    [SerializeField] Animator animator;
    /*
    Stats:
    Damage
    Velocity
    Spread
    Rof
    Pellets
    */

    [SerializeField] GameObject FlakBullet;
    private void Start(){
        float intensity = (col.r + col.g + col.b) / 3f;
        float factor = 6/intensity;
        col = new Vector4(col.r * factor, col.g * factor, col.b * factor);
        
        //gameObject.GetComponent<SpriteRenderer>().material.SetColor("_Color", col);

        SetStats();
        AdjustStats();
        
        nextShot=0f;
        animator = GetComponent<Animator>();
    }

    public override void Shoot(){
        if (Time.time >= nextShot){
            //animation:
            animator.Play("Base Layer.Shoot", 0, 0f);
            //to-do: sound & animation

            GameObject _bullet = Instantiate(FlakBullet, transform.position, Quaternion.Euler(SpreadDirection()));
            _bullet.GetComponent<Rigidbody2D>().velocity =
                GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity;

            _bullet.GetComponent<Bullet>().Init(damage, extraVel, col);
            _bullet.GetComponent<FlakBullet>().FlakInit(pellets, spread, bullet);

            
            Destroy(_bullet, 3f);
            
            
            nextShot = Time.time + 1f/ (rof/60);
        }
    }
    
    
    
    void AdjustStats(){
        damage = Mathf.RoundToInt(damage / 1.0f);
        spread /= 1.3f;
        rof /=  5f;
        extraVel /= 1f;
        pellets =(int) (pellets*2.5);
    }
}
