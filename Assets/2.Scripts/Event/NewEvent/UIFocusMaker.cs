using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIFocusMaker : MonoBehaviour
{
    public GameObject connected;
    public bool isUseEsc = false;


    private void OnEnable()
    {
        //Time.timeScale = 0.0f;
        //OnDialogSkip();
    }


    private void OnDisable()
    {
        Time.timeScale = 1.0f;
    }

    public void OnDialogSkip()
    {
        Debug.Log("µÇ³ª¿ë");
        gameObject.SetActive(false);
    }

}
