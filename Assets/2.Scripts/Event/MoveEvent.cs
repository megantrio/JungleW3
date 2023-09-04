using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEvent : EventObject
{
    //이벤트가 있다고 가정
    [Header("이동시킬 오브젝트를 선택합니다. None일경우 자기 자신을 선택합니다.")]
    public GameObject target;
    [Header("이동할 위치를 선택합니다. startPos가 None일 경우 target의 현재 위치에서 시작합니다.")]
    public Transform startPos;
    [Header("이동할 위치를 선택합니다. endPos가 None일 경우 이 오브젝트의 위치로 설정됩니다.")]
    public Transform endPos;
    [Header("이동 속도를 결정합니다.")]
    public float speed = -5f;

    private void OnEnable()
    {
    }

    public override void StartEvent()
    {
        if(target == null)
        {
            target = gameObject;
        }
        if(startPos == null)
        {
            startPos = target.transform;
        }
        if(endPos == null)
        {
            endPos = transform;
        }
        StartCoroutine(Move());
    }



    IEnumerator Move()
    {       
        //시작 위치 설정
        target.transform.position = startPos.position;
        //이동 진행
        while (true)
        {
            target.transform.position = Vector3.MoveTowards(target.transform.position, endPos.position, speed*Time.deltaTime);
            //Debug.Log("Moving");
            if ((target.transform.position ==endPos.position))
            {
                break;
            }
            yield return null;
        }

        //이동 종료
        PostEventEnded();
    }

    //UIManager 통해서 이벤트 수행하는 로직
}
