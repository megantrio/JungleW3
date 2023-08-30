using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemGet : MonoBehaviour
{
    #region PublicVariables
    public List<Item> items = new List<Item>();
    public SpriteRenderer[] playerEquipItems;
    #endregion

    #region PrivateVariables
    #endregion

    #region PublicMethod
    #endregion

    #region PrivateMethod
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Item _i = collision.gameObject.GetComponent<FieldItem>().GetItem();
            if (items.Count < playerEquipItems.Length)
            {
                playerEquipItems[items.Count].gameObject.SetActive(true);
                playerEquipItems[items.Count].sprite = _i.itemImage;
                items.Add(_i);
                Destroy(collision.gameObject);
            }
        }

        if (collision.CompareTag("Mixer"))
        {
            ItemMixer mixer = collision.gameObject.GetComponent<ItemMixer>();
            if (items.Count == 2)
            {
                Item _i = mixer.MixItem(items[0], items[1]);
                Debug.Log("æ∆¿Ã≈€ »πµÊ!! :"+_i.itemName);
                items.Clear();
                for(int i=0;i<playerEquipItems.Length;i++)
                {
                    playerEquipItems[i].gameObject.SetActive(false);
                }

                //æ∆¿Ã≈€ »πµÊ ∑Œ¡˜ ¿€º∫
            }
            else
            {
                Debug.Log("æ∆¿Ã≈€ ºˆ ∫Œ¡∑");
            }
        }
    }
    #endregion
}
