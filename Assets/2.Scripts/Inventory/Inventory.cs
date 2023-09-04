using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool _isDeleteItem;

    //현재는 실제 아이템 리스트, UI 모두를 관리함.
    #region PublicVariables
    [Header("About Inventory Data")]
    private bool isHaveItem;
    public List<Item> items = new List<Item>();     //실제 아이템을 가지고 있을 매니저
    #endregion

    #region PrivateVariables
    [SerializeField] private Transform slotParent;
    public InventorySlot[] slots;
    [SerializeField] public ItemMixer itemMixer;

    #endregion

    private void Start()
    {
        RefreshInventoryUI();
    }

    #region Test
    public void AddItem(Item _item)
    {        
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null && slots[i].item.isMixItem && slots[i].item.Equals(_item))
            {                
                items.Add(_item);
                Debug.Log("ItemAdd");
            }
            else
            {
                isHaveItem = true;
            }
        }

        isHaveItem = false;
        if (!isHaveItem && items.Count < slots.Length)
        {
            Debug.Log("ItemAdd");
            items.Add(_item);

            RefreshInventoryUI();
        }

        else
        {
            Debug.LogError("아이템 슬롯이 가득 참");
        }
    }

    public void ResetInven()
    {
        for (int i = items.Count - 1; i >= 0; i--)
        {
            items.Remove(items[i]);
        }
        RefreshInventoryUI();
    }

    public void RemoveItem(Item _item)
    {
        int i = 0;
        for(; i < items.Count; i++)
        {
            if (items[i].itemName.Equals(_item.itemName))
            {
                break;
            }
        }
       
        items.RemoveAt(i);

        RefreshInventoryUI();
    }
    #endregion

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

    private void OnValidate()
    {
        slots = GetComponentsInChildren<InventorySlot>();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].inventory = this;
        }
    }
}