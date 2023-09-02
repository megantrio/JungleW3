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

    //�̺�Ʈ�� �����մϴ�.
    private bool isEventEnded = false;
    private EventObject currentEvent
    {
        get { return eventQueue.Peek(); }
    }

    //���� ����� NPC�� ������
    public NPCEvent npcPrefab;
    public GameObject descriptionUIPrefab;

    //���� �������� �̺�Ʈ ť
    private Queue<EventObject> eventQueue = new Queue<EventObject>();
    //��ħ �̺�Ʈ��: NPCData.csv���� �ҷ��ɴϴ�.
    private List<EventObject>[] morningEvents = new List<EventObject>[10];

    private List<Dictionary<string, object>> _rawNPCData;

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

        //NPCData Load
        _rawNPCData = CSVReader.Read("Database/NPCData");
        for (int i = 0; i < morningEvents.Length; i++)
        {
            morningEvents[i] = new List<EventObject>();
        }
        foreach (var npcData in _rawNPCData)
        {
            object temp;
            npcData.TryGetValue("MoveType", out temp);
            if (temp.ToString().Equals("0"))
            {
                NPCEvent cur = Instantiate(npcPrefab, transform.position, Quaternion.identity);

                //Day
                npcData.TryGetValue("Day", out temp);
                int day = int.Parse(temp.ToString());
                morningEvents[day].Add(cur);
                //Sprite
                npcData.TryGetValue("Sprite", out temp);
                cur.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(temp.ToString());
                //Script
                npcData.TryGetValue("ScriptFile", out temp);
                string path = "Database/Descriptions/"+temp.ToString();
                npcData.TryGetValue("ScriptNum", out temp);
                string scriptType = temp.ToString();
                Debug.Log("��װ�: " + path);
                List<Dictionary<string, object>> descriptionRawData = CSVReader.Read(path);
                foreach(var row in descriptionRawData)
                {
                    object t;
                    row.TryGetValue("ID", out t);
                    if (scriptType.Equals(t.ToString()))
                    {
                        row.TryGetValue("Description", out t);
                        cur.description.Add(t.ToString());

                        row.TryGetValue("Speaker", out t);
                        cur.speaker.Add(t.ToString());
                    }
                }
                //Condition
                npcData.TryGetValue("Condition", out temp);
                cur.condition = temp.ToString();

                //���������� ��Ȱ��ȭ��
                cur.gameObject.SetActive(false);
            }

        }
    }

    private void Start()
    {
        InitMorning();
    }

    private void Update()
    {
        if (isEventEnded)
        {
            isEventEnded = false;
            //�̺�Ʈ�� ������ ��� ó��
            if (eventQueue.Count > 0)
            {
                eventQueue.Peek().gameObject.SetActive(false);
                eventQueue.Dequeue();
            }
            if (eventQueue.Count > 0)
            {
                eventQueue.Peek().gameObject.SetActive(true);
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
        //�� �̺�Ʈ ť�� �� �־��ֱ�
        isEventEnded = true;
    }

    public void InitMorning()
    {
        for (int i = 0; i < morningEvents[day].Count; i++)
        {
            eventQueue.Enqueue(morningEvents[day][i]);
        }
        if (eventQueue.Count > 0)
        {
            eventQueue.Peek().gameObject.SetActive(true);
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
