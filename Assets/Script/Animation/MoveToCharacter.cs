using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveToCharacter : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    Vector3 mousePos, transPos, targetPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                CalTargetPos();
            }
        }
        MoveToTarget();
    }
    void CalTargetPos()
    {
        mousePos = Input.mousePosition;
        transPos = Camera.main.ScreenToWorldPoint(mousePos);
        targetPos = new Vector3(transPos.x, transPos.y, 0);

    }

    void MoveToTarget()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);
    }
}
