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

    public void OnClick()
    {
        //믹서 슬롯을 클릭 시, 현재 슬롯 아이템 삭제요청
        //인벤토리의 아이템을 +1
        
    }
    #endregion
}
