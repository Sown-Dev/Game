using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour{
    private InvItem[] inventory = new InvItem[InvSize];

    public static int InvSize = 10;

    public InvItem leftEquipped = null;
    public InvItem rightEquipped= null;
    
    void Start(){
        Debug.Log(RandomGunGenerator.rngGun);

        Insert(  RandomGunGenerator.rngGun.Generate(0.1f) );
        leftEquipped = inventory[0];
        Insert(  RandomGunGenerator.rngGun.Generate(0.3f) );
        rightEquipped = inventory[1];
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.R)){
            Debug.Log(RandomGunGenerator.rngGun);
            leftEquipped.data= RandomGunGenerator.rngGun.Generate(0.1f) ;
            rightEquipped.data= RandomGunGenerator.rngGun.Generate(0.3f) ;
        }
    }
    
    

    bool Insert( GunData newInsert){
        
            for (int i = 0; i < inventory.Length; i++){
                if (inventory[i] == null){
                    inventory[i] = new InvItem(newInsert);
                    return true;
                }
            }

            return false;

    }

    public class InvItem{
        //used as a wrapper class for gundata, in case I want to add more info to the inventory
        
        public GunData data;

        public InvItem(GunData _dat){
            data = _dat;
        }

    }
}
