using System.Collections;
using UnityEngine;

public class LoveAnim : MonoBehaviour
{
    // Start is called before the first frame update
    private float moveSpeed;
  //  private float alphaSpeed;
    private float destroyTime;

    Color alpha;
    void Start()
    {
        moveSpeed = 2.0f;
       // alphaSpeed = 1.0f;
        destroyTime = 1.0f;
        //alpha = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = alpha;
        Invoke("DestroyObject", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
       transform.Translate(Vector3.up * moveSpeed * Time.deltaTime); // 텍스트 위치

        //alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // 텍스트 알파값
    }

    private void DestroyObject()
    {
       Destroy(gameObject);
    }

}