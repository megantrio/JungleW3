using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionUI : MonoBehaviour
{
    #region PublicVariables
    public ItemAssetList mixedItemAssetList;  //��ũ���ͺ������Ʈ
    public GameObject[] mixOnUIs;
    public GameObject[] mixOffUIs;
    #endregion

    #region PrivateVariables
    #endregion

    #region PublicMethod
    #endregion

    #region PrivateMethod

    private void Awake()
    {
        PlayerPrefs.DeleteAll();
    }

    private void OnEnable()
    {
        //1. ���� ������ ����
        //2. ���� �ִٸ� yes�� true
        //3. ���� ���ٸ� no�� true
        for(int i = 0; i < mixOnUIs.Length; i++)
        {
            if (PlayerPrefs.HasKey(mixedItemAssetList.items[i].itemName) && PlayerPrefs.GetInt(mixedItemAssetList.items[i].itemName)>0)
            {
                mixOnUIs[i].SetActive(true);
                mixOffUIs[i].SetActive(false);
            }
            else
            {
                mixOnUIs[i].SetActive(false);
                mixOffUIs[i].SetActive(true);
            }
        }
    }
    #endregion
}
