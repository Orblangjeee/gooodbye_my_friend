using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRunMob : MonoBehaviour
{
    public float mobSpeed = 0;
    public Vector3 StartPosition;
    
    private void OnEnable()
    {
        transform.position = StartPosition;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EndRunGM.instance.isPlay)
        transform.Translate(Vector2.left * Time.deltaTime * EndRunGM.instance.gameSpeed);

        if (transform.position.x < -7)
        {
            gameObject.SetActive(false);
        }
    }

   
}
