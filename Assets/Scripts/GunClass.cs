using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunClass : MonoBehaviour{
    //Stores all relevant info for the gun
    public GunData data;
    
    //Unique ID used to compare weapons to see if they are the same
    public int ID;
    
    //vanity stats:
    public string type;
    public string gunName; //Used to give object a unique randomized name
    public Color col; //
    
    //stats
    public GameObject bullet;
    public int damage;
    public float spread;
    public float rof;
    public float extraVel;
    public int pellets;
    
    //used for rof calculations
    [HideInInspector] public float nextShot;
    public abstract void Shoot();
    
    //Quality of the gun. Higher quality will be more valuable when selling/ buying, etc.
    public int quality;
    public float rarity;


}
