using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndEvent : EventObject
{
    public override void StartEvent()
    {
        SceneManager.LoadScene("Ending");

    }
}
