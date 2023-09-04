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

    //�̺�Ʈ�� �����մϴ�.
    private bool isEventEnded = false;
    private EventObject currentEvent
    {
        get { return eventQueue.Peek(); }
    }

    //���� ����� NPC�� ������
    [Header("���� ����� NPC�� �������Դϴ�.")]
    public NPCEvent npcPrefab;

    //���� �������� �̺�Ʈ ť
    private Queue<EventObject> eventQueue = new Queue<EventObject>();
    //��ħ �̺�Ʈ��: NPCData.csv���� �ҷ��ɴϴ�.
    private List<EventObject>[] morningNPCEvents = new List<EventObject>[10];

    //�� �ð����� Active�� ������Ʈ��
    [Header("���̳� �㿡�� ������ ������Ʈ�� �������ݴϴ�.")]
    public GameObject[] morningObjects;
    public GameObject[] nightObjects;

    [Header("�� ��¥�� ��ħ NPC �̺�Ʈ ��,�� �׸��� ���� �� �Ͼ �̺�Ʈ�� �������ݴϴ�.")]
    public DayEvents[] events = new DayEvents[MAX_DAYS+1];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("DayManager�� 2�� �̻��Դϴ�.");
        }
        DataManager.LoadAndCreateNPCData(morningNPCEvents, npcPrefab, transform.position);

        //��� ��ϵ� �̺�Ʈ ������Ʈ�� SetActive�� False�� �����մϴ�.
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
            //�̺�Ʈ�� ������ ��� ó��
            if (eventQueue.Count > 0)
            {
                //�̺�Ʈ�� �����Ѵ�
                eventQueue.Peek().gameObject.SetActive(false);
                eventQueue.Dequeue();
            }
            if (eventQueue.Count > 0)
            {
                //���� �̺�Ʈ�� �����ִٸ� �Ҵ�
                eventQueue.Peek().gameObject.SetActive(true);
                eventQueue.Peek().StartEvent();
            }
            else
            {
                //�� �̻� �̺�Ʈ�� ������ �̹� �ð��� ���̹Ƿ�, ���� �ð���� �Ѿ
                UpdateTime();
            }
        }

    }


    public void UpdateTime()
    {
        if (currentState == DayState.MORNING && day == 7)
        {
            //7���� ��ħ�� �������Ƿ� ���� ����
            Debug.Log("���� ����");
            gameObject.SetActive(false);
            return;
        }
        if (currentState == DayState.MORNING)
        {
            currentState = DayState.NIGHT;
            Debug.Log("�� �ð� ����");
            InitNight();
        }
        else if (currentState == DayState.NIGHT)
        {
            currentState = DayState.MORNING;
            day += 1;
            Debug.Log($"Day {day}, ����");
            Debug.Log("�� �ð� ����");
            InitMorning();
        }
    }

    public void InitNight()
    {
        //�� ������Ʈ False, �� ������Ʈ True
        foreach(var i in morningObjects)
        {
            i.gameObject.SetActive(false);
        }
        foreach(var i in nightObjects)
        {
            i.gameObject.SetActive(true);
        }

        //�� �̺�Ʈ ť�� �� �־��ֱ�
        for (int i = 0; i < events[day].nightEvents.Count; i++)
        {
            //1. �� ���ں� ��ħ ���� �� �̺�Ʈ ����
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
        //�� ������Ʈ False, �� ������Ʈ True
        foreach (var i in morningObjects)
        {
            i.gameObject.SetActive(true);
        }
        foreach (var i in nightObjects)
        {
            i.gameObject.SetActive(false);
        }
        //1. �� ���ں� ��ħ ���� �� �̺�Ʈ
        //2. �� ���ں� ��ħ NPC �̺�Ʈ
        //3. �� ���ں� ��ħ ���� �� �̺�Ʈ
        //4. �̺�Ʈ ����
        for (int i = 0; i < events[day].morningStartEvents.Count; i++)
        {
            //1. �� ���ں� ��ħ ���� �� �̺�Ʈ ����
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
            //1. �� ���ں� ��ħ ���� �� �̺�Ʈ ����
            eventQueue.Enqueue(events[day].morningEndEvents[i]);
        }

        //�̺�Ʈ�� �ִٸ� �ٷ� ����, ���ٸ� ����
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


    //�̺�Ʈ ����
    public void SetEventEnded(EventObject endedEvent)
    {
        //� �̺�Ʈ�� ����Ǿ����� ȣ��Ǹ�, �ش� �̺�Ʈ�� ���� �̺�Ʈ�� �����մϴ�.
        //���� �������� Update���� ����˴ϴ�.
        if (eventQueue.Peek() == endedEvent)
        {
            isEventEnded = true;
        }
        else
        {
            Debug.LogError("���� �̺�Ʈ�� �ƴ� ������Ʈ�� �̺�Ʈ ���Ḧ ��û�Ͽ����ϴ�.");
        }
    }
}
