using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

[System.Serializable]
public class Serialization<T>
{
    public Serialization(List<T> _target) => target = _target;
    public List<T> target;
}


[System.Serializable]
public class Item
{
    public Item(string _Type, string _Name, string _Explain, string _Number, bool _isUsing, string _Index, string _Price)
    { Type = _Type; Name = _Name; Explain = _Explain; Number = _Number; isUsing = _isUsing;  Index = _Index; Price = _Price;   }

    public string Type, Name, Explain;
    public string Number, Index, Price;
    public bool isUsing;
}

public class ItemGM : MonoBehaviour
{
    public static ItemGM _instance;

    public static ItemGM instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ItemGM>();
            }
            return _instance;
        }
    }
    //아이템

    public TextAsset ItemDatabase;
    public List<Item> AllItemList, MyItemList, CurItemList, UsingItemList;
    public string curType = "Snack";

    public GameObject[] Slot, UsingImage ;
    public Image[] TabImage, ItemImage  ;
    public Sprite TabIdleSprite, TabSelectSprite;
    public Sprite[] ItemSprite ;
    public GameObject ExplainPanel;
    public RectTransform[] SlotPos;
    public RectTransform CanvasRect;
    public InputField ItemNameInput, ItemNumberInput, ShopnumberInput;
    public GameObject BuyWindow;
    public TextMeshProUGUI ShopExplain, ShopPrice, HaveNumber;
    
    IEnumerator PointerCoroutine;
    RectTransform ExplainRect;
    //public Vector2 v;
    string filePath;


    void Start()
    {
        //전체 아이템 리스트 불러오기
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');

            AllItemList.Add(new Item(row[0], row[1], row[2], row[3], row[4] == "TRUE", row[5], row[6]));
        }

        filePath = Application.persistentDataPath + "/MyItemText";
        Load();
        ExplainRect = ExplainPanel.GetComponent<RectTransform>();
        
        print(filePath);
        BuyWindow.SetActive(false);
        noMoney.SetActive(false);

    }

    void Update()
    {

        //마우스 위치를 캔버스 위치로 변환해 설명창 띄움
        RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvasRect, Input.mousePosition, Camera.main, out Vector2 anchredPos);
        ExplainRect.anchoredPosition3D = anchredPos + new Vector2(-800, -350);

        

    }

    public void GetCard()
    {
        Item curItem = MyItemList.Find(x => x.Name == CardUI.instance.cardName.text);
        if (curItem != null)
        {
            curItem.Number = (int.Parse(curItem.Number) + 1).ToString();
        }
        else
        {
            // 전체에서 얻을 아이템을 찾아 내 아이템에 추가
            Item curAllItem = AllItemList.Find(x => x.Name == CardUI.instance.cardName.text);
            if (curAllItem != null)
            {
                curAllItem.Number = "1"; //입력한 숫자만큼 아이템 생성 (이거 없으면 1로 생성됨)
                MyItemList.Add(curAllItem);
            }
        }
        MyItemList.Sort((p1, p2) => p1.Index.CompareTo(p2.Index));
        Save();

    }


    public void GetItemClick()
    {
        Item curItem = MyItemList.Find(x => x.Name == ItemNameInput.text);
        ItemNumberInput.text = ItemNumberInput.text == "" ? "1" : ItemNumberInput.text; //비어있을 때 1개 생성
        if (curItem != null)
        {
            curItem.Number = (int.Parse(curItem.Number) + int.Parse(ItemNumberInput.text)).ToString();
        }
        else
        {
            // 전체에서 얻을 아이템을 찾아 내 아이템에 추가
            Item curAllItem = AllItemList.Find(x => x.Name == ItemNameInput.text);
            if (curAllItem != null)
            {
                curAllItem.Number = ItemNumberInput.text; //입력한 숫자만큼 아이템 생성 (이거 없으면 1로 생성됨)
                MyItemList.Add(curAllItem);
            }
        }
        MyItemList.Sort((p1, p2) => p1.Index.CompareTo(p2.Index));
        Save();

    }


    public void RemoveItemClick()
    {
        Item curItem = MyItemList.Find(x => x.Name == ItemNameInput.text);
        if (curItem != null)
        {
            int curNumber = int.Parse(curItem.Number) - int.Parse(ItemNumberInput.text == "" ? "1" : ItemNumberInput.text); //비어있을 때 1개 삭제

            if (curNumber <= 0) MyItemList.Remove(curItem);
            else curItem.Number = curNumber.ToString();
        }
        MyItemList.Sort((p1, p2) => p1.Index.CompareTo(p2.Index));
        Save();
    }

    public void ResetItemClick()
    {
        Item BasicItem = AllItemList.Find(x => x.Name == "삑삑이 인형");
        BasicItem.isUsing = false;
        MyItemList = new List<Item>() { BasicItem };
        Save();
        Load();
    }

    public GameObject[] DecoImage;

    public void SlotClick(int slotNum)
    {
        Item CurItem = CurItemList[slotNum];
        Item UsingItem = CurItemList.Find(x => x.isUsing == true);

        if (curType == "Snack")
        {
            CurItem.isUsing = false;
            UseSnack(slotNum);

        }
        if (curType == "Deco")
        {
            //클릭하면 true, false 둘 다 반환
            CurItem.isUsing = !CurItem.isUsing;
            //if (UsingItem == null) UsingItem.isUsing = false;
            
            int deconumber = int.Parse(CurItem.Number);
           
            if (CurItem.isUsing)
            {
                switch (int.Parse(CurItem.Index))
                {
                    case 5:
                        print("switch" + CurItem.Name);
                        DecoImage[0].SetActive(true);
                        deconumber -= 1;
                        break;
                    case 6:
                        print("switch" + CurItem.Name);
                        DecoImage[1].SetActive(true);
                        deconumber -= 1;
                        break;
                    case 7:
                        print("switch" + CurItem.Name);
                        DecoImage[2].SetActive(true);
                        deconumber -= 1;
                        break;
                    case 8:
                        print("switch" + CurItem.Name);
                        DecoImage[3].SetActive(true);
                        deconumber -= 1;
                        break;
                    case 9:
                        print("switch" + CurItem.Name);
                        DecoImage[4].SetActive(true);
                        deconumber -= 1;
                        break;
                    case 10:
                        print("switch" + CurItem.Name);
                        DecoImage[5].SetActive(true);
                        deconumber -= 1;
                        break;
                }
                CurItem.Number = deconumber.ToString();
            }
            if (CurItem.isUsing != true)
            {
                switch (int.Parse(CurItem.Index))
                {
                    case 5:
                        print("switch" + CurItem.Name);
                        DecoImage[0].SetActive(false);
                        deconumber += 1;
                        break;
                    case 6:
                        print("switch" + CurItem.Name);
                        DecoImage[1].SetActive(false);
                        deconumber += 1;
                        break;
                    case 7:
                        print("switch" + CurItem.Name);
                        DecoImage[2].SetActive(false);
                        deconumber += 1;
                        break;
                    case 8:
                        print("switch" + CurItem.Name);
                        DecoImage[3].SetActive(false);
                        deconumber += 1;
                        break;
                    case 9:
                        print("switch" + CurItem.Name);
                        DecoImage[4].SetActive(false);
                        deconumber += 1;
                        break;
                    case 10:
                        print("switch" + CurItem.Name);
                        DecoImage[5].SetActive(false);
                        deconumber += 1;
                        break;
                }
                CurItem.Number = deconumber.ToString();
            }

            //클릭하면 true만 반환
            //if (UsingItem != null) UsingItem.isUsing = false;
            //CurItem.isUsing = true;
        }

        Save();

    }

    public void UsingCheck()
    {
        //타입이 데코인 아이템의 인덱스가 5,6,7,8,9,10 인 아이템에 Using을 확인해 index-5인 배열의 이미지 활성화 체크
        //isUsing 이 true 확인
        // 그 배열 다 확인해서 인덱스 불러와서 맞는 이미지 매칭
        UsingItemList = MyItemList.FindAll(x => x.isUsing == true);
       
        if (UsingItemList.Count != 0)
        {
            for (int i = 0; i < UsingItemList.Count; i++)
            {
               
                DecoImage[int.Parse(UsingItemList[i].Index) - 5].SetActive(true);
            }

        }

    }




    public void TabClick(string tabName)
    {
        //현재 아이템 리스트에 클릭한 타입만 추가
        curType = tabName;
        CurItemList = MyItemList.FindAll(x => x.Type == tabName);
  

        for (int i = 0; i < Slot.Length; i++)
        {
            //슬롯과 텍스트 보이기
            bool isExist = i < CurItemList.Count;
            Slot[i].SetActive(isExist);
            Slot[i].GetComponentInChildren<Text>().text = isExist ? CurItemList[i].Name : ""; //+ "/" + CurItemList[i].isUsing

            //아이템 이미지 사용중인지 보이기
            if (isExist)
            {
                ItemImage[i].sprite = ItemSprite[AllItemList.FindIndex(x => x.Name == CurItemList[i].Name)];
                UsingImage[i].SetActive(CurItemList[i].isUsing);
            }
        }


        //탭 이미지
        int tabNum = 0;
        switch (tabName)
        {
            case "Snack": tabNum = 0; break;
            case "Deco": tabNum = 1; break;
                //case "Custom": tabNum = 2; break;
        }
        for (int i = 0; i < TabImage.Length; i++)
        {
            TabImage[i].sprite = i == tabNum ? TabSelectSprite : TabIdleSprite;
        }
    }

    public void PointerEnter(int slotNum)
    {
        //슬롯에 마우스를 올리면 0.5초 후에 설명창 띄움
        PointerCoroutine = PointerEnterDelay(slotNum);
        StartCoroutine(PointerCoroutine);

        //설명창에 이름, 이미지, 개수, 설명 나타내기
        ExplainPanel.GetComponentInChildren<TextMeshProUGUI>().text = CurItemList[slotNum].Name;
        ExplainPanel.transform.GetChild(2).GetComponent<Image>().sprite = Slot[slotNum].transform.GetChild(0).GetComponent<Image>().sprite;
        ExplainPanel.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = CurItemList[slotNum].Number + "개 소유";
        ExplainPanel.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = CurItemList[slotNum].Explain;
    }

   

    IEnumerator PointerEnterDelay(int slotNum)
    {
        yield return new WaitForSeconds(0.5f);
        ExplainPanel.SetActive(true);
    }
    public void PointerExit(int slotNum)
    {
        StopCoroutine(PointerCoroutine);
        ExplainPanel.SetActive(false);

    }


    public void Save() //내 아이템 저장
    {
        //암호화
        string jdata = JsonUtility.ToJson(new Serialization<Item>(MyItemList));
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jdata);
        string code = System.Convert.ToBase64String(bytes);


        File.WriteAllText(filePath + DataManager.instance.nowSlot+".txt", code);

        TabClick(curType);
        UsingCheck();

    }

    void Load() //불러오기
    {
        if (!File.Exists(filePath+ DataManager.instance.nowSlot + ".txt")) { ResetItemClick(); return; }

        string code = File.ReadAllText(filePath+ DataManager.instance.nowSlot+".txt");

        byte[] bytes = System.Convert.FromBase64String(code);
        string jdata = System.Text.Encoding.UTF8.GetString(bytes);
        MyItemList = JsonUtility.FromJson<Serialization<Item>>(jdata).target;

        TabClick(curType);
        UsingCheck();
       

    }
    public GameObject love;
    public GameObject love2;
    public GameObject love3;
    IEnumerator FloatingLove()
    {
        Instantiate(love);
        yield return new WaitForSeconds(0.2f);
        Instantiate(love2);
        yield return new WaitForSeconds(0.4f);
        Instantiate(love3);
    }
    //아이템 사용
    private void UseSnack(int slotnum)
    {
        int itemnumber = int.Parse(CurItemList[slotnum].Number);
        string itemname = CurItemList[slotnum].Name;

        if (DataManager.instance.nowPlayer.curHungry < DataManager.instance.nowPlayer.maxHungry)
        {
            switch (itemname)
            {
                case "개껌":
                    DataManager.instance.nowPlayer.curHungry += 10;
                    break;
                case "스페셜 개껌":
                    DataManager.instance.nowPlayer.curHungry += 30;
                    DataManager.instance.nowPlayer.curStress -= 10;
                    break;
                case "오리 날개":
                    DataManager.instance.nowPlayer.curHungry += 40;
                    DataManager.instance.nowPlayer.curHeart += 10;
                    break;
                case "양치껌":
                    DataManager.instance.nowPlayer.curStress -= 20;
                    break;
                case "황태 큐브":
                    DataManager.instance.nowPlayer.curHungry += 20;
                    DataManager.instance.nowPlayer.curStress -= 10;
                    DataManager.instance.nowPlayer.curHeart += 20;
                    break;

            }
            StartCoroutine(FloatingLove());
            CurItemList[slotnum].Number = (itemnumber - 1).ToString();
            if (itemnumber <= 1)
            {
                
                MyItemList.Remove(CurItemList[slotnum]);
                StopCoroutine(PointerCoroutine);
                ExplainPanel.SetActive(false);
            }



            else
            {
                print("No Stress");
            }
        }
        else
        {
            print("Hungry Full");
        }

        MyItemList.Sort((p1, p2) => p1.Index.CompareTo(p2.Index));
    }


    public string buyItemName;
    public int sellprice;
    public void BuyClick()
    {
        Item curItem = MyItemList.Find(x => x.Name == buyItemName);
        ShopnumberInput.text = ShopnumberInput.text == "" ? "0" : ShopnumberInput.text; //비어있을 때 0개 생성
        int buyprice = sellprice * int.Parse(ShopnumberInput.text);
        //돈 없으면 구매 안됑
        if (DataManager.instance.nowPlayer.money < buyprice) 
        {
            noMoney.gameObject.SetActive(true);
            moneyWarnmsg.GetComponent<TextMeshProUGUI>().text = "돈이 없습니다!";
        }
        //개수 * 돈만큼의 돈이 있으면 구매
        if (DataManager.instance.nowPlayer.money >= buyprice)
        {
            DataManager.instance.nowPlayer.money -= buyprice;
            if (curItem != null)
            {
                curItem.Number = (int.Parse(curItem.Number) + int.Parse(ShopnumberInput.text)).ToString();
            }
            else
            {
                // 전체에서 얻을 아이템을 찾아 내 아이템에 추가
                Item curAllItem = AllItemList.Find(x => x.Name == buyItemName);
                if (curAllItem != null)
                {
                    curAllItem.Number = ShopnumberInput.text; //입력한 숫자만큼 아이템 생성 (이거 없으면 1로 생성됨)
                    MyItemList.Add(curAllItem);
                }
            }
            MyItemList.Sort((p1, p2) => p1.Index.CompareTo(p2.Index));
        }
        Save();
    }

    public GameObject noMoney;
    public TextMeshProUGUI moneyWarnmsg;

    public void SellClick()
    {
        Item curItem = MyItemList.Find(x => x.Name == buyItemName);

        //소유한 개수 내로만 판매 가능 
        if (curItem == null || int.Parse(ShopnumberInput.text) > int.Parse(curItem.Number))
        {
            noMoney.gameObject.SetActive(true);
            moneyWarnmsg.GetComponent<TextMeshProUGUI>().text = "가지고 있는 아이템 개수가 충분하지 않습니다!";
        }
        if (curItem != null)
        {
            if( int.Parse(ShopnumberInput.text) <= int.Parse(curItem.Number) )
            {
                int curNumber = int.Parse(curItem.Number) - int.Parse(ShopnumberInput.text == "" ? "0" : ShopnumberInput.text); //비어있을 때 0개 삭제
                if (curNumber <= 0) MyItemList.Remove(curItem);
                else curItem.Number = curNumber.ToString();

                DataManager.instance.nowPlayer.money += sellprice * int.Parse(ShopnumberInput.text);
            }
        }
       
        MyItemList.Sort((p1, p2) => p1.Index.CompareTo(p2.Index));
        Save();
    }
    public void Sellprice(int price)
    {
        sellprice = price;
    }
    //상점
    public void ShopClick(string itemname) 
    {
        BuyWindow.SetActive(true);
        buyItemName = itemname;
       

        Item curAllItem = AllItemList.Find(x => x.Name == buyItemName);
        Item curItem = MyItemList.Find(x => x.Name == buyItemName);
        ShopExplain.GetComponent<TextMeshProUGUI>().text = curAllItem.Explain;
        ShopPrice.GetComponent<TextMeshProUGUI>().text = curAllItem.Price;
        if(curItem != null)
        {
            HaveNumber.GetComponent<TextMeshProUGUI>().text = curItem.Number;
        }
        else
        {
            HaveNumber.GetComponent<TextMeshProUGUI>().text = "0";
        }
        

    }

    public Image[] shoptab;
    public GameObject shopSnack, shopDeco;
   public void ShopTabClick(string tabName)
    {
        
        int tabNum = 0;
        switch (tabName)
        {
            case "Snack":
                tabNum = 0;
                shopSnack.SetActive(true);
                shopDeco.SetActive(false);
                break;
            case "Deco": 
                tabNum = 1;
                shopDeco.SetActive(true);
                shopSnack.SetActive(false);
                break;
                //case "Custom": tabNum = 2; break;
        }
        for (int i = 0; i < shoptab.Length; i++)
        {
            shoptab[i].sprite = i == tabNum ? TabSelectSprite : TabIdleSprite;
            
        }
    }

   

}
