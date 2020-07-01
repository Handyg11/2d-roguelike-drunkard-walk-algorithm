using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
// Background scroll speed can be set in Inspector with slider
	[Range(2f, 10f)]
	public float scrollSpeed = 20f;

	// Scroll offset value to smoothly repeat backgrounds movement
	public float scrollOffset;

	// Start position of background movement
	Vector3 startPos;

	// Backgrounds new position
	float newPos;

	// Use this for initialization
	void Start () {
		// Getting backgrounds start position
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		// Calculating new backgrounds position repeating it depending on scrollOffset
		newPos = Mathf.Repeat (Time.time * - scrollSpeed, scrollOffset);

		// Setting new position
		transform.position = startPos + Vector3.right * newPos;
	}
}
