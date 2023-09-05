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

    int count = 0;
    public void Start()
    {
        int k = 0;
        text = GetComponent<TMP_Text>();
        //foreach (var item in mixedAssetList.items)
        //{
        //    if (PlayerPrefs.HasKey(item.itemName) && PlayerPrefs.GetInt(item.itemName) > 0)
        //    {
        //        k += 1;
        //    }
        //}

        var t = CollectionUI_New.mixExpressions;
        foreach(var e in t)
        {
            if (DataManager.GetNPCCondition(e.c))
            {
                count += 1;
            }
        }


        text.text = "당신이 제작한\n물품의 수는 "+t.Count+"개 중"+count+"개 입니다.";
    }
}
