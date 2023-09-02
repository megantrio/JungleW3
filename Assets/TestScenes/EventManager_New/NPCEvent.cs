using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class NPCEvent : EventObject
{
    //대사 관련
    public List<string> speaker = new List<string>();
    public List<string> description = new List<string>();

    //이동 관련
    private Vector3 start = new Vector3(-1.4f, -4.5f);
    private Vector3 end = new Vector3(0.2f, -1.15f);
    public float speed = 5f;



    public void Start()
    {
        StartCoroutine(MainEvent());
    }

    IEnumerator MainEvent()
    {
        //이동
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
        //대화 이벤트 발생

        for (int i = 0; i < description.Count; i++)
        {
            yield return TypingManager.instance.Typing(speaker[i], description[i]);
        }
        if(TypingManager.instance != null)
        {
            TypingManager.instance.CloseTypeUI();
        }
        //다시 돌아감
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
