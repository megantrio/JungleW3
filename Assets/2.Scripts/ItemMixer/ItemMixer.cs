using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class ItemMixer : MonoBehaviour
{
    //현재는 실제 아이템 리스트, UI 모두를 관리함.
    #region PublicVariables
    public Inventory inventory;
    #endregion

    #region PrivateVariables
    [Header("믹스 후 아이템 획득 UI")]
    public GameObject mixerUI;
    public TMP_Text mixerTextUI;
    private List<Dictionary<string, object>> mixData;

    [Header("파일로 저장된 스크립터블오브젝트")]
    //저장되어있는 ScriptableObject
    public Item[] mixedItemAssets;
    public Item kkwangItemAsset;
    #endregion

    #region PublicMethod


    public void OnItemCheck(Item _item1, Item _item2)
    {

    }

    public Item MixItem(Item _item1, Item _item2)
    {
        //테이블의 첫 열
        string t1 = "table1";
        string t2 = "table2";
        string t3 = "table3";
        if (mixData == null)
        {
            CSVReader.Read("Database/MixData");
        }
        string result = "";
        foreach (var i in mixData)
        {
            if (i[t1].Equals(_item1.itemName) && i[t2].Equals(_item2.itemName)
                || i[t1].Equals(_item2.itemName) && i[t2].Equals(_item1.itemName))
            {
                result = i[t3].ToString();
            }
        }
        if (result.Equals(""))
        {
            return kkwangItemAsset;
        }
        for (int i = 0; i < mixedItemAssets.Length; i++)
        {
            if (mixedItemAssets[i].itemName.Equals(result))
            {
                return mixedItemAssets[i];
            }
        }
        Debug.LogError("조합식은 존재하지만, 해당하는 아이템이 존재하지 않습니다.");
        return null;
    }

    #endregion

    #region PrivateMethod
    private void OnValidate()
    {
        slots = GetComponentsInChildren<ItemMixerSlot>();
        inventorySlots = new InventorySlot[slots.Length];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].index = i;
            slots[i].itemMixer = this;
        }
    }

    private void Awake()
    {
        //조합데이터 로드
        mixData = CSVReader.Read("Database/MixData");
    }


    #endregion

    #region NotUse

    private InventorySlot[] inventorySlots;
    private Transform slotParent;
    private ItemMixerSlot[] slots;
    private Item resultOfMix;
    public int AddItem(Item _item)
    {
        //믹서가 켜져있을 때 인벤토리의 아이템을 클릭하면 실행됨
        //선택한 아이템이 매개변수로 들어온다.
        //1. 슬롯에 빈자리가 있나 확인
        //2. 슬롯에 빈자리가 있다면 슬롯에 추가, 인벤에서 -1
        int i = 0;
        for (; i < slots.Length; i++)
        {
            //슬롯에 빈자리가 있나 확인
            if (slots[i].item == null)
            {
                break;
            }
        }
        if (i == slots.Length)
        {
            Debug.LogError("슬롯에 더이상 자리 없다고");
            return -1;
        }
        else
        {
            //잘 되는 경우
            slots[i].item = _item;
            return i;
        }
    }
    public void RemoveItem(int _index)
    {
        //인벤토리를 우클릭하거나 슬롯을 클릭하면 실행됨
        //1. 슬롯의 아이템을 null로 바꿈
        //2. 인벤토리에 아이템을 다시 +1

        if (slots[_index].item != null)
        {
            Item _item = slots[_index].item;
            slots[_index].item = null;
        }
        else
        {
            Debug.LogError("슬롯에 아이템이 없는데 아이템 삭제 시도");
        }

    }
    #endregion
}
