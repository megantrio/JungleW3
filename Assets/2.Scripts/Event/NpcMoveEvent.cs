using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMoveEvent : MonoBehaviour
{
    //이벤트가 있다고 가정
    public GameObject target;
    public Transform moveTo;
    public float speed = 1f;

    private void OnEnable()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {       
        //플레이어 무브 시작

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

    //UIManager 통해서 이벤트 수행하는 로직
}
