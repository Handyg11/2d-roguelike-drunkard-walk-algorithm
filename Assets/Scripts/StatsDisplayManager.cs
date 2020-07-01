using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplayManager : MonoBehaviour
{
    //teks
    public Text ammo,health,food;
    //jumlah stats player
    public static int healthAmmount= 10;
    public static int ammoAmmount = 150; 
    public static int foodAmmount = 20;

    //Level Manager
    public static int levelCounter = 1;

    //buat main menu untuk menghitung jumlah musuh yang dihancurkan
    public static int enemyKilled = 0;
    //Reset Stuffs
    public int levelReset = 1;
    public int healthReset = 10;
    public int ammoReset = 150;
    public int foodReset = 20;

    public GameObject displayStats;

    void Start(){
        health.text = PlayerPrefs.GetInt("Health",10).ToString();
        ammo.text = PlayerPrefs.GetInt("Ammo",100).ToString();
        food.text = PlayerPrefs.GetInt("Food",20).ToString();
    }
    public void Update(){
        health.text = "Health: " + healthAmmount.ToString();
        ammo.text = "Ammo: " + ammoAmmount.ToString();
        food.text = "Food: " + foodAmmount.ToString();
        
        PlayerPrefs.SetInt("Health", healthAmmount);
        PlayerPrefs.SetInt("Ammo", ammoAmmount);
        PlayerPrefs.SetInt("Food", foodAmmount);
        PlayerPrefs.Save();   
    }
}
