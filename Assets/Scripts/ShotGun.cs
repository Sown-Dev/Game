using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShotGun : GunClassMain
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

            for (int i = 0; i < pellets; i++){
                SpawnBullet(transform.position);
            }

            nextShot = Time.time + 1f/ (rof/60);
        }
    }

    

    void AdjustStats(){
        damage /= 4;
        spread *= 3.6f;
        rof /= 3.3f;
        extraVel /= 2;
        pellets *= 2;
    }
}