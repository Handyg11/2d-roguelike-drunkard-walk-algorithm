using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip  playerShootSound, playerHitSound, playerDeathSound, enemyWalkSound, enemyShootSound, enemyHitSound, enemyDeathSound, bossShootSound, bossHitSound, bossDeathSound, emptyMag, health, ammoPicked, bulletDestroyed;
    static AudioSource asr;

    void Start(){
        playerShootSound = Resources.Load<AudioClip>("playerShoot");
        playerHitSound = Resources.Load<AudioClip>("playerHit");
        playerDeathSound = Resources.Load<AudioClip>("playerDeath");

        enemyWalkSound = Resources.Load<AudioClip>("enemyStepfloor");
        enemyShootSound = Resources.Load<AudioClip>("enemyShoot");
        enemyHitSound = Resources.Load<AudioClip>("enemyHit");
        enemyDeathSound = Resources.Load<AudioClip>("enemyDeath");

        bossShootSound = Resources.Load<AudioClip>("bossShoot");
        bossHitSound = Resources.Load<AudioClip>("bossHit");
        bossDeathSound = Resources.Load<AudioClip>("bossDeath");

        emptyMag = Resources.Load<AudioClip>("emptyMag");

        health = Resources.Load<AudioClip>("health");
        ammoPicked = Resources.Load<AudioClip>("ammo");
        bulletDestroyed = Resources.Load<AudioClip>("bulletDestroyed");

        asr = GetComponent<AudioSource>();
    }

    void Update(){

    }
    public static void PlaySound(string clip){
        switch(clip){
            case "playerShoot":
                asr.PlayOneShot(playerShootSound);
                break;
            case "playerHit":
                asr.PlayOneShot(playerHitSound);
                break;
            case "playerDeath":
                asr.PlayOneShot(playerDeathSound);
                break;
            case "enemyStepfloor":
                asr.PlayOneShot(enemyWalkSound);
                break;
            case "enemyShoot":
                asr.PlayOneShot(enemyShootSound);
                break;
            case "enemyHit":
                asr.PlayOneShot(enemyHitSound);
                break;
            case "enemyDeath":
                asr.PlayOneShot(enemyDeathSound);
                break;
            case "bossShoot":
                asr.PlayOneShot(bossShootSound);
                break;
            case "bossHit":
                asr.PlayOneShot(bossHitSound);
                break;
            case "bossDeath":
                asr.PlayOneShot(bossDeathSound);
                break;
            case "emptyMag":
                asr.PlayOneShot(emptyMag);
                break;
            case "health":
                asr.PlayOneShot(health);
                break;
            case "ammo":
                asr.PlayOneShot(ammoPicked);
                break;
            case "bulletDestroyed":
                asr.PlayOneShot(bulletDestroyed);
                break;
    }
    }
}
