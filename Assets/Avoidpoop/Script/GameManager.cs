using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance; //싱글톤
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }


    [SerializeField]
    private GameObject poop;
   
    public int score;

    public bool stopTrigger = true;

    [SerializeField]
    private Text scoreTxt;
    [SerializeField]
    private Text endscore;
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private Text moneyTxt;
   
    
    void Start()
    {
        GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        Time.timeScale = 1;
        score = 0;
        stopTrigger = true;
        StartCoroutine(CreatepoopRoutine());
        panel.SetActive(false);
    }

    public void GameEnd()
    {
        DataManager.instance.nowPlayer.money += score;
        Time.timeScale = 0;
        stopTrigger = false;
        StopCoroutine(CreatepoopRoutine());
        panel.SetActive(true);
        
    }



    public void Score()
    {
        score++;
        scoreTxt.text = "Score : " + score;
        endscore.text = "Score : " + score;
        
        
    }
    IEnumerator CreatepoopRoutine()
    {
        
        while (stopTrigger)
        {
            CreatePoop();
            yield return new WaitForSeconds(1);
        }
        
    }
    
    private void CreatePoop()
    {
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(0.0f,1.0f), 1.1f, 0)); //메인카메라 뷰 안에서만 생성
        pos.z = 0.0f; //게임화면에 보이게 하기
        Instantiate(poop, pos, Quaternion.identity);
    }
}
