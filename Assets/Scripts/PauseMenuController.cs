using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public static bool GameIsPaused = false;

	public GameObject pauseMenuUI;
	private StatsDisplayManager sdm;
	public PlayerMove pm;
	// public CameraFollow cmf;
	// public CameraFollowBoss cmfb;

	public GameObject gameOverUI;
	static AudioSource asrc;

	void Start(){
		sdm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StatsDisplayManager>();
		asrc = GetComponent<AudioSource>();
	}

	void Update(){
		pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
		// if(StatsDisplayManager.levelCounter == 5 ||StatsDisplayManager.levelCounter ==10 ||StatsDisplayManager.levelCounter == 15){
		// 	cmfb = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollowBoss>();
		// }else{
		// 	cmf = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
		// }
		if(Input.GetKeyDown(KeyCode.Escape)){
			sdm.displayStats.SetActive(false);
			if(GameIsPaused){
				Resume();
			}
			else{
				Pause();
			}
		}
		if(StatsDisplayManager.healthAmmount <= 0){
			sdm.displayStats.SetActive(false);
			gameOverUI.SetActive(true);
			asrc.Pause();
			pm.isPause = true;
		}
	}
	public void Resume(){
		asrc.Play();
		pauseMenuUI.SetActive(false);
		sdm.displayStats.SetActive(true);
		Time.timeScale = 1f;
		pm.isPause = false;
		GameIsPaused = false;
	}void Pause(){
		asrc.Pause();
		pauseMenuUI.SetActive(true);
		pm.isPause = true;
		Time.timeScale = 0f;
		GameIsPaused = true;
	}
	
	public void Retry(){
		Reset();
		Time.timeScale = 1f;
		GameIsPaused = false;
		SceneManager.LoadScene("SplashLevel");
	}
	public void ReturnMenu(){
		Reset();
		Debug.Log(StatsDisplayManager.ammoAmmount);
		Time.timeScale = 1f;
		GameIsPaused = false;
		AudioListener.pause = false;
		SceneManager.LoadScene("Main");
	}

	void Reset(){
		StatsDisplayManager.healthAmmount = sdm.healthReset;
		StatsDisplayManager.ammoAmmount = sdm.ammoReset;
		StatsDisplayManager.foodAmmount = sdm.foodReset;
		StatsDisplayManager.levelCounter = sdm.levelReset;
	}
}
