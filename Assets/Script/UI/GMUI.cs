using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GMUI : MonoBehaviour
{
   

    public Slider heart, stress, hungry;

    public TextMeshProUGUI dayText, heartText, stressText, hungryText, moenyText;

    public Button feed, walk, walk_A, play, gotcha;

    public GameObject food_full, food_no, GotCha;
    public GameObject Character_obj;
    public GameObject DayStoryPanel, fadeIn, storyfade;
    public GameObject[] StoryDialogue;
    public Image portraitmask;

    public Image[] day_note;
    public Sprite[] portrait;
    

    
    void Start()
    {
        
        Button btn = GetComponent<Button>();

       // DataManager.instance.WhatDay();
        food_no.gameObject.SetActive(true);
        food_full.gameObject.SetActive(false);

        feed.interactable = true;
        walk.interactable = true;
        play.interactable = true;
        gotcha.interactable = true;



        heart.value = (float)DataManager.instance.nowPlayer.curHeart / (float)DataManager.instance.nowPlayer.maxHeart;
        fadeIn.SetActive(false);
        storyfade.SetActive(false);
        DayChk();
        Note();
    }


    void Awake()
    {
        DataManager.instance.WhatDay();
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(2f);
        DayChk();
    }
    
  
    void Update()
    {
       
        //money 관리
        moenyText.text = (int)DataManager.instance.nowPlayer.money + " 원";

        //Heart 관리
        heartText.text = (int)DataManager.instance.nowPlayer.curHeart + "/" + (int)DataManager.instance.nowPlayer.maxHeart;

        //Hungry 관리
        hungryText.text = (int)DataManager.instance.nowPlayer.curHungry + "/" + (int)DataManager.instance.nowPlayer.maxHungry;

        //Stress 관리
        stressText.text = (int)DataManager.instance.nowPlayer.curStress + "/" + (int)DataManager.instance.nowPlayer.maxStress;
        

        Bar();

        //레벨(데이)관리
        dayText.text = DataManager.instance.nowPlayer.day.ToString();
        DaySystem();

        //모든 능력치 0 이하가 되면 0으로 초기화
        if (DataManager.instance.nowPlayer.curStress <= 0)
        {
            DataManager.instance.nowPlayer.curStress = 0;
        }
        if (DataManager.instance.nowPlayer.curHungry <= 0)
        {
            DataManager.instance.nowPlayer.curHungry = 0;
        }
        if (DataManager.instance.nowPlayer.curHeart <= 0)
        {
            DataManager.instance.nowPlayer.curHeart = 0;
        }
        //모든 능력치가 max 초과면 max로 초기화
        if (DataManager.instance.nowPlayer.curStress > DataManager.instance.nowPlayer.maxStress)
        {
            DataManager.instance.nowPlayer.curStress =  DataManager.instance.nowPlayer.maxStress;
        }
        if (DataManager.instance.nowPlayer.curHungry > DataManager.instance.nowPlayer.maxHungry)
        {
            DataManager.instance.nowPlayer.curHungry = DataManager.instance.nowPlayer.maxHungry;
        }
        if (DataManager.instance.nowPlayer.curHeart > DataManager.instance.nowPlayer.maxHeart)
        {
            DataManager.instance.nowPlayer.curHeart = DataManager.instance.nowPlayer.maxHeart;
        }

        if (DataManager.instance.nowPlayer.curHungry >= 10) //배고픔 10 이상이면 산책가기 버튼 활성화
        {
            walk.interactable = true;
            walk_A.interactable = true;
        }
        if (DataManager.instance.nowPlayer.curHungry >= 5) //배고픔 5 이상이면 놀아주기 버튼 활성화
        {
            play.interactable = true;
        }
        if (DataManager.instance.nowPlayer.feedClick == 0) //밥주기 버튼 비활성화
        {
            feed.interactable = false;
        }
        if (DataManager.instance.nowPlayer.curHungry < 10) //산책하기 버튼 비활성화
        {
            walk.interactable = false;
            walk_A.interactable = false;
        }
        if (DataManager.instance.nowPlayer.curHungry < 5) //놀아주기 버튼 비활성화
        {
            play.interactable = false;
        }
        if (DataManager.instance.nowPlayer.gotchaClick == 0) //갓챠 클릭 버튼 비활성화
        {
            gotcha.interactable = false;
        }

        if ( DataManager.instance.nowPlayer.day >= 7) //초상화와 캐릭터 변경
        {
            if (DataManager.instance.nowPlayer.day >= 10)
            {
                Character.Instance.character_anim.SetBool("day10", true);
                portraitmask.sprite = portrait[1]; //초상화 변경
            }
            else
            {
                Character.Instance.character_anim.SetBool("day7", true);
                portraitmask.sprite = portrait[0]; //초상화 변경
            }
           
        }
    }
    public GameObject MaxstressWarn;
    public void Feed()  //밥주기
    {
        if(DataManager.instance.nowPlayer.curStress != DataManager.instance.nowPlayer.maxStress)
        {
            if (DataManager.instance.nowPlayer.feedClick >= 1)
            {
                if (DataManager.instance.nowPlayer.curHungry < DataManager.instance.nowPlayer.maxHungry)
                {
                    
                   Character.Instance.character_anim.SetTrigger("Feed");
                    
                    
                    food_no.gameObject.SetActive(false);

                    DataManager.instance.nowPlayer.curHungry += 20;
                    DataManager.instance.nowPlayer.curHeart += 20;
                    DataManager.instance.nowPlayer.feedClick -= 1;
                }
            }
            else
            {
                DataManager.instance.nowPlayer.feedClick = 0;
            }

        }
        if (DataManager.instance.nowPlayer.curStress == DataManager.instance.nowPlayer.maxStress)
        {
            MaxstressWarn.SetActive(true);
            feed.interactable = false;
        }

    }

    public void GotChaDay()
    {
        DataManager.instance.nowPlayer.gotchaClick -= 1;
        
    }

    private void Bar()
    {
        heart.value = Mathf.Lerp(heart.value, (float)DataManager.instance.nowPlayer.curHeart / (float)DataManager.instance.nowPlayer.maxHeart, Time.deltaTime * 10); //Mathf를 사용하여 부드럽게 증감
        stress.value = Mathf.Lerp(stress.value, (float)DataManager.instance.nowPlayer.curStress / (float)DataManager.instance.nowPlayer.maxStress, Time.deltaTime * 10);
        hungry.value = Mathf.Lerp(hungry.value, (float)DataManager.instance.nowPlayer.curHungry / (float)DataManager.instance.nowPlayer.maxHungry, Time.deltaTime * 10);
    }

    public void Walk()
    {
        if (DataManager.instance.nowPlayer.curHungry >= 10)
        {
            DataManager.instance.nowPlayer.curHungry -= 10;
           // DataManager.instance.nowPlayer.curStress -= DataManager.instance.nowPlayer.walkstress;
        }
        else if (DataManager.instance.nowPlayer.curHungry < 10)
        {
            
        }

    }
    public GameObject love;
    public GameObject love2;
    public GameObject love3;
    IEnumerator FloatingLove()
    {
        Instantiate(love);
        yield return new WaitForSeconds(0.2f);
        Instantiate(love2);
        yield return new WaitForSeconds(0.4f);
        Instantiate(love3);
    }

    public void Play()
    {
        
        if (DataManager.instance.nowPlayer.curHungry >= 5)
        {
            StartCoroutine(FloatingLove());
            DataManager.instance.nowPlayer.curHungry -= 5;
            DataManager.instance.nowPlayer.curStress -= 5;
            DataManager.instance.nowPlayer.curHeart += DataManager.instance.nowPlayer.playheart;

        }
    }

    public Image[] minigame;

    public void PointerEnterColor(int i) 
    {
       minigame[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public  void PointerExitColor(int i)
    {
        minigame[i].GetComponent<Image>().color = new Color32(145, 145, 145, 255);
    }

    public void Minigame_poop()
    {
        SceneManager.LoadScene("05.Minigame_avoidpoop");
    }

    public void DaySystem()
    {
        if (DataManager.instance.nowPlayer.curHeart >= DataManager.instance.nowPlayer.maxHeart)
        {
            DataManager.instance.nowPlayer.day += 1;
            feed.interactable = true;
            gotcha.interactable = true;
            DataManager.instance.nowPlayer.feedClick = 2;
            DataManager.instance.nowPlayer.gotchaClick = 1;
            DataManager.instance.nowPlayer.curHeart = 0;
            DataManager.instance.WhatDay();
            DayChk();
        }
        if (DataManager.instance.nowPlayer.day <= 4) //day4 이상부터 갓챠 활성화
        {
            GotCha.SetActive(false);
        }
        else
        {
            GotCha.SetActive(true);
        }
        
    }

    public Sprite[] dayimg;
    public GameObject[] notebtn;
  
    
    public void DayChk()
    {
        if (DataManager.instance.nowPlayer.Day1_chk ==1)
        {
            DayStoryPanel.gameObject.SetActive(true);
            StoryDialogue[0].gameObject.SetActive(true);
            DataManager.instance.nowPlayer.Day1_note = true;
            Note();
        }
        if (DataManager.instance.nowPlayer.Day2_chk ==1)
        {
            StoryDialogue[1].gameObject.SetActive(true);
            DayStoryPanel.gameObject.SetActive(true);
            DataManager.instance.nowPlayer.Day2_note = true;
            Note();
        }
        if (DataManager.instance.nowPlayer.Day3_chk == 1)
        {
            StoryDialogue[2].gameObject.SetActive(true);
            DayStoryPanel.gameObject.SetActive(true);
            DataManager.instance.nowPlayer.Day3_note = true;
            Note();
        }
        if (DataManager.instance.nowPlayer.Day4_chk == 1)
        {
            
            StoryDialogue[3].gameObject.SetActive(true);
            DayStoryPanel.gameObject.SetActive(true);
            DataManager.instance.nowPlayer.Day4_note = true;
            Note();
        }
        if (DataManager.instance.nowPlayer.Day5_chk == 1)
        {
            StoryDialogue[4].gameObject.SetActive(true);
            DayStoryPanel.gameObject.SetActive(true);
            DataManager.instance.nowPlayer.Day5_note = true;
            Note();
        }
        if (DataManager.instance.nowPlayer.Day6_chk == 1)
        {
            StoryDialogue[5].gameObject.SetActive(true);
            DayStoryPanel.gameObject.SetActive(true);
            DataManager.instance.nowPlayer.Day6_note = true;
            Note();
        }
        if (DataManager.instance.nowPlayer.Day7_chk == 1)
        {
            Character.Instance.character_anim.SetBool("day7", true);
           
            Debug.Log("7");
            StoryDialogue[6].gameObject.SetActive(true);
           DayStoryPanel.SetActive(true);
           DataManager.instance.nowPlayer.Day7_note = true;
           Note();
        }
        if (DataManager.instance.nowPlayer.Day8_chk == 1)
        {
            Character.Instance.character_anim.SetBool("day7", true);
          
            Debug.Log("8");
            StoryDialogue[7].gameObject.SetActive(true);
            DayStoryPanel.SetActive(true);
            DataManager.instance.nowPlayer.Day8_note = true;
            Note();
        }
        
        if (DataManager.instance.nowPlayer.Day9_chk == 1)
        {
            Character.Instance.character_anim.SetBool("day7", true);
           
            Debug.Log("9");
            if (DataManager.instance.nowPlayer.Day9_sty == 0)
            {
                fadeIn.SetActive(true);
                DataManager.instance.nowPlayer.Day9_sty += 1;
            }
            else
            {
                fadeIn.SetActive(false);
            }

        }
        if (DataManager.instance.nowPlayer.Day10_chk == 1)
        {
            Character.Instance.character_anim.SetBool("day7", true);
           
            StoryDialogue[8].gameObject.SetActive(true);
            DayStoryPanel.SetActive(true);
            DataManager.instance.nowPlayer.Day10_note = true;
            Note();
        }
        if (DataManager.instance.nowPlayer.Day11_chk == 1)
        {
            Character.Instance.character_anim.SetBool("day7", true);
           
            StoryDialogue[9].gameObject.SetActive(true);
            DayStoryPanel.SetActive(true);
           
            Note();
        }
        if (DataManager.instance.nowPlayer.Day12_chk == 1)
        {
            storyfade.SetActive(true);

        }

    }
    
    public void Note()
    {
        if (DataManager.instance.nowPlayer.Day1_note)
        {
            day_note[0].sprite = dayimg[0];
            notebtn[0].SetActive(true);
        }
        if (DataManager.instance.nowPlayer.Day2_note)
        {
            day_note[1].sprite = dayimg[1];
            notebtn[1].SetActive(true);
        }
        if (DataManager.instance.nowPlayer.Day3_note)
        {
            day_note[2].sprite = dayimg[2];
            notebtn[2].SetActive(true);
        }
        if (DataManager.instance.nowPlayer.Day4_note)
        {
            day_note[3].sprite = dayimg[3];
            notebtn[3].SetActive(true);
        }
        if (DataManager.instance.nowPlayer.Day5_note)
        {
            day_note[4].sprite = dayimg[4];
            notebtn[4].SetActive(true);
        }
        if (DataManager.instance.nowPlayer.Day6_note)
        {
            day_note[5].sprite = dayimg[5];
            notebtn[5].SetActive(true);
        }
        if (DataManager.instance.nowPlayer.Day7_note)
        {
         
           day_note[6].sprite = dayimg[6];
           notebtn[6].SetActive(true);
        }
        if (DataManager.instance.nowPlayer.Day8_note)
        {
            
            day_note[7].sprite = dayimg[7];
            notebtn[7].SetActive(true);
        }
        if (DataManager.instance.nowPlayer.Day10_note)
        {
            
            day_note[8].sprite = dayimg[8];
            notebtn[8].SetActive(true);
        }


    }
    public void StoryRout()
    {
        Debug.Log("oneplus1");
        DataManager.instance.nowPlayer.storyScore += 1;
    }
   
}
