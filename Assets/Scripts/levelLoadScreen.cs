using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class levelLoadScreen : MonoBehaviour
{
public Text text;
private StatsDisplayManager sd;

    IEnumerator Start(){
        sd = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StatsDisplayManager>();
        sd.displayStats.SetActive(false);
        if(StatsDisplayManager.levelCounter==15){
            text.text = "Our Last Day";
        }else if(StatsDisplayManager.levelCounter==16){
            StatsDisplayManager.healthAmmount = sd.healthReset;
            StatsDisplayManager.ammoAmmount = sd.ammoReset;
            StatsDisplayManager.foodAmmount = sd.foodReset;
            StatsDisplayManager.levelCounter = sd.levelReset;
            SceneManager.LoadScene("Main");
        }
        else{
            text.text = "Day " + StatsDisplayManager.levelCounter.ToString();
        }
        text.canvasRenderer.SetAlpha(0.0f);
        FadeIn();
        yield return new WaitForSeconds(1.5f);
        FadeOut();
        yield return new WaitForSeconds(1.0f);
        if(StatsDisplayManager.levelCounter==1){
				SceneManager.LoadScene("test");
			}
            else if(StatsDisplayManager.levelCounter==5){
				SceneManager.LoadScene("Boss 1");
			}
			else if(StatsDisplayManager.levelCounter==10){
				SceneManager.LoadScene("Boss 2");
			}
			else if(StatsDisplayManager.levelCounter==15){
				SceneManager.LoadScene("Final Boss");
			}
            else if(StatsDisplayManager.levelCounter==16){
			}
			else{
				SceneManager.LoadScene("test");
			};
    }
    void FadeIn()
    {
        text.CrossFadeAlpha(1.0f, 0.5f, false);
    }

    void FadeOut()
    {
        text.CrossFadeAlpha(0.0f, 1.0f, false);
    }
}
