﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IPointerDownHandler
{
    public static CardUI _instance; //싱글톤
    public Image chr;
    public Text cardName;
    Animator animator;

    public static CardUI instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CardUI>();
            }
            return _instance;
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    // 카드의 정보를 초기화
    public void CardUISet(Card card)
    {
        chr.sprite = card.cardImage;
        cardName.text = card.cardName;
    }
    // 카드가 클릭되면 뒤집는 애니메이션 재생
    public void OnPointerDown(PointerEventData eventData)
    {
        animator.SetTrigger("Flip");
        ItemGM.instance.GetCard();
        
    }
    void OnDisable()
    {
        Destroy(this.gameObject);
    }
}
