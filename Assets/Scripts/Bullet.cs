using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	float moveSpeed = 7f;

	Rigidbody2D rb;

	private Transform target;
	Vector2 moveDirection;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		target = GameObject.FindGameObjectWithTag("Player").transform;
		moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
		rb.velocity = new Vector2 (moveDirection.x, moveDirection.y);
		Destroy (gameObject, 3f);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if(col.CompareTag("Player")){
            StatsDisplayManager.healthAmmount-=2;
            Destroy(gameObject);
        }
        // if(col.CompareTag("Enemy")){
        //     Destroy(gameObject);
        // }
	}

}
