using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionEvent : EventObject
{
    //단순한 대화 이벤트입니다.
    [Header("단순한 대화 이벤트를 발생시킵니다.")]
    public string speaker;
    public string[] description;
    public override void StartEvent()
    {
        StartCoroutine(PostScriptEvent());
    }

    public IEnumerator PostScriptEvent()
    {
        if (TypingManager.instance == null)
        {
            Debug.LogError("TypingManager 없습니다.");
            yield break;
        }
        for(int i=0;i<description.Length; i++)
        {
            yield return TypingManager.instance.Typing(speaker, description[i]);
        }
        Debug.Log("대화 end");
        
        PostEventEnded();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
