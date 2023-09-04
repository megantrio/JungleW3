using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixEvent : EventObject
{
    //�ͽ� �̺�Ʈ: �ͽ� UI�� ����ݴϴ�.
    //�ͽ��� ����Ǹ� �ͽ� UI�� �����մϴ�.
    public GameObject uiManager;

    private void Awake()
    {
        uiManager.SetActive(false);
    }

    public override void StartEvent()
    {
        StartCoroutine(Mix());
    }

    IEnumerator Mix()
    {
        uiManager.SetActive(true);
        while (uiManager.activeSelf)
        {
            yield return null;
        }
        PostEventEnded();
    }
}
