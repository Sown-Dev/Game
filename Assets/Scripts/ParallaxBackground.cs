using System;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour{
    private Transform camTransform;
    private Vector3 lastPos;

    [SerializeField] private float parallaxMult;
    private float textureUnitSizeX;
    private float textureUnitSizeY;
    
    void Start(){
        camTransform = Camera.main.transform;
        lastPos = camTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
        textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
    }

    private void Update(){
        Vector3 MovementChange = camTransform.position - lastPos;
        transform.position += MovementChange * parallaxMult;
        lastPos = camTransform.position;

        if (Mathf.Abs(camTransform.position.x - transform.position.x )>= textureUnitSizeX){
            float offsetPositionX = (camTransform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector2(camTransform.position.x +offsetPositionX, transform.position.y);
        }
        
        if (Mathf.Abs(camTransform.position.y - transform.position.y) >= textureUnitSizeY){
            float offsetPositionY = (camTransform.position.y - transform.position.y) % textureUnitSizeY;
            transform.position = new Vector2( transform.position.x, camTransform.position.y +offsetPositionY);
        }
    }
}