using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun Data", menuName = "ScriptableObjects/GunData", order = 1)]
public class GunData : ScriptableObject{
    
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
    
    //Quality of the gun. Higher quality will be more valuable when selling/ buying, etc.
    public int quality;
    public float rarity; // 0-1 rarity



}