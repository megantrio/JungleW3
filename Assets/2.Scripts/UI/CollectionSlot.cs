using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MixExpression
{
    public string a;
    public string b;
    public string c;
}

public class CollectionSlot : MonoBehaviour
{
    public MixExpression expression;
    public CollectionUI_New myCollection;
    public Image[] images;
    public GameObject[] qs;

    //1. 지정된 키워드가 있나 확인
    //2. 지정된 키워드가 있으면 images에 해당 이미지들 로드함
    //3. 지정된 키워드가 없으면 ?를 표출함

    public void Init()
    {
        //초기화
        foreach (var i in images)
        {
            i.gameObject.SetActive(false);
        }
        foreach (var i in qs)
        {
            i.gameObject.SetActive(false);
        }

        //실제 사용될 UI만 표출
        if (DataManager.GetNPCCondition(expression.c))
        {
            images[0].sprite = myCollection.FindSpriteFronItemName(expression.a);
            images[1].sprite = myCollection.FindSpriteFronItemName(expression.b);
            images[2].sprite = myCollection.FindSpriteFronItemName(expression.c);
            foreach(var i in images)
            {
                i.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (var i in qs)
            {
                i.gameObject.SetActive(true);
            }
        }
    }
}
