using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseSMask : MonoBehaviour
{
  [Range(0.05f,0.2f)]
    public float flicTime;


    [Range(0.02f, 0.09f)]
    public float addSize;

    float timer = 0;
    private bool bigger = true;
    private Vector3 mousePos, offset;

    public PlayerMove pm;
    void Start(){
        offset = new Vector3(0,0,3.0f);
    }
    public void Update()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        if(!pm.isPause){
                  timer += Time.deltaTime;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (timer > flicTime)
        {
            if (bigger)
            {
            transform.localScale = new Vector3(transform.localScale.x + addSize, transform.localScale.y + addSize, transform.localScale.z);
            }
            else
            {
            transform.localScale = new Vector3(transform.localScale.x - addSize, transform.localScale.y - addSize, transform.localScale.z);
            }

            timer = 0;
            bigger = !bigger;

        }
        if (mousePos!=null)
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
        else{ 
            Debug.Log("mousepaused");
        }
  
    }
}
