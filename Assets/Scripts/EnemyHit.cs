using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHit : MonoBehaviour
{
    private LevelGenerator lg;
    private Transform playerPos;
    private bool getHit;

    private StatsDisplayManager sdm;

    public int enemyHP = 3;
       void Start()
    {
        getHit = false;
        //nanti ganti tag Player di GMS jadi game manager 
        lg = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelGenerator>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        sdm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StatsDisplayManager>();
    }

    void OnTriggerEnter2D(Collider2D other){
            if(other.CompareTag("Player")){
                SoundManager.PlaySound("playerHit");
                //stats
                if(StatsDisplayManager.levelCounter >=11){
                    StatsDisplayManager.healthAmmount-=4;
                    StatsDisplayManager.foodAmmount -=8;
                    enemyHP-=5;
                }else{
                    StatsDisplayManager.healthAmmount-=2;
                    StatsDisplayManager.foodAmmount -=4;
                    enemyHP-=3;
                }
                if(StatsDisplayManager.foodAmmount <=0){
                    StatsDisplayManager.foodAmmount = 0;
                }
                //Debug.Log(player.health);
                if(enemyHP <= 0){
                    Destroy(transform.parent.gameObject);
                    lg.enemyCount -=1;
                    StatsDisplayManager.enemyKilled++;
                
                    if(StatsDisplayManager.levelCounter == 5||StatsDisplayManager.levelCounter == 10|| StatsDisplayManager.levelCounter == 5){
                        Debug.Log("don't end");
                    }else{
                        enemyZero();
                    }
                }
            }
            if(other.CompareTag("Bullet") && !getHit){
                enemyHP--;
                SoundManager.PlaySound("enemyHit");
                //Debug.Log(lg.enemyCount);
                Destroy(other.gameObject);
                if(enemyHP <= 0){
                    getHit = true;
                    SoundManager.PlaySound("enemyDeath");
                    //Debug.Log("dead");
                    Destroy(transform.parent.gameObject);
                    lg.enemyCount -=1;
                    StatsDisplayManager.enemyKilled++;
                    if(StatsDisplayManager.levelCounter == 5||StatsDisplayManager.levelCounter == 10|| StatsDisplayManager.levelCounter == 5){
                        Debug.Log("don't end");
                    }else{
                    enemyZero();
                    }
                }
            }
        }

        void enemyZero(){
        if(lg.enemyCount <= 0 && StatsDisplayManager.healthAmmount > 0){
            StatsDisplayManager.levelCounter++;
			// if(StatsDisplayManager.levelCounter==1){
			// 	SceneManager.LoadScene("test");
			// }
			if(StatsDisplayManager.levelCounter==5){
				SceneManager.LoadScene("CutScene");
			}
			else if(StatsDisplayManager.levelCounter==6){
				SceneManager.LoadScene("CutScene");
			}
			else if(StatsDisplayManager.levelCounter==10){
				SceneManager.LoadScene("CutScene");
			}
			else if(StatsDisplayManager.levelCounter==11){
				SceneManager.LoadScene("CutScene");
			}
			else if(StatsDisplayManager.levelCounter==15){
				SceneManager.LoadScene("CutScene");
			}
			else if(StatsDisplayManager.levelCounter==16){
				SceneManager.LoadScene("CutScene");
			}
			else{
				SceneManager.LoadScene("SplashLevel");
			};
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Debug.Log("test");
        }
    }
}
