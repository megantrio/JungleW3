using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemMixer : MonoBehaviour
{
    //현재는 실제 아이템 리스트, UI 모두를 관리함.
    #region PublicVariables
    public Inventory inventory;
    #endregion

    #region PrivateVariables
    [Header("About ItemMixer UI")]
    [SerializeField] private Transform slotParent;
    [SerializeField] public ItemMixerSlot[] slots;
    public Item resultOfMix;

    private InventorySlot[] inventorySlots;

    private List<Dictionary<string, object>> data;
    #endregion

    #region PublicMethod

    public int AddItem(Item _item)
    {
        //믹서가 켜져있을 때 인벤토리의 아이템을 클릭하면 실행됨
        //선택한 아이템이 매개변수로 들어온다.
        //1. 슬롯에 빈자리가 있나 확인
        //2. 슬롯에 빈자리가 있다면 슬롯에 추가, 인벤에서 -1
        int i = 0;
        for(;i<slots.Length; i++)
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

    public void MixItem()
    {
        Debug.Log("믹스!!!!!");
        int i = 0;
        for (; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                break;
            }
        }
        if(i== slots.Length)
        {
            Debug.Log("획득 + " + MixItem(slots[0].item.itemName, slots[1].item.itemName));
            //모든 슬롯에 아이템이 있으므로, 믹스 시작
            Debug.Log("믹스 시작");
            //현재 등록된 모든 아이템을 삭제함
            for(int j=0;j< slots.Length; j++)
            {
                RemoveItem(j);
            }
            //아이템 획득 구현
        }
        else
        {
            //모든 슬롯에 아이템이 있진 않음, 믹스 실패
            Debug.Log("믹스 실패... 모든 슬롯에 아이템이 있나 확인해주세요.");
        }
    }

    public void LoadMixDataFromCSV()
    {
        
    }

    public string MixItem(string a, string b)
    {
        string t1 = "table1";
        string t2 = "table2";
        string t3 = "table3";
        if(data == null)
        {
            CSVReader.Read("Database/MixData");
        }
        foreach(var i in data)
        {
            if (i[t1].Equals(a) && i[t2].Equals(b)
                || i[t1].Equals(b) && i[t2].Equals(a))
            {
                return i[t3].ToString();
            }
        }
        return "";
    }
    #endregion

    #region PrivateMethod
    private void OnValidate()
    {
        slots = GetComponentsInChildren<ItemMixerSlot>();
        inventorySlots = new InventorySlot[slots.Length];
        for(int i=0;i<slots.Length;i++)
        {
            slots[i].index = i;
            slots[i].itemMixer = this;
        }
    }

    private void Awake()
    {
        data = CSVReader.Read("Database/MixData");
    }


    #endregion
}
