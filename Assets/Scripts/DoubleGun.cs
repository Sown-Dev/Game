using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DoubleGun : GunClassMain
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

    private float offset = 0.06f;
    private void Start(){
        float intensity = (col.r + col.g + col.b) / 3f;
        float factor = 6/intensity;
        col = new Vector4(col.r * factor, col.g * factor, col.b * factor);
        
        SetStats();
        AdjustStats();
        nextShot=0f;
        animator = GetComponent<Animator>();
    }

    public override void Shoot(){
        if (Time.time >= nextShot){
            //animation:
            animator.Play("Base Layer.Shoot", 0, 0f);
            
            //to-do: sound 

            Vector3 SpawnPos = transform.position;
            SpawnBullet(SpawnPos + (transform.right* -offset));
            SpawnBullet(SpawnPos + (transform.right * offset));

            nextShot = Time.time + 1f/  (rof/60);
        }
    }

    
   
    
    void AdjustStats(){
        damage = Mathf.RoundToInt(damage / 1.2f);
        spread /= 4f;
        rof /= 1.4f;
        extraVel /= 0.8f;
    }
}
