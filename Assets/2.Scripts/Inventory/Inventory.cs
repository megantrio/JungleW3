using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //현재는 실제 아이템 리스트, UI 모두를 관리함.
    #region PublicVariables
    [Header("About Inventory Data")]
    public List<Item> items = new List<Item>();     //실제 아이템을 가지고 있을 매니저
    #endregion

    #region PrivateVariables
    [Header("About Inventory UI")]
    [SerializeField] private Transform slotParent;
    [SerializeField] private InventorySlot[] slots;
    [SerializeField] public ItemMixer itemMixer;
    #endregion

    #region PublicMethod

    public bool AddItem(Item _item)
    {
        if (items.Count < slots.Length)
        {
            int count = AddItemOnPlayerPrefs(_item);
            Debug.Log("(" + _item.itemName + ") 아이템 추가, 개수 : " + count);
            if (count == 1)
            {
                items.Add(_item);
            }
            else
            {
                //아이템 숫자 올라가게 하기
            }
            RefreshInventoryUI();
            return true;
        }
        else
        {
            Debug.LogError("아이템 슬롯이 가득 참");
            return false;
        }
    }

    public void RemoveItem(Item _item)
    {
        int i = 0;
        for (i = 0; i < items.Count; i++)
        {
            if (items[i].itemName.Equals(_item.itemName))
            {
                break;
            }
        }
        if (i == items.Count)
        {
            //다 돌았는데 같은 아이템이 없으므로 삭제 불가
            //return false;
        }
        int itemCount = PlayerPrefs.GetInt(_item.itemName);
        itemCount -= 1;
        PlayerPrefs.SetInt(_item.itemName, itemCount);
        Debug.Log("아이템 삭제: " + _item.itemName + " 삭제 후 수량: " + itemCount);
        if (itemCount == 0)
        {
            items.RemoveAt(i);
        }
        RefreshInventoryUI();
        //return true;
    }
    #endregion

    #region PrivateMethod
    private void Awake()
    {
        RefreshInventoryUI();
    }

    private void RefreshInventoryUI()
    {
        int i = 0;
        for (; i < items.Count && i < slots.Length; i++)
        {
            slots[i].item = items[i];
        }
        for (; i < slots.Length; i++)
        {
            slots[i].item = null;
        }
    }

    private int AddItemOnPlayerPrefs(Item _item)
    {
        if (PlayerPrefs.HasKey(_item.itemName))
        {
            int cur = PlayerPrefs.GetInt(_item.itemName);
            cur += 1;
            PlayerPrefs.SetInt(_item.itemName, cur);
            return cur;
        }
        else
        {
            PlayerPrefs.SetInt(_item.itemName, 1);
            return 1;
        }
    }


    private void OnValidate()
    {
        slots = GetComponentsInChildren<InventorySlot>();
        for(int i=0;i<slots.Length;i++)
        {
            slots[i].inventory = this;
            slots[i].itemMixer = itemMixer;
        }
    }

    public void Test_GetItem(Item _item)
    {
        AddItem(_item);
    }

    #endregion
}
