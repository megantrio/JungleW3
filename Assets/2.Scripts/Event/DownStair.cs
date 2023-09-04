using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DownStair : MonoBehaviour
{
    public GameObject map1f;
    public GameObject mapBf;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Invoke("MapDown", 0.2f);
        }
    }

    public void MapDown()
    {
        //map1f.SetActive(false);
        //mapBf.SetActive(true);
    }

}
