using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectionUI_New : MonoBehaviour
{
    public static List<MixExpression> mixExpressions = null;

    public int page = 0;
    public List<CollectionSlot> slots = new List<CollectionSlot>();
    public ItemAssetList allItemList;

    public Button lbutton;
    public Button rbutton;
    public TMP_Text index;
    public GameObject collectionOpenButton;
    //1. 데이터 합성 불러옴
    private void Awake()
    {
        if(mixExpressions == null)
        {
            mixExpressions = new List<MixExpression>();
            List<Dictionary<string, object>> kvp = CSVReader.Read("Database/MixData");
            foreach(var i in kvp)
            {
                MixExpression m = new MixExpression();
                m.a = i["table1"].ToString();
                m.b = i["table2"].ToString();
                m.c = i["table3"].ToString();
                mixExpressions.Add(m);
            }
        }
    }

    private void OnEnable()
    {
        UpdatePage(page);
        collectionOpenButton.SetActive(false);
    }

    private void OnDisable()
    {
        collectionOpenButton.SetActive(true);
    }

    public void AddPageNumber(int addition)
    {
        if(addition + page < 0|| addition + page > 4)
        {
            return;
        }
        page += addition;
        UpdatePage(page);
    }

    void UpdatePage(int p)
    {
        page = p;
        //페이지를 넘긴다면
        //새로운 페이지를 등록하고, 오브젝트들에 해당 페이지 정보 부여
        for(int i = 0; i < 10; i++)
        {
            slots[i].gameObject.SetActive(false);
            if (p * 10 + i < mixExpressions.Count)
            {
                slots[i].gameObject.SetActive(false);
                slots[i].expression = mixExpressions[i + p * 10];
                slots[i].myCollection = this;
                slots[i].Init();
                slots[i].gameObject.SetActive(true);
            }            
        }

        //UI업데이트
        lbutton.gameObject.SetActive(true);
        rbutton.gameObject.SetActive(true);
        index.text = (page + 1) + " / " + 5;
        if (page == 0)
        {
            lbutton.gameObject.SetActive(false);
        }
        if (page == 4)
        {
            rbutton.gameObject.SetActive(false);
        }
    }

    public Sprite FindSpriteFronItemName(string itemName)
    {
        foreach(var i in allItemList.items)
        {
            if (itemName.Equals(i.itemName))
            {
                return i.itemImage;
            }
        }
        return null;
    }


}
