using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItem : MonoBehaviour, IObjectItem
{
    public Item _item;

    public Item GetItem()
    {
        return _item;
    }
    #region PublicVariables
    #endregion

    #region PrivateVariables
    #endregion

    #region PublicMethod
    #endregion

    #region PrivateMethod
    void OnValidate()
    {
        GetComponent<SpriteRenderer>().sprite = _item.itemImage;
         
    }
    #endregion
}
