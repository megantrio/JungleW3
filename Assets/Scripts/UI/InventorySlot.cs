using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    #region PublicVariables
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
    #endregion

    #region PrivateVariables
    private Item _item;
    #endregion

    #region PublicMethod
    #endregion

    #region PrivateMethod
    #endregion
}
