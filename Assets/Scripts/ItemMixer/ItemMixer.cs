using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class ItemMixer : MonoBehaviour
{
    //����� ���� ������ ����Ʈ, UI ��θ� ������.
    #region PublicVariables
    public Inventory inventory;
    #endregion

    #region PrivateVariables
    [Header("�ͽ� �� ������ ȹ�� UI")]
    public GameObject mixerUI;
    public TMP_Text mixerTextUI;
    private List<Dictionary<string, object>> mixData;

    [Header("���Ϸ� ����� ��ũ���ͺ������Ʈ")]
    //����Ǿ��ִ� ScriptableObject
    public Item[] mixedItemAssets;
    public Item kkwangItemAsset;
    #endregion

    #region PublicMethod


    public void OnItemCheck(Item _item1, Item _item2)
    {

    }

    public Item MixItem(Item _item1, Item _item2)
    {
        //���̺��� ù ��
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
        Debug.LogError("���ս��� ����������, �ش��ϴ� �������� �������� �ʽ��ϴ�.");
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
        //���յ����� �ε�
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
        //�ͼ��� �������� �� �κ��丮�� �������� Ŭ���ϸ� �����
        //������ �������� �Ű������� ���´�.
        //1. ���Կ� ���ڸ��� �ֳ� Ȯ��
        //2. ���Կ� ���ڸ��� �ִٸ� ���Կ� �߰�, �κ����� -1
        int i = 0;
        for (; i < slots.Length; i++)
        {
            //���Կ� ���ڸ��� �ֳ� Ȯ��
            if (slots[i].item == null)
            {
                break;
            }
        }
        if (i == slots.Length)
        {
            Debug.LogError("���Կ� ���̻� �ڸ� ���ٰ�");
            return -1;
        }
        else
        {
            //�� �Ǵ� ���
            slots[i].item = _item;
            return i;
        }
    }
    public void RemoveItem(int _index)
    {
        //�κ��丮�� ��Ŭ���ϰų� ������ Ŭ���ϸ� �����
        //1. ������ �������� null�� �ٲ�
        //2. �κ��丮�� �������� �ٽ� +1

        if (slots[_index].item != null)
        {
            Item _item = slots[_index].item;
            slots[_index].item = null;
        }
        else
        {
            Debug.LogError("���Կ� �������� ���µ� ������ ���� �õ�");
        }

    }
    #endregion
}
