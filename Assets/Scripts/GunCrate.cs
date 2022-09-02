using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCrate : MonoBehaviour{
    public GameObject weapon;

    public GameObject pickUp(){
        //make sure to also play any sounds or animations here
        Destroy(gameObject);
        return weapon;
    }
    
}
