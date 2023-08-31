using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
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

    public GameObject successEffect;
    public GameObject failEffect;

    #region PrivateMethod
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Mixer"))
        {
            ItemMixer mixer = collision.gameObject.GetComponent<ItemMixer>();
            if (items.Count == 1)
            {
                //아이템 하나로 갈 경우 아이템 초기화
                items.Clear();
                
                for (int i = 0; i < playerEquipItems.Length; i++)
                {
                    playerEquipItems[i].gameObject.SetActive(false);
                }

                AkSoundEngine.PostEvent("Fail", gameObject);

            }
            if (items.Count == 2)
            {
                AkSoundEngine.PostEvent("Mix", gameObject);
                Item _i = mixer.MixItem(items[0], items[1]);
                Debug.Log("아이템 획득!! :"+_i.itemName);
                items.Clear();
                for(int i=0;i<playerEquipItems.Length;i++)
                {
                    playerEquipItems[i].gameObject.SetActive(false);
                }

                //아이템 획득 로직 작성
                //1. UI 띄우기
                itemGetUIText.text = _i.itemName;
                itemGetUIImage.sprite = _i.itemImage;
                itemGetUI.SetActive(true);
                
                //2. prefs 저장
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
                Debug.Log("아이템 수 부족");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            lastCollidedItem = collision.gameObject;
        }
        if (collision.CompareTag("Interactor"))
        {
            lastCollidedObject = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            lastCollidedItem = null;
        }
        if (collision.CompareTag("Interactor"))
        {
            lastCollidedObject = null;
        }
    }

    private GameObject lastCollidedItem;
    private GameObject lastCollidedObject;

    public GameObject newsUI;
    public GameObject listUI;
    public void GetItem(InputAction.CallbackContext callback)
    {
        if (callback.started)
        {
            if (lastCollidedItem != null)
            {
                Item _i = lastCollidedItem.GetComponent<FieldItem>().GetItem();
                if (items.Count < playerEquipItems.Length)
                {
                    playerEquipItems[items.Count].gameObject.SetActive(true);
                    playerEquipItems[items.Count].sprite = _i.itemImage;
                    items.Add(_i);

                    AkSoundEngine.PostEvent("Pickup", gameObject);
                }
            }
            if (lastCollidedObject != null)
            {
                //뉴스 표출
                if(newsUI != null)
                {
                    newsUI.SetActive(true);
                }
                if(listUI != null)
                {
                    listUI.SetActive(true);
                }
            }
        }
    }

    
    #endregion
}
