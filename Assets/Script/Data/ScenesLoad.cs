using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoad : MonoBehaviour
{
    // Start is called before the first frame update
    public string SceneToLoad;

    // Update is called once per frame
    public void LoadGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneToLoad);
    }
    public void Ending()
    {
        Debug.Log(DataManager.instance.nowPlayer.storyScore);
        if (DataManager.instance.nowPlayer.storyScore == 2)
        {

            SceneManager.LoadScene("06.Nomal");
        }
        if (DataManager.instance.nowPlayer.storyScore < 2)
        {
            SceneManager.LoadScene("07.Bad");
        }
    }
}
