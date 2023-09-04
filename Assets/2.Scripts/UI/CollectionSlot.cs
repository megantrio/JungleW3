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

    //1. ������ Ű���尡 �ֳ� Ȯ��
    //2. ������ Ű���尡 ������ images�� �ش� �̹����� �ε���
    //3. ������ Ű���尡 ������ ?�� ǥ����

    public void Init()
    {
        //�ʱ�ȭ
        foreach (var i in images)
        {
            i.gameObject.SetActive(false);
        }
        foreach (var i in qs)
        {
            i.gameObject.SetActive(false);
        }

        //���� ���� UI�� ǥ��
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
