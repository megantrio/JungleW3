using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionUI : MonoBehaviour
{
    #region PublicVariables
    public ItemAssetList mixedItemAssetList;  //스크립터블오브젝트
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
        //1. 현재 데이터 수집
        //2. 값이 있다면 yes를 true
        //3. 값이 없다면 no를 true
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
