using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public static DayManager instance;

    public int day = 0;
    public enum DayState
    {
        MORNING,
        NIGHT,
    }
    public DayState currentState = DayState.MORNING;


    private bool isEventEnded = false;
    private EventObject _currentEvent;

    public Queue<EventObject> eventQueue = new Queue<EventObject>();

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("DayManager가 2개 이상입니다.");
        }
    }

    private void Start()
    {
        InitMorning();
    }

    private void Update()
    {        
        if(isEventEnded)
        {
            isEventEnded = false;
            //이벤트가 끝났으니 이벤트를 하나 뺀다.
            _currentEvent.gameObject.SetActive(false);
            eventQueue.Dequeue();

            //이벤트가 남아있다면 
            if(eventQueue.Count > 0)
            {
                _currentEvent = eventQueue.Peek();
                _currentEvent.gameObject.SetActive(true);
            }
            else
            {
                //더 이상 이벤트가 없으면 이번 시간은 끝이므로, 다음 시간대로 넘어감
                UpdateTime();
            }
        }

    }


    public void UpdateTime()
    {
        if(currentState == DayState.MORNING && day == 7)
        {
            //7일차 아침이 끝났으므로 게임 종료
            Debug.Log("게임 종료");
            gameObject.SetActive(false);
            return;
        }
        if(currentState == DayState.MORNING)
        {
            currentState = DayState.NIGHT;
            Debug.Log("밤 시간 시작");
            InitNight();
        }
        else if(currentState == DayState.NIGHT)
        {
            currentState = DayState.MORNING;
            day += 1;
            Debug.Log($"Day {day}, 시작");
            Debug.Log("낮 시간 시작");
            InitMorning();
        }
    }

    public void InitNight()
    {
        //밤 이벤트 큐에 값 넣어주기
    }

    public void InitMorning()
    {
        return;
        //낮이 되면 NPC 데이터를 불러오고, 이벤트 리스트에 추가합니다.
        List<Dictionary<string, object>> dic = CSVReader.Read("Database/NPCData");
        foreach(var kvp in dic)
        {
            foreach(var j in kvp)
            {
                Debug.Log(j.Key + ", " +  j.Value);
            }
        }
    }


    //이벤트 관련
    public void SetEventEnded(EventObject endedEvent)
    {
        //어떤 이벤트가 종료되었을때 호출되며, 해당 이벤트의 다음 이벤트를 실행합니다.
        //다음 프레임의 Update에서 실행됩니다.
        if (_currentEvent == endedEvent)
        {
            isEventEnded = true;
        }
        else
        {
            Debug.LogError("현재 이벤트가 아닌 오브젝트가 이벤트 종료를 요청하였습니다.");
        }
    }
}
