using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class splashScreen : MonoBehaviour
{
    public Text text;


    IEnumerator Start(){
        text.canvasRenderer.SetAlpha(0.0f);
        FadeIn();
        yield return new WaitForSeconds(2.5f);
        FadeOut();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("main");
    }
    void FadeIn()
    {
        text.CrossFadeAlpha(1.0f, 1.5f, false);
    }

    void FadeOut()
    {
        text.CrossFadeAlpha(0.0f, 2.5f, false);
    }
}
