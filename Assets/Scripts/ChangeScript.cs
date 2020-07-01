using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScript : MonoBehaviour
{
    private SpriteRenderer rend;
    private Sprite floorSprite1, floorSprite2 ,floorSprite3;
    private Sprite wallSprite1, wallSprite2 ,wallSprite3;

    private void Start(){
        ///belom coba dan baru kelarin floor tinggal wall
        //rend = GetComponent<SpriteRenderer>();
        //floorSprite1 = Resources.Load<Sprite>("floor1");
        // floorSprite2 = Resources.Load<Sprite>("floor2");
        // floorSprite3 = Resources.Load<Sprite>("floor3");

        // wallSprite1 = Resources.Load<Sprite>("wall1");
        // wallSprite2 = Resources.Load<Sprite>("wall2");
        // wallSprite3 = Resources.Load<Sprite>("wall3");

        //rend.sprite = floorSprite1;
        //rend.sprite = wallSprite1;
    }
    private void Update(){
        // if(StatsDisplayManager.levelCounter>=6){
        //     //Debug.Log("masuk");
        //     if(rend.sprite == floorSprite1){
        //         floorSprite1 = floorSprite2;
        //     }
        // }
        // else if(StatsDisplayManager.levelCounter>=11){
        //     if(rend.sprite == floorSprite2){
        //         floorSprite2 = floorSprite3;
        //     }
        // }
        // else{
        //     if(rend.sprite == floorSprite2){
        //         floorSprite2 = floorSprite1;
        //     }
        //     else if(rend.sprite == floorSprite3){
        //         floorSprite3 = floorSprite1;
        //     }
        // };
    }
}
