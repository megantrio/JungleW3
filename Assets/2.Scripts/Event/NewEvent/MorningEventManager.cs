using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MorningEventManager : MonoBehaviour
{
    
    public enum GameState
    {
        MORNING,
        NIGHT
    }

    public GameState state;

    public static MorningEventManager instance;
    public static int day = 0;
    public bool isEventEnded = false;
    public int currentEvent = 0;

    public GameObject[] morningObjects;
    public GameObject[] nightObject;  //밤에만 보이게 할 오브젝트 출현

    public List<GameEvent> morningEvents;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            isEventEnded = true;
            currentEvent = 0;
            state = GameState.MORNING;
        }
    }

    public void Update()
    {
        if (state == GameState.MORNING)
        {
            if (isEventEnded)
            {
                if (morningEvents.Count > currentEvent)
                {
                    morningEvents[currentEvent].StartEvent();
                    currentEvent += 1;
                    isEventEnded = false;
                }
                else
                {
                    foreach (var v in morningObjects)
                    {
                        v.SetActive(false);
                    }
                    state = GameState.NIGHT;
                    foreach(var v in nightObject)
                    {
                        v.SetActive(true);
                    }
                }
            }
        }
    }
}
