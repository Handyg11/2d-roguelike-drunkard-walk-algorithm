using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
	public float speed;
    private Rigidbody2D rb;
    private Vector2 moveSpeed;

    public GameObject shoot;
    private Transform playerPos;
    
    public static bool GameIsFreeze = false;

    // Animation
    private Animator anim;

    //PlayerPrefs
    public bool isFiring;
    public bool isPause;
    static AudioSource asrc;

    public Transform playerGun, gunTip, torch;
    public SpriteRenderer gunRender, playerRender;
    //MouseInput
    Vector3 mousePos, mouseVector;
	CameraFollow Cam;
    
    void Start()
    {   
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerPos = GetComponent<Transform>();
        asrc = GetComponent<AudioSource>();
        Cam = FindObjectOfType<CameraFollow>();
        playerRender = GetComponent<SpriteRenderer>();
        startLevel();
    }

    void Update()
    {
        if(!isPause){
            AudioListener.pause = false;
            Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            moveSpeed = movement.normalized * speed;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(StatsDisplayManager.foodAmmount <= 12){
                torch.localScale = new Vector3(2f,2f,2f);
                speed = 3;
            }else if(StatsDisplayManager.foodAmmount <= 5){
                speed = 2;
            }else {
                speed = 4;
            }

            if(StatsDisplayManager.healthAmmount <= 4){
                torch.localScale = new Vector3(2f,2f,2f);
            }else {
                torch.localScale = new Vector3(3f,3f,3f);
            }
            mousePos.z = transform.position.z;
            mouseVector = (mousePos - transform.position).normalized;
            float angle = Mathf.Atan2(mouseVector.y, mouseVector.x) * Mathf.Rad2Deg;
            playerGun.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            if (angle < 90 && angle > -90){
                playerRender.flipX = false;
                gunRender.flipY = false;
            }else{
                playerRender.flipX = true;
                gunRender.flipY = true;
            }
            
            if(movement.magnitude == 0){
                asrc.Stop();
                anim.SetBool("isWalking", false);
            }
            else
            {
                if(!asrc.isPlaying)
                asrc.Play();
                anim.SetBool("isWalking",true);
            }
        }else{
            AudioListener.pause = true;
            Debug.Log("isPause");
        }
        if(StatsDisplayManager.healthAmmount <= 0){
                gameOver();
                isPause = true;
                AudioListener.pause = false;
                asrc.Stop();
        }

        if(Input.GetMouseButtonDown(0) && !isFiring && StatsDisplayManager.ammoAmmount > 0 && !isPause){
                SoundManager.PlaySound("playerShoot");
                isFiring = true;
                StatsDisplayManager.ammoAmmount--;
                //bullet
                Instantiate(shoot,gunTip.position,Quaternion.identity);
                Cam.Shake((transform.position - gunTip.position).normalized, 1.5f, 0.05f);
                isFiring = false;
        }else if(Input.GetMouseButtonDown(0) && !isFiring && StatsDisplayManager.ammoAmmount <= 0 && !isPause){
                SoundManager.PlaySound("emptyMag");
        }
        else{
                //Debug.Log("pause");
        }
    }
    public void startLevel(){
        Time.timeScale = 1f;
        GameIsFreeze = false;
    }
    public void gameOver(){
        Time.timeScale = 0f;
        GameIsFreeze = true;
        
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime);
    }

}
