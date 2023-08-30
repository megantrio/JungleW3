using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class Dialogue_Fix : MonoBehaviour
{
    public string speaker;
    public string[] description;

    public bool isAlchemySuccess;

    public string[] description_True;
    public string[] description_False;
    void OnEnable()
    {
        StartCoroutine(StartType());
    }

    public void ActiveSelf()
    {
        Debug.Log("다이얼 활성화");
        gameObject.SetActive(true);
    }

    IEnumerator StartType()
    {
        for (int i = 0; i < description.Length; i++)
        {
            yield return TypingManager.instance.Typing(speaker, description[i]);
        }

        if(isAlchemySuccess) 
        {
            for (int i = 0; i < description.Length; i++)
            {
                yield return TypingManager.instance.Typing(speaker, description_True[i]);
            }
        }

        else
        {
            for (int i = 0; i < description.Length; i++)
            {
                yield return TypingManager.instance.Typing(speaker, description_False[i]);
            }
        }

        TypingManager.instance.CloseTypeUI();
        if(EventManager_Fix._instance.isEventOn) EventManager_Fix._instance.isEventOn = false;
        gameObject.SetActive(false);
    }

}
