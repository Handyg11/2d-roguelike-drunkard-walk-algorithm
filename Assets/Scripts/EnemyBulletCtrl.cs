using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletCtrl : MonoBehaviour
{
	public float speed;
    private Vector2 target;
    private Transform player;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    void Update(){
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(transform.position.x == target.x && transform.position.y == target.y){
			Destroy(gameObject);
		}
    }
    void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Player")){
            SoundManager.PlaySound("playerHit");
            if(StatsDisplayManager.levelCounter >=6 && StatsDisplayManager.levelCounter <=10){
                StatsDisplayManager.healthAmmount-=3;
                Destroy(gameObject);
            }else if(StatsDisplayManager.levelCounter >=11){
                StatsDisplayManager.healthAmmount-=4;
                Destroy(gameObject);
            }else{
                StatsDisplayManager.healthAmmount-=2;
                Destroy(gameObject);
            }
        }
        if(col.CompareTag("Wall")){
            Destroy(gameObject);
        }
        // if(col.CompareTag("Enemy")){
        //     Destroy(gameObject);
        // }
    }
}

