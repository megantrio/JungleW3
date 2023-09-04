using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayTextUI : MonoBehaviour
{
    TMP_Text text;
    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }
    private void Update()
    {
        text.text = "Day " + DayManager.instance.day;
    }


}
