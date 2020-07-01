using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour {
	private StatsDisplayManager sdm;
	int healthTmp, ammoTmp, foodTmp;
	void Start(){
		sdm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StatsDisplayManager>();
	}
	void Update(){
		if (Input.GetKeyDown("space")){
			// SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			StatsDisplayManager.levelCounter++;
			// if(StatsDisplayManager.levelCounter==1){
			// 	SceneManager.LoadScene("test");
			// }
			 if(StatsDisplayManager.levelCounter==5){
				SceneManager.LoadScene("CutScene");
			}
			else if(StatsDisplayManager.levelCounter==6){
				SceneManager.LoadScene("CutScene");
			}
			else if(StatsDisplayManager.levelCounter==10){
				SceneManager.LoadScene("CutScene");
			}
			else if(StatsDisplayManager.levelCounter==11){
				SceneManager.LoadScene("CutScene");
			}
			else if(StatsDisplayManager.levelCounter==15){
				SceneManager.LoadScene("CutScene");
			}
			else if(StatsDisplayManager.levelCounter==16){
				SceneManager.LoadScene("CutScene");
			}
			else{
				SceneManager.LoadScene("SplashLevel");
			};
			healthTmp = StatsDisplayManager.healthAmmount;
			ammoTmp = StatsDisplayManager.ammoAmmount;
			foodTmp = StatsDisplayManager.foodAmmount;
		}
		if (Input.GetKeyDown("escape")){
			Application.Quit();
		}
	}
	void Reset(){
		StatsDisplayManager.healthAmmount = sdm.healthReset;
		StatsDisplayManager.ammoAmmount = sdm.ammoReset;
		StatsDisplayManager.foodAmmount = sdm.foodReset;
		StatsDisplayManager.levelCounter = sdm.levelReset;
	}
}
