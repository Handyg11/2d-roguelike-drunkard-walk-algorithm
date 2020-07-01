using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyCtrl : MonoBehaviour
{
    public float speed = 2f;
    private Transform playerPos;

    //private GameManagerScript gameManager;
    //private LevelGenerator lg;

    Rigidbody2D rb;
    bool follow = false;

    Vector3 directionToTarget;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //nantyi ganti tag Player di GMS jadi game manager
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        //gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        //lg = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform ngebug
        if (follow)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position,speed * Time.deltaTime);
            // directionToTarget = (playerPos.transform.position - transform.position).normalized;
            // rb.velocity = new Vector2(directionToTarget.x * speed, directionToTarget.y * speed);
        }
        else
        {
            rb.velocity = Vector2.zero ;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")){
            follow = true;
        }
        else
        {
            follow = false;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        follow = false;

    }
}
