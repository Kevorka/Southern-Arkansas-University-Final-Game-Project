﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GAMEMANAGER;
using GUN;
using PLAYER;


public class hud : MonoBehaviour {
    public Text timer;
    float countDown=300f;

    //Ammo Display

    public Player playerScript;
    public Text pickUpText;
    public Text yourWeapon;
    GameManager gm;
    public Text ammoDisplay;

    //Items Player Collect
    public Text keyText;
    public Text evidText;

    //Player health bar
    float maxHealth = 100.0f;
    public GameObject needle;
    public float smooth = 2.0f;

    //Gun Icon
    public Image gunIcon;
    public Sprite[] icons;


    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        playerScript = GameObject.FindGameObjectWithTag("player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update () {
        playerScript = GameObject.FindGameObjectWithTag("player").GetComponent<Player>();

        //prints out the timer
        countDown-=Time.deltaTime;
        int sec=(int)countDown%60;
        int min = (int)countDown / 60;
        timer.text=min.ToString()+ ":"+sec.ToString();

        if(countDown <= 0){
            countDown = 300f;
        }

        //updates health meter
        loseHealth();

        //Updates Ammo player has
        if (gm.yourGun != null)
        {
            ShowAmmo();
        }

        //tells which gun you have
       yourWeapon.text = gm.yourGun.CurrentWeapon;

        //figures which icon to use
        if(gm.yourGun.CurrentWeapon == "revolver"){
            gunIcon.sprite = icons[0];
        }
        if (gm.yourGun.CurrentWeapon == "railgun")
        {
            gunIcon.sprite = icons[1];
        }
        if (gm.yourGun.CurrentWeapon == "tommygun")
        {
            gunIcon.sprite = icons[2];
        }
        if (gm.yourGun.CurrentWeapon == "shotgun")
        {
            gunIcon.sprite = icons[3];
        }

    }

    //Health
    void loseHealth(){
        //rotating needle to represent health
        float rotationZ = 270.0f * ((float)playerScript.player_health / maxHealth)-270.0f;
        Quaternion target = Quaternion.Euler(0,0,rotationZ);
        needle.transform.rotation = Quaternion.Slerp(needle.transform.rotation, target, smooth * Time.deltaTime);
    }


    //Ammo
    void ShowAmmo()
    {
       ammoDisplay.text = gm.yourGun.currentAmmo+ "/" + gm.yourGun.AmmoUpdate;

    }
}
