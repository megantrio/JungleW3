using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class Dialogue : MonoBehaviour
{
    public string speaker;
    public string[] description;

    void OnEnable()
    {
        StartCoroutine(StartType());
        //AkSoundEngine.PostEvent("Dialogue", gameObject);
    }

    IEnumerator StartType()
    {
        for (int i = 0; i < description.Length; i++)
        {
            Debug.Log("d");
            yield return TypingManager.instance.Typing(speaker, description[i]);           
        }
        TypingManager.instance.CloseTypeUI();
        gameObject.SetActive(false);
    }

}
