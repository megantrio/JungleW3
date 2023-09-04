using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class DayEvents
{
    public List<EventObject> morningStartEvents;
    public List<EventObject> morningEndEvents;
    public List<EventObject> nightEvents;
}

public class DayManager : MonoBehaviour
{
    public const int MAX_DAYS = 7;
    public static DayManager instance;

    public int day = 0;
    public enum DayState
    {
        MORNING,
        NIGHT,
    }
    public DayState currentState = DayState.MORNING;

    //이벤트를 관리합니다.
    private bool isEventEnded = false;
    private EventObject currentEvent
    {
        get { return eventQueue.Peek(); }
    }

    //낮에 사용할 NPC의 프리팹
    [Header("낮에 사용할 NPC의 프리팹입니다.")]
    public NPCEvent npcPrefab;

    //현재 진행중인 이벤트 큐
    private Queue<EventObject> eventQueue = new Queue<EventObject>();
    //아침 이벤트들: NPCData.csv에서 불러옵니다.
    private List<EventObject>[] morningNPCEvents = new List<EventObject>[10];

    //각 시간에만 Active될 오브젝트들
    [Header("낮이나 밤에만 켜지는 오브젝트를 지정해줍니다.")]
    public GameObject[] morningObjects;
    public GameObject[] nightObjects;

    [Header("각 날짜별 아침 NPC 이벤트 전,후 그리고 저녁 시 일어날 이벤트를 지정해줍니다.")]
    public DayEvents[] events = new DayEvents[MAX_DAYS+1];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("DayManager가 2개 이상입니다.");
        }
        DataManager.LoadAndCreateNPCData(morningNPCEvents, npcPrefab, transform.position);

        //모든 등록된 이벤트 오브젝트의 SetActive를 False로 변경합니다.
        foreach (var i in events)
        {
            foreach (var j in i.morningStartEvents)
            {
                j.gameObject.SetActive(false);
            }
            foreach (var j in i.morningEndEvents)
            {
                j.gameObject.SetActive(false);
            }
            foreach (var j in i.nightEvents)
            {
                j.gameObject.SetActive(false);
            }
        }
    }

    private void Start()
    {
        if (currentState == DayState.MORNING)
        {
            InitMorning();
        }
        else if (currentState == DayState.NIGHT)
        {
            InitNight();
        }
    }



    private void Update()
    {
        if (isEventEnded)
        {
            isEventEnded = false;
            //이벤트가 공석인 경우 처리
            if (eventQueue.Count > 0)
            {
                //이벤트를 삭제한다
                eventQueue.Peek().gameObject.SetActive(false);
                eventQueue.Dequeue();
            }
            if (eventQueue.Count > 0)
            {
                //다음 이벤트가 남아있다면 켠다
                eventQueue.Peek().gameObject.SetActive(true);
                eventQueue.Peek().StartEvent();
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
        if (currentState == DayState.MORNING && day == 7)
        {
            //7일차 아침이 끝났으므로 게임 종료
            Debug.Log("게임 종료");
            gameObject.SetActive(false);
            return;
        }
        if (currentState == DayState.MORNING)
        {
            currentState = DayState.NIGHT;
            Debug.Log("밤 시간 시작");
            InitNight();
        }
        else if (currentState == DayState.NIGHT)
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
        //낮 오브젝트 False, 밤 오브젝트 True
        foreach(var i in morningObjects)
        {
            i.gameObject.SetActive(false);
        }
        foreach(var i in nightObjects)
        {
            i.gameObject.SetActive(true);
        }

        //밤 이벤트 큐에 값 넣어주기
        for (int i = 0; i < events[day].nightEvents.Count; i++)
        {
            //1. 각 일자별 아침 시작 시 이벤트 삽입
            eventQueue.Enqueue(events[day].nightEvents[i]);
        }
        if (eventQueue.Count > 0)
        {
            eventQueue.Peek().gameObject.SetActive(true);
            eventQueue.Peek().StartEvent();
        }
        else
        {
            isEventEnded = true;
        }
    }

    public void InitMorning()
    {
        //낮 오브젝트 False, 밤 오브젝트 True
        foreach (var i in morningObjects)
        {
            i.gameObject.SetActive(true);
        }
        foreach (var i in nightObjects)
        {
            i.gameObject.SetActive(false);
        }
        //1. 각 일자별 아침 시작 시 이벤트
        //2. 각 일자별 아침 NPC 이벤트
        //3. 각 일자별 아침 종료 시 이벤트
        //4. 이벤트 시작
        for (int i = 0; i < events[day].morningStartEvents.Count; i++)
        {
            //1. 각 일자별 아침 시작 시 이벤트 삽입
            eventQueue.Enqueue(events[day].morningStartEvents[i]);
        }
        for (int i = 0; i < morningNPCEvents[day].Count; i++)
        {
            if (DataManager.GetNPCCondition(morningNPCEvents[day][i].condition))
            {
                eventQueue.Enqueue(morningNPCEvents[day][i]);
            }
        }
        for (int i = 0; i < events[day].morningEndEvents.Count; i++)
        {
            //1. 각 일자별 아침 시작 시 이벤트 삽입
            eventQueue.Enqueue(events[day].morningEndEvents[i]);
        }

        //이벤트가 있다면 바로 시작, 없다면 삭제
        if (eventQueue.Count > 0)
        {
            eventQueue.Peek().gameObject.SetActive(true);
            eventQueue.Peek().StartEvent();
        }
        else
        {
            isEventEnded = true;
        }

    }


    //이벤트 관련
    public void SetEventEnded(EventObject endedEvent)
    {
        //어떤 이벤트가 종료되었을때 호출되며, 해당 이벤트의 다음 이벤트를 실행합니다.
        //다음 프레임의 Update에서 실행됩니다.
        if (eventQueue.Peek() == endedEvent)
        {
            isEventEnded = true;
        }
        else
        {
            Debug.LogError("현재 이벤트가 아닌 오브젝트가 이벤트 종료를 요청하였습니다.");
        }
    }
}
