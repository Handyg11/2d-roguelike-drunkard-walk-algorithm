using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour {

	private Vector2 target;
	public float speed;

	void Start(){
		target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}
	void FixedUpdate(){
		transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

		if(Vector2.Distance(transform.position, target) < 0.2f){
			Destroy(gameObject);
		}
	}
	void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Wall")){
			SoundManager.PlaySound("bulletDestroyed");
            Destroy(gameObject);
        }
    }
	
}
