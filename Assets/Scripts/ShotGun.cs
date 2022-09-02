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

    void SpawnBullet(Vector3 pos){
        GameObject _bullet = Instantiate(bullet, pos, Quaternion.Euler(SpreadDirection()));
        _bullet.GetComponent<Rigidbody2D>().velocity =
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity;
        _bullet.GetComponent<Rigidbody2D>().angularVelocity = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<Rigidbody2D>().angularVelocity;
        _bullet.GetComponent<Bullet>().Init(damage, extraVel, col);
        Destroy(_bullet, 2f);
    }
    Vector3 SpreadDirection(){
        Vector3 targetPos=transform.eulerAngles;
        targetPos = new Vector3(
            targetPos.x,
            targetPos.y,
            targetPos.z + Random.Range(-spread, spread)
        );
        return targetPos;
    }

    void AdjustStats(){
        damage /= 4;
        spread *= 3.5f;
        rof /= 3.3f;
        extraVel /= 2;
        pellets *= 2;
    }
}