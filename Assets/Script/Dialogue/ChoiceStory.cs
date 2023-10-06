using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoiceStory : MonoBehaviour
{
    public static ChoiceStory instance;

    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    #endregion Singleton

    private AudioManager theAudio; //사운드 재생

    private string question;
    private List<string> answerList;

    public GameObject go; //평소에 비활성화 시킬 목적으로 선언
    public TextMeshProUGUI question_Text;
    public TextMeshProUGUI[] answer_Text;
    public GameObject[] answer_Panel;
    public Animator anim;
    public string keySound;
    public string enterSound;
    public bool choiceIng; //대기 
    private bool keyInput; //키처리 활성화, 비활성화
    private int count; //배열의 크기
    private int result; //선택한 선택창번호
    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        answerList = new List<string>();
        for (int i =0; i <=answer_Text.Length; i++)
        {
            answer_Text[i].text = "";
            answer_Panel[i].SetActive(false);
        }
        question_Text.text = "";
    }

    public void ShowChoice(Choice _choice)
    {
        result = 0;
        question = _choice.question;
        for (int i = 0; i < _choice.answers.Length; i++)
        {
            answerList.Add(_choice.answers[i]);
            answer_Panel[i].SetActive(true);
            count = i;
        }

        anim.SetBool("Appear", true);
        StartCoroutine(ChoiceCoroutine());
    }

    IEnumerator ChoiceCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
    }

    IEnumerator TypingQuestion()
    {
        for (int i=0; i<question.Length; i++)
        {
            question_Text.text += question[i];
            yield return waitTime;
        }
    }

    IEnumerator TypingAnswer_0()
    {
        for (int i = 0; i < answerList[0].Length; i++)
        {
            answer_Text[0].text += answerList[0][i];
            yield return waitTime;
        }
    }

    IEnumerator TypingAnswer_1()
    {
        for (int i = 0; i < answerList[1].Length; i++)
        {
            answer_Text[1].text += answerList[1][i];
            yield return waitTime;
        }
    }

    IEnumerator TypingAnswer_2()
    {
        for (int i = 0; i < answerList[2].Length; i++)
        {
            answer_Text[2].text += answerList[2][i];
            yield return waitTime;
        }
    }
}
