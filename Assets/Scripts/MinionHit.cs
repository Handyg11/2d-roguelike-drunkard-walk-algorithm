using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionHit : MonoBehaviour
{
    private Transform playerPos;
    private bool getHit;
    public int enemyHP = 2;
       void Start()
    {
        getHit = false;
        //nanti ganti tag Player di GMS jadi game manager
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnTriggerEnter2D(Collider2D other){
            if(other.CompareTag("Player")){
                enemyHP-=2;
                SoundManager.PlaySound("playerHit");
                //stats
                StatsDisplayManager.healthAmmount-=2;
                StatsDisplayManager.foodAmmount -=3;
                //Debug.Log(player.health);
                if(enemyHP <= 0){
                    Destroy(transform.parent.gameObject);
                    StatsDisplayManager.enemyKilled++;
                }
            }
            if(other.CompareTag("Bullet") && !getHit){
                enemyHP--;
                SoundManager.PlaySound("enemyHit");
                Destroy(other.gameObject);
                if(enemyHP <= 0){
                    getHit = true;
                    SoundManager.PlaySound("enemyDeath");
                    Destroy(transform.parent.gameObject);
                    StatsDisplayManager.enemyKilled++;
                }
            }
        }
}
