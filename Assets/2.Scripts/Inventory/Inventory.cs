using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //����� ���� ������ ����Ʈ, UI ��θ� ������.
    #region PublicVariables
    [Header("About Inventory Data")]
    public List<Item> items = new List<Item>();     //���� �������� ������ ���� �Ŵ���
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
            Debug.Log("(" + _item.itemName + ") ������ �߰�, ���� : " + count);
            if (count == 1)
            {
                items.Add(_item);
            }
            else
            {
                //������ ���� �ö󰡰� �ϱ�
            }
            RefreshInventoryUI();
            return true;
        }
        else
        {
            Debug.LogError("������ ������ ���� ��");
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
            //�� ���Ҵµ� ���� �������� �����Ƿ� ���� �Ұ�
            //return false;
        }
        int itemCount = PlayerPrefs.GetInt(_item.itemName);
        itemCount -= 1;
        PlayerPrefs.SetInt(_item.itemName, itemCount);
        Debug.Log("������ ����: " + _item.itemName + " ���� �� ����: " + itemCount);
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
