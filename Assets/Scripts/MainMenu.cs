using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject menu, creditText;
    public Slider slider;
    public Text progressText;
    // Start is called before the first frame update
    void Start()
    {
        AudioListener.pause = false;
        Time.timeScale = 1f;
        menu.SetActive(true);
    }

    public void PlayButton(int sceneIndex)
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        menu.SetActive(false);
        SceneManager.LoadScene("CutScene");
        //StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    // IEnumerator LoadAsynchronously(int sceneIndex){
    //     AsyncOperation op = SceneManager.LoadSceneAsync(sceneIndex);
    //     loadingScreen.SetActive(true);
    //     while(!op.isDone){
            
    //         float progress= Mathf.Clamp01(op.progress/ .9f);
    //         slider.value = progress;
    //         progressText.text = progress *100f + "%";
    //         yield return new WaitForSeconds(1.0f);
    //     }
    // }
    public void ExitButton()
    {
        menu.SetActive(false);
        creditText.SetActive(true);
    }
    public void Quit(){
        Debug.Log("quit");
        Application.Quit();
    }
    public void SetVolume(float volume){
        AudioListener.volume = volume;
    }

}
