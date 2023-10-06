using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GM : MonoBehaviour
{
    private static GM _instance; //싱글톤
    public static GM Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GM>();
            }
            return _instance;
        }
    }

   

    
    void Start()
    {

    }


    void Awake()
    {
        //GM 저장, 동일 GM 파괴
        var objs = FindObjectsOfType<GM>();
        if (objs.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

  



   
  

   

   
   
}
