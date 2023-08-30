using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBranch : MonoBehaviour
{
    public Item item;

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey(item.itemName) && PlayerPrefs.GetInt(item.itemName) > 0)
        {
            GetComponent<BasicNpcEvent>().branch = 1;
        }
        else
        {
            GetComponent<BasicNpcEvent>().branch = 0;
        }
    }
}
