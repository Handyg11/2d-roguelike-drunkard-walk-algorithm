using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootCtrl : MonoBehaviour
{
    public Transform enemyGun, enemyGFX, gunRotate;

    public SpriteRenderer gunRend;
    public GameObject projPrefab;

    private Transform target;

    private float attackTimer;
    public float attackRate;
    float nextFire;


    public float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        nextFire = Time.time;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Enemy 2 problem shoot
    }
    void OnTriggerStay2D(Collider2D cl){
        attackTimer += Time.deltaTime;
        Vector3 dir = transform.position - target.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180;
        gunRotate.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if(angle < 90 || angle > -270 && angle > 270 || angle<-90){
            gunRend.flipY = false;
        }else if ( angle > 90 || angle < -270 && angle < 270 || angle > -90){
            gunRend.flipY = true;
        }
        enemyGFX.rotation = Quaternion.AngleAxis(0, Vector3.forward);
        if(cl.CompareTag("Player")){
            if(attackTimer>= attackRate){
                attackTimer = 0.0f;
                SoundManager.PlaySound("enemyShoot");
                Instantiate(projPrefab, enemyGun.position, transform.rotation);
                Debug.Log("shoot");
            }
        }
	}

}
