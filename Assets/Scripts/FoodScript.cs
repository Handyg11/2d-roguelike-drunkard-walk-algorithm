using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    private LevelGenerator lg;
    private StatsDisplayManager sdm;
    void Start()
    {
        lg = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelGenerator>();
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Player")){
            SoundManager.PlaySound("health");
            StatsDisplayManager.foodAmmount += 2;
            Destroy(gameObject);
        }
    }
}
