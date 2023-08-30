using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionPanel : MonoBehaviour
{
    public GameObject collectionObj;

    public void OpenCollection()
    {
        collectionObj.SetActive(true);
    }

    public void CloseCollection()
    {
        collectionObj.SetActive(false);
    }
    
}
