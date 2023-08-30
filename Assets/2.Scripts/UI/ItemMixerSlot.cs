using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMixerSlot : MonoBehaviour
{
    #region PublicVariables
    public int index = -1;
    public ItemMixer itemMixer;
    [Header("Management of Slot UI in Inventory")]
    [SerializeField] private Image image;
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
    #endregion

    #region PrivateMethod

    public void OnClick()
    {
        //�ͼ� ������ Ŭ�� ��, ���� ���� ������ ������û
        //�κ��丮�� �������� +1
        
    }
    #endregion
}
