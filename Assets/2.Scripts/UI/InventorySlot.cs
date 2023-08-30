using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    #region PublicVariables
    public Inventory inventory;
    [Header("Management of Slot UI in Inventory")]
    [SerializeField] private Image image;

    [Header("About Mixer UI")]
    public ItemMixer itemMixer;
    public Item item
    {
        get { return _item; }
        set
        {
            //item�� �Ҵ� �� �ڵ����� Ȱ��ȭ
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

    #endregion

    #region PrivateVariables
    private Item _item;
    #endregion

    #region PublicMethod
    public void OnLeftClick()
    {
        //�ͼ��� �������� �� ������ Ŭ�� �� �ͼ� ����
        if (itemMixer.gameObject.activeSelf)
        {
            
        }
    }
    #endregion

    #region PrivateMethod

    private void Awake()
    {
    }
    #endregion
}
