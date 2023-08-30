using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMoveEvent : MonoBehaviour
{
    //�̺�Ʈ�� �ִٰ� ����
    public GameObject target;
    public Transform moveTo;
    public float speed = 1f;

    private void OnEnable()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {       
        //�÷��̾� ���� ����

        while (true)
        {
            target.transform.position = Vector3.MoveTowards(target.transform.position, moveTo.position, speed*Time.deltaTime);
         
            if ((target.transform.position ==moveTo.position))
            {
                break;
            }
            yield return null;
        }
        gameObject.SetActive(false);
    }

    //UIManager ���ؼ� �̺�Ʈ �����ϴ� ����
}
