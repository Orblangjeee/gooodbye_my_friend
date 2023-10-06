using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EndRunGM : MonoBehaviour
{
    #region instance
    public static EndRunGM instance;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        
    }
    #endregion

    public delegate void OnPlay(bool isplay);
    public OnPlay onPlay;

    public float gameSpeed = 1;
    public Vector3 StartPosition;
    public bool isPlay = false;

    public GameObject playBtn;
    public GameObject Walk;

    

    public Text scoreTxt;
    public int score = 0;

    public TextMeshProUGUI explain;
    public TextMeshProUGUI success;
    public TextMeshProUGUI fail;

    IEnumerator AddScore()
    {
        while (isPlay)
        {
            if (score < 100)
            {
                score++;
                scoreTxt.text = score.ToString();
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                GameDone();
            }
        }
    }

    private void OnEnable()
    {
        transform.position = StartPosition;
    }

    void Start()
    {
      
        explain.gameObject.SetActive(true);
        fail.gameObject.SetActive(false);
        success.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        if (Walk.gameObject.activeSelf == false)
        {
            isPlay = false;
            StopCoroutine(AddScore());
            score = 0;
            Time.timeScale = 1;
            playBtn.gameObject.SetActive(true);
            explain.gameObject.SetActive(true);
            fail.gameObject.SetActive(false);
            success.gameObject.SetActive(false);
        }


    }

    // Update is called once per frame
    void Update()
    {
        

        
    }

  


    public void PlayBtnClick()
    {
        Time.timeScale = 1;
        fail.gameObject.SetActive(false);
        success.gameObject.SetActive(false);
        explain.gameObject.SetActive(false);
        playBtn.SetActive(false);
        isPlay = true;
        onPlay.Invoke(isPlay);
        score = 0;
        scoreTxt.text = score.ToString();
        StartCoroutine(AddScore());
        
    }

    public void GameOver()
    {
        
        fail.gameObject.SetActive(true);
        
        isPlay = false;
        onPlay.Invoke(isPlay);
        StopCoroutine(AddScore());
    }

    public void GameDone()
    {
       
        success.gameObject.SetActive(true);
        
        isPlay = false;
        onPlay.Invoke(isPlay);
        StopCoroutine(AddScore());
        DataManager.instance.nowPlayer.money += 100;
    }

    public void End()
    {
        Time.timeScale = 1;
    }
}
