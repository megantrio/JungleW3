using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicNpcEvent : GameEvent
{
    public float speed = 5f;
    public string speakerName;
    public int branch = 0;

    [System.Serializable]
    public class DiscriptionBranch
    {
        public string[] description;
    }
    public DiscriptionBranch[] scriptList;
    private Vector3 start = new Vector3(-1.4f, -4.5f);
    private Vector3 end = new Vector3(0.2f, -1.15f);
    public override void StartEvent()
    {
        StartCoroutine(MainEvent());
    }

    IEnumerator MainEvent()
    {
        //�̵�
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

        for (int i = 0; i < scriptList[branch].description.Length; i++)
        {
            yield return TypingManager.instance.Typing(speakerName, scriptList[branch].description[i]);
        }
        TypingManager.instance.CloseTypeUI();
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
        EndEvent();
    }
}
