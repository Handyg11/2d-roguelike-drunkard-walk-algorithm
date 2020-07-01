using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestarterSlowGen : MonoBehaviour
{
	void Update(){
		if (Input.GetKeyDown("space")){
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		if (Input.GetKeyDown("escape")){
			Application.Quit();
		}
	}
}
