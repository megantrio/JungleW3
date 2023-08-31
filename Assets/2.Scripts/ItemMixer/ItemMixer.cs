using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class ItemMixer : MonoBehaviour
{
    //����� ���� ������ ����Ʈ, UI ��θ� ������.
    #region PublicVariables
    #endregion

    #region PrivateVariables
    [Header("�ͽ� �� ������ ȹ�� UI")]
    public GameObject mixerUI;
    public TMP_Text mixerTextUI;
    private List<Dictionary<string, object>> mixData;

    [Header("���Ϸ� ����� ��ũ���ͺ������Ʈ")]
    //����Ǿ��ִ� ScriptableObject
    public ItemAssetList mixedItemAssetList;
    public Item kkwangItemAsset;

    [Header("VFX")]
    public GameObject success;
    public GameObject failure;
    #endregion

    #region PublicMethod


    public void OnItemCheck(Item _item1, Item _item2)
    {

    }

    

    public IEnumerator ParticleMaker(GameObject a)
    {
        float t;
        if (a == success)
            t = 0.7f;
        else
            t = 0.5f;
        GameObject r = Instantiate(a, transform.position, Quaternion.identity);
        r.transform.localScale *= 1.5f;
        r.transform.position = gameObject.transform.position; 
        yield return new WaitForSeconds(t);
        Destroy(r);
    }

    public Item MixItem(Item _item1, Item _item2)
    {
        //���̺��� ù ��
        string t1 = "table1";
        string t2 = "table2";
        string t3 = "table3";
        if (mixData == null)
        {
            CSVReader.Read("Database/MixData");
        }
        string result = "";
        foreach (var i in mixData)
        {
            if (i[t1].Equals(_item1.itemName) && i[t2].Equals(_item2.itemName)
                || i[t1].Equals(_item2.itemName) && i[t2].Equals(_item1.itemName))
            {
                result = i[t3].ToString();
            }
        }
        if (result.Equals(""))
        {
            StartCoroutine(ParticleMaker(failure));
            AkSoundEngine.PostEvent("Result", gameObject);
            return kkwangItemAsset;
        }
        for (int i = 0; i < mixedItemAssetList.items.Length; i++)
        {
            if (mixedItemAssetList.items[i].itemName.Equals(result))
            {
                StartCoroutine(ParticleMaker(success));
                return mixedItemAssetList.items[i];
            }
        }
        Debug.LogError("���ս��� ����������, �ش��ϴ� �������� �������� �ʽ��ϴ�.");
        return null;
    }

    #endregion

    #region PrivateMethod

    private void Awake()
    {
        //���յ����� �ε�
        mixData = CSVReader.Read("Database/MixData");
        string a = "";
        foreach (var i in mixData)
        {
            foreach (var j in i)
            {
                a += j.Value.ToString() + " ";
            }

        }
        Debug.Log(a);

    }


    #endregion
}
