using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MeteorManager : MonoBehaviour{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject meteorPrefab;

    public int bRange = 5;
    public int maxMeteors = 20;
    private int currentMeteors;
    private BoxCollider2D mColl;

    
    //this is used to count how many seconds have passed since there havent been enough meteors. once passed, new ones will start spawning
    private int meteorTime;
    private int maxTime = 50;

    private List<GameObject> mList;
    
    void Start(){
        mColl = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        transform.position = player.transform.position;
        if (currentMeteors < maxMeteors){
            meteorTime++;
            if (meteorTime > maxTime){
                spawnMeteor();
                meteorTime = 0;
            }
        }
        else{
            meteorTime = 0;
        }
    }

    private void spawnMeteor(){
        float cX = mColl.size.x / 2;
        float cY = mColl.size.y / 2;
        
        Vector2 randPos = new Vector2(Random.Range(-cX , cX ),
            Random.Range(-cY , cY ));
        
        
        Rect bounds = Rect.MinMaxRect(-cX + bRange, - cY +bRange, cX-bRange, cY-bRange);
        if (!bounds.Contains(randPos)){
            Instantiate(meteorPrefab, randPos + (Vector2)transform.position, Quaternion.identity );
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D coll){
        currentMeteors++;
    }

    private void OnTriggerExit2D(Collider2D other){
        currentMeteors--;
    }
}
