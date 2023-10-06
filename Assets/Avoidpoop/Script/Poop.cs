using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private Rigidbody2D pooprigid;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        pooprigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            GameManager.Instance.Score();
            animator.SetTrigger("poop");
            //Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
           
            GameManager.Instance.GameEnd();
            animator.SetTrigger("poop");
            
            //Destroy(this.gameObject);
        }
    }

}
