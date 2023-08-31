using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOutCheck : MonoBehaviour
{  
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && MorningEventManager.instance.isEventEnded)
        {
            MorningEventManager.instance.CallSceneChange();
        }
    }
}
