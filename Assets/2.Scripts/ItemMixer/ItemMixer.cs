using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemMixer : MonoBehaviour
{
    //����� ���� ������ ����Ʈ, UI ��θ� ������.
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
        //�ͼ��� �������� �� �κ��丮�� �������� Ŭ���ϸ� �����
        //������ �������� �Ű������� ���´�.
        //1. ���Կ� ���ڸ��� �ֳ� Ȯ��
        //2. ���Կ� ���ڸ��� �ִٸ� ���Կ� �߰�, �κ����� -1
        int i = 0;
        for(;i<slots.Length; i++)
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

    public void MixItem()
    {
        Debug.Log("�ͽ�!!!!!");
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
            Debug.Log("ȹ�� + " + MixItem(slots[0].item.itemName, slots[1].item.itemName));
            //��� ���Կ� �������� �����Ƿ�, �ͽ� ����
            Debug.Log("�ͽ� ����");
            //���� ��ϵ� ��� �������� ������
            for(int j=0;j< slots.Length; j++)
            {
                RemoveItem(j);
            }
            //������ ȹ�� ����
        }
        else
        {
            //��� ���Կ� �������� ���� ����, �ͽ� ����
            Debug.Log("�ͽ� ����... ��� ���Կ� �������� �ֳ� Ȯ�����ּ���.");
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
