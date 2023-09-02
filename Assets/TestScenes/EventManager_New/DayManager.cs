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
            Debug.LogError("DayManager�� 2�� �̻��Դϴ�.");
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
            //�̺�Ʈ�� �������� �̺�Ʈ�� �ϳ� ����.
            _currentEvent.gameObject.SetActive(false);
            eventQueue.Dequeue();

            //�̺�Ʈ�� �����ִٸ� 
            if(eventQueue.Count > 0)
            {
                _currentEvent = eventQueue.Peek();
                _currentEvent.gameObject.SetActive(true);
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
        if(currentState == DayState.MORNING && day == 7)
        {
            //7���� ��ħ�� �������Ƿ� ���� ����
            Debug.Log("���� ����");
            gameObject.SetActive(false);
            return;
        }
        if(currentState == DayState.MORNING)
        {
            currentState = DayState.NIGHT;
            Debug.Log("�� �ð� ����");
            InitNight();
        }
        else if(currentState == DayState.NIGHT)
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
        //�� �̺�Ʈ ť�� �� �־��ֱ�
    }

    public void InitMorning()
    {
        return;
        //���� �Ǹ� NPC �����͸� �ҷ�����, �̺�Ʈ ����Ʈ�� �߰��մϴ�.
        List<Dictionary<string, object>> dic = CSVReader.Read("Database/NPCData");
        foreach(var kvp in dic)
        {
            foreach(var j in kvp)
            {
                Debug.Log(j.Key + ", " +  j.Value);
            }
        }
    }


    //�̺�Ʈ ����
    public void SetEventEnded(EventObject endedEvent)
    {
        //� �̺�Ʈ�� ����Ǿ����� ȣ��Ǹ�, �ش� �̺�Ʈ�� ���� �̺�Ʈ�� �����մϴ�.
        //���� �������� Update���� ����˴ϴ�.
        if (_currentEvent == endedEvent)
        {
            isEventEnded = true;
        }
        else
        {
            Debug.LogError("���� �̺�Ʈ�� �ƴ� ������Ʈ�� �̺�Ʈ ���Ḧ ��û�Ͽ����ϴ�.");
        }
    }
}
