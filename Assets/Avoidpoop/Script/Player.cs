using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D bodyrigid;
    private Animator animator;
    private SpriteRenderer sprender;
    private float speed = 3;
    private float horizontal;

    
    public bool isDie = false;

  

    void Start()
    {
        bodyrigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprender = GetComponent<SpriteRenderer>();
        
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        if (GameManager.Instance.stopTrigger)
        {
            PlayerMove();
        }

        if (!GameManager.Instance.stopTrigger)
        {
            animator.SetTrigger("dead");
        }
        ScreenChk();
    }

   
   
    private void PlayerMove()
    {
        animator.SetFloat("speed", Mathf.Abs(horizontal));
        if (horizontal < 0)
        {
            sprender.flipX = true;
        }
        else
        {
            sprender.flipX = false;
        }
        bodyrigid.velocity = new Vector2(horizontal * speed, bodyrigid.velocity.y);
    }

    private void ScreenChk()
    {
        Vector3 worlpos = Camera.main.WorldToViewportPoint(this.transform.position);
        if (worlpos.x < 0.05f) worlpos.x = 0.05f;
        if (worlpos.x > 0.95f) worlpos.x = 0.95f;
        this.transform.position = Camera.main.ViewportToWorldPoint(worlpos);
    }
}
