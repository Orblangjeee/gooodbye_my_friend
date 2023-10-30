using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


public class PlayerData
{   //초기값
    public float maxHeart = 100;
    public float curHeart = 0;
    public float maxStress = 50;
    public float curStress = 10;
    public float maxHungry = 100;
    public float curHungry = 50;

    public int day = 1;
    public int feedClick = 2;
    public int gotchaClick = 1;
    public float walkstress = 10;
    public float playheart = 5;
    public int money = 10000;
    public int storyScore = 0;

    public int Day1_chk = 0;
    public int Day2_chk = 0;
    public int Day3_chk = 0;
    public int Day4_chk = 0;
    public int Day5_chk = 0;
    public int Day6_chk = 0;
    public int Day7_chk = 0;
    public int Day8_chk = 0;
    public int Day9_chk = 0;
    public int Day9_sty = 0;
    public int Day10_chk = 0;
    public int Day11_chk = 0;
    public int Day12_chk = 0;

    public bool Day1_note = false;
    public bool Day2_note = false;
    public bool Day3_note = false;
    public bool Day4_note = false;
    public bool Day5_note = false;
    public bool Day6_note = false;
    public bool Day7_note = false;
    public bool Day8_note = false;
    public bool Day10_note = false;

    public string date = "none";
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance; //싱글톤

    public PlayerData nowPlayer = new PlayerData();

    public string path;
    public int nowSlot;

    private void Awake()
    {
        #region 싱글톤
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        #endregion
        

        path = Application.persistentDataPath + "/MySaveData";
        print(path);
       
    }

    void Start()
    {
        Character.Instance.character_anim.SetBool("day7", false);
        Character.Instance.character_anim.SetBool("day10", false);
    }

    public void SaveData()
    {
        nowPlayer.date = DateTime.Now.ToString();
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path + nowSlot.ToString() + ".txt", data);
        
    }


    public void LoadData()
    {
       string data = File.ReadAllText(path+ nowSlot.ToString()+".txt");
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
       
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }

    

    public void WhatDay()
    {
        switch (nowPlayer.day)
        {
            case 1:
                nowPlayer.Day1_chk += 1;
                
                break;
            case 2:
                nowPlayer.Day1_chk += 2;
                nowPlayer.Day2_chk += 1;
                nowPlayer.curStress += 20;
                break;
            case 3:
                nowPlayer.Day2_chk += 2;
                nowPlayer.Day3_chk += 1;
                nowPlayer.curStress += 30;
                break;
            case 4:
                nowPlayer.Day3_chk += 2;
                nowPlayer.Day4_chk += 1;
                nowPlayer.curStress += 30;
                break;
            case 5:
                nowPlayer.Day4_chk += 2;
                nowPlayer.Day5_chk += 1;
                nowPlayer.curStress += 40;
                break;
            case 6:
                nowPlayer.Day5_chk += 2;
                nowPlayer.Day6_chk += 1;
                nowPlayer.curStress += 40;
                break;
            case 7:
                nowPlayer.Day6_chk += 2;
                nowPlayer.Day7_chk += 1;
                nowPlayer.curStress = 50;
                break;
            case 8:
                nowPlayer.Day7_chk += 2;
                nowPlayer.Day8_chk += 1;
                nowPlayer.curStress += 40;
                print(nowPlayer.day.ToString() + nowPlayer.Day8_chk.ToString() + nowPlayer.Day9_chk);
                break;
            case 9:
               
                nowPlayer.Day8_chk += 2;
                nowPlayer.Day9_chk += 1;
                print(nowPlayer.day.ToString() + nowPlayer.Day8_chk.ToString() + nowPlayer.Day9_chk);
                break;
            case 10:
              
                nowPlayer.Day9_chk += 2;
                nowPlayer.Day10_chk += 1;
                break;
            case 11:
             
                nowPlayer.Day10_chk += 2;
                nowPlayer.Day11_chk += 1;
                break;
            case 12:
                nowPlayer.Day11_chk += 2;
                nowPlayer.Day12_chk += 1;
                break;
        }

        
    }

    
}
