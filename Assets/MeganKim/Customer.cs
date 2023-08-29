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
    public GameObject[] customerPrefab;
    GameObject customer;

    public Vector2 spawnPos;   
    int curruntCustomer = 0;

    void Start()
    {
        StartCoroutine(CustomerSpawn());
    }

    public IEnumerator CustomerSpawn()
    {
        //GameFlowManager.currentTime == GameTime.AFTERNOON
        while (maxCustomer > curruntCustomer)
        {
            yield return new WaitForSeconds(spawnDuration);
            CustomerSpawner();         
            curruntCustomer++;
            Debug.Log("CurruntCustomer is : " +  curruntCustomer);
            
        }
    }

    public void CustomerSpawner()
    {
        int index = Random.Range(0, customerPrefab.Length);
        Vector3 spawnPosition3D = new Vector3(spawnPos.x, spawnPos.y, transform.position.z);
        customer = Instantiate(customerPrefab[index], spawnPosition3D, Quaternion.identity);

    }
}
