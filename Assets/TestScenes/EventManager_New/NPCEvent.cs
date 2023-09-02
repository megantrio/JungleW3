using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class DiscriptionBranch
{
    public string[] description;
    public string[] speaker;
}

public class NPCEvent : EventObject
{
    //��� ����
    public DiscriptionBranch discription;

    //�̵� ����
    private Vector3 start = new Vector3(-1.4f, -4.5f);
    private Vector3 end = new Vector3(0.2f, -1.15f);
    public float speed = 5f;

    //�б� ���� ������
    public string condition = "";


    public void Start()
    {
        StartCoroutine(MainEvent());
    }

    IEnumerator MainEvent()
    {
        //�̵�
        transform.position = start;
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, end, speed*Time.deltaTime);
            if(transform.position == end)
            {
                break;
            }
            yield return null;
        }
        //��ȭ �̺�Ʈ �߻�

        for (int i = 0; i < discription.description.Length; i++)
        {
            yield return TypingManager.instance.Typing(discription.speaker[i], discription.description[i]);
        }
        if(TypingManager.instance != null)
        {
            TypingManager.instance.CloseTypeUI();
        }
        //�ٽ� ���ư�
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, start, speed * Time.deltaTime);
            if (transform.position == start)
            {
                break;
            }
            yield return null;
        }
        PostEventEnded();
    }
}
