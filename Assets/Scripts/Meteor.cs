using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour, IDamageble{
    public int health;
 
    public void takeDamage(int dmg){
        health -= dmg;
        if (health <= 0){   
            Destroy(gameObject);
        }
    }
}
