using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Shooting : MonoBehaviour{
    public GameObject leftHolder;
    public GameObject rightHolder;

    public InventoryManager inMan;
    
    
    //need list of gun types:
    public GameObject NormalGun;
    public GameObject DoubleGun;
    public GameObject ShotGun;
    public GameObject FlakGun;

    void Start(){ }

    void Update(){
        // for memory efficiency as well as readability, get the children at the start of update
        GunData lEquip = null;
        GunData lData = null;
        GameObject lChild = null;
        if (leftHolder.transform.childCount > 0){
            lChild = leftHolder.transform.GetChild(0).gameObject;
            if (lChild.GetComponent<GunClass>() != null){
                lData = lChild.GetComponent<GunClass>().data;
            }
        }
        if (inMan.leftEquipped != null){
            lEquip = inMan.leftEquipped.data;
        }
        
        
        GunData rEquip = null;
        GunData rData = null;
        GameObject rChild = null;
        if (rightHolder.transform.childCount > 0){
            rChild = rightHolder.transform.GetChild(0).gameObject;
            if (rChild.GetComponent<GunClass>() != null){
                rData = rChild.GetComponent<GunClass>().data;
            }
        }

        if (inMan.rightEquipped != null){
            if (inMan.rightEquipped != null){
                rEquip = inMan.rightEquipped.data;
            }
        }




        //check if equipped has changed
        if (rData != null && rEquip != null){
            if (rData.ID != rEquip.ID){
                //change weapon
                weaponChange( rEquip, rightHolder.transform);
            }
        }
        if (rData == null && rEquip != null){
            //change weapon
            weaponChange( rEquip, rightHolder.transform);
        }
        if (rData != null && rEquip == null){
            //change weapon
            //actually just destroy all children so that its empty
            if (rightHolder.transform.childCount > 0){
                foreach (Transform child in rightHolder.transform) {
                    Destroy(child.gameObject);
                }
            }
        }
        
        
        
        //LEFT 
        //check if equipped has changed
        if (lData != null && lEquip != null){
            if (lData.ID != lEquip.ID){
                //change weapon
                weaponChange( lEquip, leftHolder.transform);
            }
        }
        if (lData == null && lEquip != null){
            //change weapon
            weaponChange( lEquip, leftHolder.transform);
        }
        if (lData != null && lEquip == null){
            //change weapon
            //actually just destroy all children so that its empty
            if (leftHolder.transform.childCount > 0){
                foreach (Transform child in leftHolder.transform) {
                    Destroy(child.gameObject);
                }
            }
        }
        
        


        


        if (Input.GetMouseButton(0)){
            if (lChild != null){
                lChild.GetComponent<GunClass>().Shoot();
            }
            
            if (rChild != null){
                rChild.GetComponent<GunClass>().Shoot();
            }
        }
    }


    public void weaponChange(GunData wep, Transform parent){
        if (parent.childCount > 0){
            foreach (Transform child in rightHolder.transform){
                Destroy(child.gameObject);
            }
        }

        GameObject obj = new GameObject();
        foreach (GameObject _gun in Utils.main.Guns){
            if (wep.type == _gun.GetComponent<GunClass>().type){
                obj = Instantiate(_gun, leftHolder.transform);
                break;
            }
        }

        GunClass gClass = obj.GetComponent<GunClass>();
        gClass.data = wep;
    }


}