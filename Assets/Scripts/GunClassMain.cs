using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunClassMain : GunClass{
    public void SetStats(){
        ID = data.ID;
        type = data.type;
        gunName = data.gunName;
        col = data.col;

        //stats
        bullet = data.bullet;
        damage = data.damage;
        spread = data.spread;
        rof = data.rof;
        extraVel = data.extraVel;
        pellets = data.pellets;

        quality = data.pellets;
        rarity = data.rarity;
    }

    public override void Shoot(){ }
    
    public Vector3 SpreadDirection(){
        Vector3 targetPos=transform.eulerAngles;
        targetPos = new Vector3(
            targetPos.x,
            targetPos.y,
            targetPos.z + Random.Range(-spread, spread)
        );
        return targetPos;
    }
    
    public void SpawnBullet(Vector3 pos){
        GameObject _bullet = Instantiate(bullet, pos, Quaternion.Euler(SpreadDirection()));
        _bullet.GetComponent<Rigidbody2D>().velocity =
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity;
        
        _bullet.GetComponent<Bullet>().Init(damage, extraVel, col);
        Destroy(_bullet, 2f);
    }
}