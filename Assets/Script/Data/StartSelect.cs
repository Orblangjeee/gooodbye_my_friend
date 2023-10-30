using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
using System;

public class StartSelect : MonoBehaviour
{
    public GameObject creat;
    public TextMeshProUGUI[] slotText;
    public string loadscene;
    public string newscene;
    bool[] savefile = new bool[3];
    // Start is called before the first frame update
    void Start()
    {
        //슬롯별로 저장된 데이터가 존재하는지 판단
        for (int i= 0; i < 3; i++) 
        {
            if (File.Exists(DataManager.instance.path + $"{i}" + ".txt"))
            {
                savefile[i] = true;
                DataManager.instance.nowSlot = i;
                DataManager.instance.LoadData();
                slotText[i].text = DataManager.instance.nowPlayer.day + " 일차\n" + DataManager.instance.nowPlayer.date;
            }
            else
            {

                slotText[i].text = "비어있음";
            }
        }
       
    }


    public void SaveSlot(int number)
    {
        DataManager.instance.nowSlot = number;

        if (savefile[number])  //2. 저장된 데이터가 있을 때 -> 불러오기해서 게임씬으로 넘어감
        {
            DataManager.instance.LoadData();
            GoGame();
        }
        else //1. 저장된 데이터가 없을 때
        {
            Creat();
        }
        

    }

    public void Creat()
    {
        creat.gameObject.SetActive(true);
    }

    public void GoGame()
    {
        if (!savefile[DataManager.instance.nowSlot]) // 현재 슬롯번호의 데이터가 없다면
        {
            DataManager.instance.SaveData();  // 현재 정보를 저장함.
            SceneManager.LoadScene(newscene);
        }
        else
        {
            SceneManager.LoadScene(loadscene);
        }
        
    }

}
