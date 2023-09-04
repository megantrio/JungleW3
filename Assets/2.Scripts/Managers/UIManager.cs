using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;

public class UIManager : EventObject
{
    // Instance
    public static UIManager Instance;

    public FadeInOut fadeInOut;

    [Header("ItemMix")]
    public ItemMixerSlot[] itemMixSlot;
    public InventorySlot itemInfo;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemInfoText;
    public GameObject mixErrorUI;
    public ItemMixer itemMix;
    [HideInInspector] public bool isItemMax;
    [HideInInspector] public bool isMixed;
    [HideInInspector] public bool isItemAdd = false;

    [Header("Inventory")]
    public ItemAssetList stuffItemList;
    public ItemAssetList SpecialItemList;
    public ItemAssetList mixedItemList;
    public ItemAssetList SpecialMixItemList;

    [SerializeField] private GameObject invenObj;
    public Inventory basicInven;
    public Inventory specialInven;
    public Inventory mixInven;
    public Inventory specialMixInven;
    [SerializeField] private int maxItemCount;

    [Header("Clock")]
    [SerializeField] private TextMeshProUGUI nowDays;
    [SerializeField] private GameObject clock;
    [SerializeField] private GameObject hand;
    private bool isTimeGo = true;
    private bool isUpdate;
    private float fdt = 80f;
    [SerializeField] private float plusFdt = 30f;
    [SerializeField] private float maxFdt = -90f;

    [Header("News&OrderList")]
    [SerializeField] private GameObject newsUI;
    public TextMeshProUGUI newsPaperHeadLine;
    public TextMeshProUGUI newsPaperText;

    public Transform orderListParent;
    public GameObject orderPref;
    [HideInInspector] public GameObject temp;

    private List<Dictionary<string, object>> newsAndOrderList;

    private int newsID = 0;
    private int orderID = 1;
    public int nowDay;
    private void Awake()
    {
        Instance = this;
        newsAndOrderList = CSVReader.Read("Database/newsAndOrder");
        Debug.Log("Readcsv");
    }

    private void OnEnable()
    {
        nowDay = DayManager.instance.day;
        AllUpdate();
    }

    
    public override void StartEvent()
    {
        
    }
    
    public void EndEvent() 
    {
        Debug.Log("EventEnd");
        PostEventEnded();
    }
    
    public void AllUpdate()
    {
        nowDays.text = nowDay.ToString();
        isUpdate = true;
        MixItemReset();
        UpdateInfo();
        UpdateInven();
    }
    /*
    public void FadeResetEffect()
    {
        isTimeGo = false;
        isUpdate = true;
        StartCoroutine(menuSwitching());
    }


    IEnumerator menuSwitching()
    {
        fadeInOut.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Fade실행");
        fadeInOut.FadeIn(0.5f);
        yield return new WaitForSeconds(1f);
        AllUpdate();
        fdt = 80f;
        fadeInOut.FadeOut(0.5f);
        yield return new WaitForSeconds(0.5f);
        isTimeGo = true;
        fadeInOut.gameObject.SetActive(false);
    }
    */
    public void NewsUION()
    {
        isTimeGo = !newsUI.activeSelf;
        newsUI.SetActive(!newsUI.activeSelf);
    }

    public void UpdateInfo()
    {
        RectTransform[] childList = orderListParent.GetComponentsInChildren<RectTransform>();

        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform) { Destroy(childList[i].gameObject); }
            }
        }

        foreach (var v in newsAndOrderList)
        {
            if (v["Day"].Equals(nowDay) && v["ID"].Equals(newsID))
            {
                newsPaperHeadLine.text = v["HeadOrder"].ToString();
                newsPaperText.text = v["Script"].ToString();
            }

            if (v["Day"].Equals(nowDay) && v["ID"].Equals(orderID))
            {
                temp = Instantiate(orderPref, orderListParent);
                OrderList tempOrder = temp.GetComponent<OrderList>();
                tempOrder.ResetList();
                string orderer = v["Orderer"].ToString();
                Debug.Log($"주문자 : {orderer}");
                tempOrder.ordererText.text = orderer;
                tempOrder.orderItem = v["HeadOrder"].ToString();
                tempOrder.orderList.text = v["Script"].ToString();

            }
        }
    }

    public void MixItemCheck(Item item)
    {
        OrderList[] orderList = orderListParent.GetComponentsInChildren<OrderList>();

        foreach (var i in orderList)
        {
            i.checkItem(item.itemName);
        }
    }

    public void MixItemPlus(Item item)
    {
        if (item == null) return;

        isMixed = false;
        itemInfo.item = item;
        itemNameText.text = item.itemName;
        itemInfoText.text = item.itemInfo;

        if (item.isMixItem && item.isSpecialItem) return;


        if (itemMixSlot[0].item == null)
        {
            itemMixSlot[0].item = item;
        }

        else if (itemMixSlot[1].item == null)
        {
            itemMixSlot[1].item = item;
            isItemMax = true;
        }

        else
        {
            mixErrorUI.SetActive(true);
            isItemAdd = true;
            Debug.LogError("믹스 창 아이템 가득 참");
        }
    }

    public void MixItemReset()
    {
        isItemMax = false;
        for (int i = 0; i < itemMixSlot.Length; i++)
        {
            if (itemMixSlot[i].item == null) return;

            if (itemMixSlot[i].item.isSpecialItem && itemMixSlot[i].item.isMixItem)
            {
                specialMixInven.AddItem(itemMixSlot[i].item);
            }

            else if (!isMixed && itemMixSlot[i].item.isMixItem)
            {
                Debug.Log("isMixed = false");
                mixInven.AddItem(itemMixSlot[i].item);
            }

            else if (itemMixSlot[i].item.isSpecialItem)
            {
                if (!isMixed && !isItemAdd && itemMixSlot[i].item.itemCount <= 0)
                {
                    specialInven.AddItem(itemMixSlot[i].item);
                    itemMixSlot[i].item.itemCount++;
                }
                else if (!isItemAdd && !isMixed)
                {
                    itemMixSlot[i].item.itemCount++;
                }
            }

            itemMixSlot[i].item = null;
        }
        isMixed = false;
        isItemAdd = false;
    }

    public void MixItemCall()
    {
        Item mixItem;
        if (itemMixSlot[0].item != null && itemMixSlot[1].item != null)
        {
            fdt -= plusFdt;
            hand.transform.rotation = Quaternion.Euler(0,0,fdt);
            mixItem = itemMix.MixItem(itemMixSlot[0].item, itemMixSlot[1].item);       
        }
        else
        {
            return;
        }

        if (mixItem.isMixItem && mixItem.isSpecialItem)
        {
            specialMixInven.AddItem(mixItem);
        }

        else
        {
            mixInven.AddItem(mixItem);
            Debug.Log("MixItem++");
        }

        if (fdt < -maxFdt)
        {
            EndEvent();
            fdt = 80f;
        }
    }

    public void OpenInven()
    {
        invenObj.SetActive(true);
    }

    public void UpdateInven()
    {
        basicInven.ResetInven();
        specialInven.ResetInven();
        mixInven.ResetInven();
        specialMixInven.ResetInven();

        // nowDay = DataManager_Fix.instance.nowDays;
        for (int i = 0; i < stuffItemList.items.Length; i++)
        {
            if (stuffItemList.items[i].applyDay <= nowDay && !stuffItemList.items[i].isMixItem)
            {
                basicInven.AddItem(stuffItemList.items[i]);
            }
        }

        for (int i = 0; i < SpecialItemList.items.Length; i++)
        {
            SpecialItemList.items[i].itemCount = maxItemCount;
            if (SpecialItemList.items[i].applyDay <= nowDay && !stuffItemList.items[i].isMixItem)
            {
                specialInven.AddItem(SpecialItemList.items[i]);
            }
        }
    }

    public void CloseMaxMix()
    {
        isItemAdd = false;
        mixErrorUI.SetActive(false);
    }


    public void CloseInven()
    {
        invenObj.SetActive(false);
    }

    public void checkActiveSelf(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}

