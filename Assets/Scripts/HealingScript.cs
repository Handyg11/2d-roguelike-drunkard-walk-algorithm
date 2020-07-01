using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingScript : MonoBehaviour
{

    private LevelGenerator lg;

    // Start is called before the first frame update
    void Start()
    {
        lg = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Player")){
            SoundManager.PlaySound("health");
            StatsDisplayManager.healthAmmount+=3;
            Destroy(gameObject);
        }
    }
}
