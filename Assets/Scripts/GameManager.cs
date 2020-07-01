using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public LevelGenerator lgScript;
    public MainMenu menu;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        lgScript = GetComponent<LevelGenerator>();
    }

    void InitGame(){
       lgScript.StartUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
