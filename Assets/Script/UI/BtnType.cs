using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BtnType : MonoBehaviour
{
    bool isSound;
    public BTNType currentType;
    public GameObject ExplainPannel;
    public GameObject[] Explainimage;

   public void OnBtnClick()
    {
        switch (currentType)
        {
            case BTNType.New:
                StartSceneLoader.LoadSceneHandle("01.Prologue", 0);
                Debug.Log("새게임");
                break;
            case BTNType.Save:
                DataManager.instance.SaveData();
                Debug.Log("저장하기");
                break;
            case BTNType.Option:
                Time.timeScale = 0;

                break;
            case BTNType.Sound:
                if (isSound)
                {
                    Debug.Log("사운드 OFF");
                }
                else
                {
                    Debug.Log("사운드 ON");
                }
                isSound = !isSound;
                break;
            case BTNType.Back:
                Time.timeScale = 1;
                Debug.Log("이어하기");
                break;
            case BTNType.Quit:
                Application.Quit();
                Debug.Log("앱종료");
                break;
            case BTNType.Exit:
                StartSceneLoader.LoadSceneHandle("00.Start",0);
                Debug.Log("게임종료");
                break;
            case BTNType.Explain:
                ExplainPannel.SetActive(true);
                for(int i =0; i<Explainimage.Length; i++)
                {
                    Explainimage[i].SetActive(true);
                }
                break;
        }
    }

    

   
}
