using System.Collections;
using System.Collections.Generic;
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
            //��� ���Կ� �������� �����Ƿ�, �ͽ� ����
            Debug.Log("�ͽ� ����");
            //���� ��ϵ� ��� �������� ������
            for(int j=0;j< slots.Length; j++)
            {
                RemoveItem(j);
            }
            inventory.AddItem(resultOfMix);
        }
        else
        {
            //��� ���Կ� �������� ���� ����, �ͽ� ����
            Debug.Log("�ͽ� ����... ��� ���Կ� �������� �ֳ� Ȯ�����ּ���.");
        }
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


    #endregion
}
