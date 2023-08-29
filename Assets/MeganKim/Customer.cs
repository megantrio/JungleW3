using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    //1. 낮 동안만 스폰이 되어야 함
    //2. 스폰 시간 및 Max customer 선언 필요
    //3. 

    public float spawnDuration;
    public int maxCustomer;
    public GameObject[] CustomerPrefabs;
    public Vector2 spawnpos;

    int curruntCustomer = 0;

    void Start()
    {
        StartCoroutine(CustomerSpawn());
    }

    IEnumerator CustomerSpawn()
    {
        //GameFlowManager.currentTime == GameTime.AFTERNOON
        while (maxCustomer >= curruntCustomer)
        {
            yield return new WaitForSeconds(spawnDuration);
            CustomerSpawn ();
            
            curruntCustomer++;
        }
    }

    public void CustomerSpawner(GameObject CustomerPrefabs, Vector2 spawnpos)
    {
        GameObject customer = Instantiate(CustomerPrefabs);
        customer.transform.position = spawnpos;   
    }

}
