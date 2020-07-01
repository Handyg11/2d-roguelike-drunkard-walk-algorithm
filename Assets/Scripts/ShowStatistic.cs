using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStatistic : MonoBehaviour
{
    public Text enemyKilledAmmount;
    public int killedReset = 0; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyKilledAmmount.text = "Enemy Killed: " + StatsDisplayManager.enemyKilled.ToString();
    }

    public void ResetButton(){
        StatsDisplayManager.enemyKilled = killedReset;
    }
}
