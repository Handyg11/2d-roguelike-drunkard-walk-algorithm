using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cutsceneManager : MonoBehaviour
{
    public GameObject panel1,panel2,panel3,panel4,panel5,panel6,finalPanel;
    private StatsDisplayManager sd;
    // Start is called before the first frame update
    void Start()
    {
        sd = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StatsDisplayManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(StatsDisplayManager.levelCounter==1){
            sd.displayStats.SetActive(false);
            panel1.SetActive(true);
            panel2.SetActive(false);
            panel3.SetActive(false);
            panel4.SetActive(false);
            panel5.SetActive(false);
            panel6.SetActive(false);
            finalPanel.SetActive(false);
        }
        else if(StatsDisplayManager.levelCounter==5){
            sd.displayStats.SetActive(false);
            panel1.SetActive(false);
            panel2.SetActive(true);
            panel3.SetActive(false);
            panel4.SetActive(false);
            panel5.SetActive(false);
            panel6.SetActive(false);
            finalPanel.SetActive(false);
        }
        else if(StatsDisplayManager.levelCounter==6){
            sd.displayStats.SetActive(false);
            panel1.SetActive(false);
            panel2.SetActive(false);
            panel3.SetActive(true);
            panel4.SetActive(false);
            panel5.SetActive(false);
            panel6.SetActive(false);
            finalPanel.SetActive(false);
        }else if(StatsDisplayManager.levelCounter==10){
            sd.displayStats.SetActive(false);
            panel1.SetActive(false);
            panel2.SetActive(false);
            panel3.SetActive(false);
            panel4.SetActive(true);
            panel5.SetActive(false);
            panel6.SetActive(false);
            finalPanel.SetActive(false);
        }else if(StatsDisplayManager.levelCounter==11){
            sd.displayStats.SetActive(false);
            panel1.SetActive(false);
            panel2.SetActive(false);
            panel3.SetActive(false);
            panel4.SetActive(false);
            panel5.SetActive(true);
            panel6.SetActive(false);
            finalPanel.SetActive(false);
        }else if(StatsDisplayManager.levelCounter==15){
            sd.displayStats.SetActive(false);
            panel1.SetActive(false);
            panel2.SetActive(false);
            panel3.SetActive(false);
            panel4.SetActive(false);
            panel5.SetActive(false);
            panel6.SetActive(true);
            finalPanel.SetActive(false);
        }
        else if(StatsDisplayManager.levelCounter==16){
            sd.displayStats.SetActive(false);
            panel1.SetActive(false);
            panel2.SetActive(false);
            panel3.SetActive(false);
            panel4.SetActive(false);
            panel5.SetActive(false);
            panel6.SetActive(false);
            finalPanel.SetActive(true);
        }
        else{
            sd.displayStats.SetActive(false);
            panel1.SetActive(false);
            panel2.SetActive(false);
            panel3.SetActive(false);
            panel4.SetActive(false);
            panel5.SetActive(false);
            panel6.SetActive(false);
            finalPanel.SetActive(false);
        }
    }

    public void nextScene(){
        sd.displayStats.SetActive(true);
           SceneManager.LoadScene("SplashLevel");
    }
}
