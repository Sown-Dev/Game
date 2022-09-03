using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking.Types;
using Random = UnityEngine.Random;

public class RandomGunGenerator : MonoBehaviour{
    //name arrays
    string[] baseName ={
        "blaster", "laser emitter", "repeater", "launcher", "destroyer", "sidearm", "railgun", "particle emitter",
        "bullet spewer", "dronekiller", "projectile accelerator"
    };

    string[] adjective ={
        "phenomenal", "black-market", "handmade", "quality", "chrome", "John's", "salvaged", "inexpensive", "great",
        "time-tested", "legendary", "heat-treated"
    };

    string[] adjective2 =
        { "tempered", "resistance", "scrapping", "old-fashioned", "rusty", "electromagnetic", "charged" };

    string[] ending ={ "of Terra", "of worlds", "of unknown origin", "of Andromeda", "" };

    // order:
    // adjective + adjective2 + baseName + ending


    public GameObject[] BulletTypes;
    public GameObject[] GunTypes;

    public int ramp; //increases w/ every generation, resulting in better stats over time

    public static RandomGunGenerator rngGun;

    void Awake(){
        //singleton
        rngGun = this;
    }

    //rarity is 0-1 used for better weapon quality, ramp is used to get higher stats later on
    public GunData Generate(float rarity){
        //in order to get different results when calling in the same frame, change the seed at the beginning of the function
        Random.InitState((int)System.DateTime.Now.Ticks * Random.Range(0, 100));

        //increase ramp
        ramp++;

        //used to equally increment arrays
        float gIncr = 100f / GunTypes.Length;
        float bIncr = 100f / BulletTypes.Length;

        //instantiate a scriptable object which holds the info
        GunData gDat = ScriptableObject.CreateInstance<GunData>();

        float gunRand = Random.Range(0, 100 + rarity * 0);

        for (int i = 0; i < GunTypes.Length; i++){
            //equally divides the array and checks for random value in range of one of the increments
            if (inRange(gunRand, i * gIncr, (i + 1) * gIncr)){
                gDat.type = GunTypes[i].GetComponent<GunClass>().type;
            }
        }

        //we now have our gun object. now we get the gun class
        //GunClass gClass = _gun.GetComponent<GunClass>();

        //Now do the same for bullet type
        float bulletRand = Random.Range(0, 80 + rarity * 20);

        for (int i = 0; i < BulletTypes.Length; i++){
            if (inRange(bulletRand, i * bIncr, (i + 1) * bIncr)){
                gDat.bullet = BulletTypes[i];
            }
        }

        //randomize stats:
        gDat.damage = Random.Range(10, 45) + ramp + (int)(12 * rarity);
        //implement something that reduces rof with higher damage
        gDat.rof = Mathf.Clamp(Random.Range(100, 400) + (ramp * 4) + (int)(80 * rarity)
                               - Mathf.Clamp((gDat.damage * 5), 0, 320), 100, 1000); //this part reduces rof based on damage
        gDat.pellets = Mathf.Clamp(Mathf.RoundToInt(Random.Range(0, 6) + ramp / 10 + rarity * 2), 2, 16);
        gDat.col = Random.ColorHSV();
        gDat.spread = Mathf.Clamp((Random.Range(2, 26) - ramp / 2 - rarity * 4), 2, 25);
        gDat.extraVel = Mathf.Clamp((Random.Range(0, 8) + rarity * 2), 0, 25);


        //name:
        gDat.gunName = GenerateName();
        //_gun.name = gClass.gunName;


        //gives the gun a unique ID
        gDat.ID = GetID();
        return gDat;
    }

    public static bool inRange(float value, float range1, float range2){
        return range1 <= value && value <= range2;
    }

    public string GenerateName(){
        string retName = "";
        retName += adjective[Mathf.RoundToInt(Random.Range(0, adjective.Length))] + " ";

        if (Random.Range(0, 3) < 2){
            retName += adjective2[Mathf.RoundToInt(Random.Range(0, adjective2.Length))] + " ";
        }

        retName += baseName[Mathf.RoundToInt(Random.Range(0, baseName.Length))] + " ";
        if (Random.Range(0, 10) < 1){
            retName += ending[Mathf.RoundToInt(Random.Range(0, ending.Length))];
        }

        //makes first letter uppercase
        retName = retName.First().ToString().ToUpper() + String.Join("", retName.Skip(1));

        return retName;
    }

    private static int nextID = 0;

    public static int GetID(){
        nextID++;
        return nextID;
    }
}