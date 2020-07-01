using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBehavior : MonoBehaviour
{
    public int health;
    public float moveSpeed;
    public float attackRange;
    private float attackTimer;
    public float attackRate;
    public float projectileSpeed;

    public GameObject target;
    public GameObject projPrefab;
    public Rigidbody2D rb;

    private bool hasUpgrade;

    public Transform bossGun;
    public Transform bossGFX;

    private bool getHit;

    public GameObject spawner;
    public Transform spawnArea1,spawnArea2,spawnArea3,spawnArea4,spawnArea5,spawnArea6;

    private Animator animator;

    void Start(){
        animator = GetComponentInChildren<Animator>();
    }
    void Update(){
        attackTimer += Time.deltaTime;
        //mencari target
        if(target != null){
            Vector3 dir = transform.position - target.transform.position;
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            bossGFX.rotation = Quaternion.AngleAxis(0, Vector3.forward);

        //Mengecek target apakah dikejar atau ditembak
            if(Vector3.Distance(transform.position, target.transform.position) > attackRange)
            {
                Chase();
                animator.SetBool("isWalking", true);
            }
            else
            {
                if(attackTimer >= attackRate)
                {
                    attackTimer = 0.0f;
                    ShotBullet();
                    animator.SetBool("isWalking", false);
                }
            }
            if(target.transform.position.x>= 0.01){
                transform.localScale = new Vector3(1f,1f,1f);
            }else if(target.transform.position.x <= -0.01){
                transform.localScale = new Vector3(-1f,1f,1f);
            }

        }
        else{
            rb.simulated = false;
        }
        if(!hasUpgrade){
            if(StatsDisplayManager.levelCounter == 5){
                if(health <= 20){
                    hasUpgrade = true;
                    attackRate = 0.5f;
                    projectileSpeed *= 2f;
                    Instantiate(spawner,spawnArea1.position,Quaternion.identity);
                    Instantiate(spawner,spawnArea2.position,Quaternion.identity);
                    Instantiate(spawner,spawnArea3.position,Quaternion.identity);
                    Instantiate(spawner,spawnArea4.position,Quaternion.identity);
                }
            }
            else if(StatsDisplayManager.levelCounter == 10){
                if(health <= 30){
                    hasUpgrade = true;
                    attackRate = 0.5f;
                    projectileSpeed *= 2f;
                    Instantiate(spawner,spawnArea1.position,Quaternion.identity);
                    Instantiate(spawner,spawnArea2.position,Quaternion.identity);
                    Instantiate(spawner,spawnArea3.position,Quaternion.identity);
                    Instantiate(spawner,spawnArea4.position,Quaternion.identity);
                    Instantiate(spawner,spawnArea5.position,Quaternion.identity);
                }
            }
            else if(StatsDisplayManager.levelCounter == 15){
                if(health <= 40){
                    hasUpgrade = true;
                    attackRate = 0.3f;
                    projectileSpeed *= 2f;
                    Instantiate(spawner,spawnArea1.position,Quaternion.identity);
                    Instantiate(spawner,spawnArea2.position,Quaternion.identity);
                    Instantiate(spawner,spawnArea3.position,Quaternion.identity);
                    Instantiate(spawner,spawnArea4.position,Quaternion.identity);
                    Instantiate(spawner,spawnArea5.position,Quaternion.identity);
                    Instantiate(spawner,spawnArea6.position,Quaternion.identity);
                }
            }
        }

    }

    void Chase(){
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
    }
    void ShotBullet(){
        SoundManager.PlaySound("enemyShoot");
        Instantiate(projPrefab, bossGun.position, transform.rotation);
        Debug.Log("shoot");
    }

    void OnTriggerEnter2D(Collider2D other){
            if(other.CompareTag("Player")){
                health-=3;
                StatsDisplayManager.healthAmmount-=2;
                StatsDisplayManager.foodAmmount -=3;
                if(health <= 0){
                    SoundManager.PlaySound("bossDeath");
                    Destroy(gameObject);
                    bossDied();
                }
            }
            if(other.CompareTag("Bullet") && !getHit){
                health--;
                SoundManager.PlaySound("bossHit");
                //Debug.Log(lg.enemyCount);
                Destroy(other.gameObject);
                if(health <= 0){
                    getHit = true;
                    SoundManager.PlaySound("bossDeath");
                    Debug.Log("dead");
                    Destroy(gameObject);
                    bossDied();
                }
            }
        }

        void bossDied(){
            StatsDisplayManager.levelCounter++;
			SceneManager.LoadScene("CutScene");
            Debug.Log("SplashLevel");
    }
}
