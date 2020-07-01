using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour
{
    //private GameManagerScript gameManager;
    private LevelGenerator lg;

    private StatsDisplayManager sdm;

    // Start is called before the first frame update
    void Start()
    {
        //gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        lg = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelGenerator>();
        //sdm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StatsDisplayManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Player")){
            //gameManager.ammo += 20 ;
            //sdm.ammoAmmount +=20;
            SoundManager.PlaySound("ammo");
            StatsDisplayManager.ammoAmmount+=20;
            //Debug.Log(gameManager.ammo);
            Destroy(gameObject);
        }
    }
}
