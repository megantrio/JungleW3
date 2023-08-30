using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DownStair : MonoBehaviour
{
    public GameObject map1f;
    public GameObject mapBf;

    private bool isUnderground = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            map1f.SetActive(false);
            bool isUnderground = true;
            mapBf.SetActive(true);
        }
        if(isUnderground == true && collision.gameObject.CompareTag("Player") )
        {
            mapBf.SetActive(false) ;
            bool isUnderground = false;
            map1f.SetActive(true) ;
        }
    }

}
