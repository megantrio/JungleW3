using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMove : MonoBehaviour
{
    //�̺�Ʈ�� �ִٰ� ����
    [Header("�̵���ų ������Ʈ�� �����մϴ�. None�ϰ�� �ڱ� �ڽ��� �����մϴ�.")]
    public GameObject target;
    [Header("�̵��� ��ġ�� �����մϴ�. startPos�� None�� ��� target�� ���� ��ġ���� �����մϴ�.")]
    public Transform startPos;
    [Header("�̵��� ��ġ�� �����մϴ�. endPos�� None�� ��� �� ������Ʈ�� ��ġ�� �����˴ϴ�.")]
    public Transform endPos;
    [Header("�̵� �ӵ��� �����մϴ�.")]
    public float speed = 5f;

    public void OnEnable()
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
        //���� ��ġ ����
        target.transform.position = startPos.position;
        //�̵� ����
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

        //�̵� ����
        gameObject.SetActive(false);
    }

    //UIManager ���ؼ� �̺�Ʈ �����ϴ� ����
}