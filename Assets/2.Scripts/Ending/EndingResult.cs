using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndingResult : MonoBehaviour
{
    #region PublicVariables
    #endregion

    #region PrivateVariables
    #endregion


    private TMP_Text text;
    public ItemAssetList mixedAssetList;
    public void Start()
    {
        int k = 0;
        text = GetComponent<TMP_Text>();
        foreach (var item in mixedAssetList.items)
        {
            if (PlayerPrefs.HasKey(item.itemName) && PlayerPrefs.GetInt(item.itemName) > 0)
            {
                k += 1;
            }
        }
        text.text = "����� ������ ��\n������ ������ "+k+"��\n�Դϴ�.";
    }
}
