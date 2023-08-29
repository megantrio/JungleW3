using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    //1. �� ���ȸ� ������ �Ǿ�� ��
    //2. ���� �ð� �� Max customer ���� �ʿ�
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
