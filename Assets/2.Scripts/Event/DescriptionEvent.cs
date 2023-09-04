using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionEvent : EventObject
{
    //�ܼ��� ��ȭ �̺�Ʈ�Դϴ�.
    [Header("�ܼ��� ��ȭ �̺�Ʈ�� �߻���ŵ�ϴ�.")]
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
            Debug.LogError("TypingManager �����ϴ�.");
            yield break;
        }
        for(int i=0;i<description.Length; i++)
        {
            yield return TypingManager.instance.Typing(speaker, description[i]);
        }
        Debug.Log("��ȭ end");
        
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
