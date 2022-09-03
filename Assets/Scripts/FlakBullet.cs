using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FlakBullet : MonoBehaviour, Bullet{
    private Rigidbody2D rb;
    private Collider2D myCollider;

    public int damage = 10;
    private int pellets;
    private float spread;

    private float detDist = 4f;
    private Vector3 startPos;

    public GameObject BulletType;
    

    public Color col;

    void Awake(){
        myCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start(){

        startPos = transform.position;


        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        rb.AddForce(transform.up * 50f);
    }

    void Update(){
       
        if (Vector3.Distance(startPos, transform.position) >= detDist){
            Debug.Log("bruh");
            detonate();
        }
    }

    void detonate(){
        for (int i = 0; i < pellets; i++){
            SpawnBullet(transform.position);
        }
        
        
        Destroy(gameObject);
    }
    
    void SpawnBullet(Vector3 pos){
        GameObject _bullet = Instantiate(BulletType, pos, Quaternion.Euler(SpreadDirection()));
        _bullet.GetComponent<Rigidbody2D>().velocity =
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity;
        _bullet.GetComponent<Bullet>().Init(damage/2, -4f, col);
        Destroy(_bullet, 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Bullets"){
            Physics2D.IgnoreCollision(collision.collider, myCollider, false);
            return;
        }

        if (collision.collider.gameObject.GetComponent<IDamageble>() != null){
            collision.collider.gameObject.GetComponent<IDamageble>().takeDamage(damage);
        }
        detonate();
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

    public void Init(int dmg, float velocity, Color color){
        damage += dmg;
        rb.AddForce(transform.up * velocity);
        col = color;
        
    }

    public void FlakInit(int _pellets, float _spread, GameObject _bullet){
        pellets = _pellets;
        spread = _spread *11;
        BulletType = _bullet;
    }
}