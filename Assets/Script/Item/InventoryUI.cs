using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{


    //인벤토리
    public GameObject inventoryPanel;
    bool activeInventory = false;
    
    void Start()
    {
        inventoryPanel.SetActive(activeInventory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Inventory()
    {
        activeInventory = !activeInventory;
        inventoryPanel.SetActive(activeInventory);
    }

  
}
