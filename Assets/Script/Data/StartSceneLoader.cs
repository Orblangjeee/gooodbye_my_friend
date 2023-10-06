using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneLoader : MonoBehaviour
{
    public static string loadScene;
    public static int loadType;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    public static void LoadSceneHandle(string _name, int _loadType)
    {
        loadScene = _name;
        loadType = _loadType;
        if (loadType == 0)
        {
            Debug.Log("새게임");
            SceneManager.LoadScene(loadScene);
        }
        else if (loadType == 1)
        {
            
        }
        
        
    }

    
    
}
