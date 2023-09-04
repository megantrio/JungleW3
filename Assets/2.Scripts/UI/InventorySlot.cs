using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InventorySlot : MonoBehaviour
{
    public Inventory inventory;
    [Header("Management of Slot UI in Inventory")]
    [SerializeField] private Image image;

    public Item item
    {
        get { return _item; }
        set
        {
            //item에 할당 시 자동으로 활성화
            _item = value;
            if (_item != null)
            {
                image.sprite = item.itemImage;
                image.color = new Color(1, 1, 1, 1);
            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
            }
        }
    }

    private Item _item;

    public void OnLeftClick()
    {
        if (item == null) return;

        if (item.isSpecialItem && !item.isMixItem)
        {
            if (item != null)
            {
                //item.itemCount--;
            }
        }

        else if (item.isSpecialItem && item.isMixItem)
        {
            if(item.itemName == "얇고 가벼운 금붙이")
            {
                UIManager.Instance.MixItemPlus(item);
                inventory.RemoveItem(item);
                return;
            }

            else if (item != null)
            {
                UIManager.Instance.itemInfoUpdate(item);
                return;
            }
        }
        UIManager.Instance.MixItemPlus(item);

        if (inventory._isDeleteItem || (item.isSpecialItem && item.itemCount <= 0))
        {
            inventory.RemoveItem(item);
        }
    }
    
    public void OnRightClick()
    {
        UIManager.Instance.itemInfoUpdate(item);
    }
}