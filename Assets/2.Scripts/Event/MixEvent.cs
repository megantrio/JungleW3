using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixEvent : EventObject
{
    //믹스 이벤트: 믹스 UI를 띄워줍니다.
    //믹스가 종료되면 믹스 UI를 종료합니다.
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
