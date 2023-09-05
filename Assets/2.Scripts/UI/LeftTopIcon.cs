using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftTopIcon : MonoBehaviour
{
    public GameObject dayIcon;
    public GameObject nightIcon;

    public void Update()
    {
        if(DayManager.instance != null)
        {
            if (DayManager.instance.currentState == DayManager.DayState.MORNING)
            {
                dayIcon.SetActive(true);
                nightIcon.SetActive(false);
            }
            else
            {
                dayIcon.SetActive(false);
                nightIcon.SetActive(true);
            }
        }
    }
}
