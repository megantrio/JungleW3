using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemGet : MonoBehaviour
{
    #region PublicVariables
    public List<Item> items = new List<Item>();
    public SpriteRenderer[] playerEquipItems;

    public GameObject itemGetUI;
    public Image itemGetUIImage;
    public TMP_Text itemGetUIText;
        
        
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
                //1. UI ∂ÁøÏ±‚
                itemGetUIText.text = _i.itemName;
                itemGetUIImage.sprite = _i.itemImage;
                itemGetUI.SetActive(true);
                //2. prefs ¿˙¿Â
                if (PlayerPrefs.HasKey(_i.itemName))
                {
                    PlayerPrefs.SetInt(_i.itemName, PlayerPrefs.GetInt(_i.itemName)+1);
                }
                else
                {
                    PlayerPrefs.SetInt(_i.itemName, 1);
                }
            }
            else
            {
                Debug.Log("æ∆¿Ã≈€ ºˆ ∫Œ¡∑");
            }
        }
    }
    #endregion
}
