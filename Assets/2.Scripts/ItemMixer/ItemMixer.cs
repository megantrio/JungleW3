using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class ItemMixer : MonoBehaviour
{
    //현재는 실제 아이템 리스트, UI 모두를 관리함.
    #region PublicVariables
    #endregion

    #region PrivateVariables
    [Header("믹스 후 아이템 획득 UI")]
    public int a;
    public GameObject mixerUI;
    public TMP_Text mixerTextUI;
    public List<Dictionary<string, object>> mixData;

    [Header("파일로 저장된 스크립터블오브젝트")]
    //저장되어있는 ScriptableObject
    public ItemAssetList mixedItemAssetList;
    public ItemAssetList specialMixItemList;
    public Item kkwangItemAsset;

    [Header("VFX")]
    [SerializeField] private Transform parent;
    public GameObject success;
    public GameObject specialSuccess;
    public GameObject failure;
    #endregion

    #region PublicMethod

    public void OnParticleSystemStopped()
    {
        Transform[] childList = parent.GetComponentsInChildren<Transform>();

        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform) { Destroy(childList[i].gameObject); }
            }
        }
    }


    public IEnumerator ParticleMaker(GameObject a)
    {
        Debug.Log("Particle");
        float t;
        if (a == success)
            t = 0.7f;
        else
            t = 0.5f;
        GameObject r = Instantiate(a, parent);
        
        r.transform.localScale *= 1.5f;
        r.transform.position = gameObject.transform.position;
        yield return new WaitForSeconds(t);
        Destroy(r);
    }

    public Item MixItem(Item _item1, Item _item2)
    {

        //테이블의 첫 열
        string t1 = "table1";
        string t2 = "table2";
        string t3 = "table3";
        if (mixData == null)
        {
            mixData = CSVReader.Read("Database/MixData");
        }
        string result = "";

        if (mixData == null) Debug.LogError("mixDatanull!!");


        foreach (var i in mixData)
        {
            if (i[t1].Equals(_item1.itemName) && i[t2].Equals(_item2.itemName)
                || i[t1].Equals(_item2.itemName) && i[t2].Equals(_item1.itemName))
            {
                result = i[t3].ToString();
            }
        }

        UIManager.Instance.isMixed = true;


        if (result.Equals(""))
        {
            StartCoroutine(ParticleMaker(failure));
            AkSoundEngine.PostEvent("Mang", gameObject);
            itemReset(kkwangItemAsset);          
            return kkwangItemAsset;
        }

        if ((_item1.isSpecialItem || _item2.isSpecialItem))
        {
            for (int i = 0; i < specialMixItemList.items.Length; i++)
            {
                if(result == specialMixItemList.items[i].itemName)
                {
                    StartCoroutine(ParticleMaker(specialSuccess));
                    AkSoundEngine.PostEvent("Result", gameObject);
                    itemReset(specialMixItemList.items[i]);
                    DataManager.SetNPCCondition(specialMixItemList.items[i].itemName, true);                    
                    return specialMixItemList.items[i];
                }            
            }
        }


        for (int i = 0; i < mixedItemAssetList.items.Length; i++)
        {
            if (mixedItemAssetList.items[i].itemName.Equals(result))
            {
                success.SetActive(true);
                AkSoundEngine.PostEvent("Result", gameObject);
                StartCoroutine(ParticleMaker(success));
                itemReset(mixedItemAssetList.items[i]);
                DataManager.SetNPCCondition(mixedItemAssetList.items[i].itemName, true);
                return mixedItemAssetList.items[i];
            }
        }
        Debug.LogError("조합식은 존재하지만, 해당하는 아이템이 존재하지 않습니다.");
        return null;
    }

    public void itemReset(Item item)
    {
        UIManager.Instance.MixItemCheck(item);
        UIManager.Instance.isMixed = true;
        UIManager.Instance.MixItemReset();
    }

    #endregion

    #region PrivateMethod

    private void Awake()
    {
        //조합데이터 로드
        mixData = CSVReader.Read("Database/Mixdata");
    }


    #endregion
}
