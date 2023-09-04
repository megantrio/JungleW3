using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    #region PublicVariables
    [Header("Item spec")]
    public bool isMixItem;
    public bool isSpecialItem;
    public int itemCount;
    public int applyDay;
    public string itemName;
    public Sprite itemImage;
    public string itemInfo;
    #endregion

    #region PrivateVariables
    #endregion

    #region PublicMethod
    #endregion

    #region PrivateMethod
    #endregion
}
