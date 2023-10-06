using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRunPlayer : MonoBehaviour
{
    bool isJump = false;
    bool isTop = false;
    public float jumpHeight = 0;
    public float jumpSpeed = 0;

    Vector2 startPosition;
    Animator runanimator;
   

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        runanimator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EndRunGM.instance.isPlay)
        {
            Time.timeScale = 1;
            runanimator.SetBool("run", true);
        }
        else
        {
            Time.timeScale = 0;
            runanimator.SetBool("run", false);
        }
        
        
        if (Input.GetKey(KeyCode.Space) && EndRunGM.instance.isPlay)
        {
            isJump = true;
        } else if (transform.position.y <= startPosition.y)
        {
            isJump = false;
            isTop = false;
            transform.position = startPosition;
        }

        if (isJump)
        {
            if ( transform.position.y <= jumpHeight - 0.1f && !isTop)
            {
                transform.position = Vector2.Lerp(transform.position,
                    new Vector2(transform.position.x, jumpHeight), jumpSpeed * Time.deltaTime);
            }
            else
            {
                isTop = true;
            }
            if (transform.position.y > startPosition.y && isTop)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                    startPosition, jumpSpeed * Time.deltaTime);
            }
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mob"))
        {
            EndRunGM.instance.GameOver();
            
        }
        if (collision.CompareTag("Ground"))
        {
            runanimator.SetBool("jump", false);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            runanimator.SetBool("jump", true);
        }
    }

}
