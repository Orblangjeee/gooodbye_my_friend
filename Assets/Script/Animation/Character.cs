using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    
    private static Character _instance; //싱글톤
    public static Character Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Character>();
            }
            return _instance;
        }
    }

    public Animator character_anim;

    void Awake()
    {
        
    }

    void Start()
    {
        Animator character_anim = GetComponent<Animator>();
    }

    void Update()
    {
      
            if (DataManager.instance.nowPlayer.curStress == DataManager.instance.nowPlayer.maxStress)
            {
                Character.Instance.character_anim.SetBool("maxstress", true);
            }
            else
            {
                Character.Instance.character_anim.SetBool("maxstress", false);
            }
      
    }

   
}
