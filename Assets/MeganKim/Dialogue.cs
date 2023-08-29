using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI speakerName;
    public TextMeshProUGUI descriptionObj;
    public string speker;
    public string[] description;


    public void SetSpekerName()
    {
        speakerName.SetText($"speker");
    }

    void Start()
    {
        TypingManager.instance.Typing(description, descriptionObj);
    }


}
