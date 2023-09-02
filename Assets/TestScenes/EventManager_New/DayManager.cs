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

    //이벤트를 관리합니다.
    private bool isEventEnded = false;
    private EventObject currentEvent
    {
        get { return eventQueue.Peek(); }
    }

    //낮에 사용할 NPC의 프리팹
    public NPCEvent npcPrefab;
    public GameObject descriptionUIPrefab;

    //현재 진행중인 이벤트 큐
    private Queue<EventObject> eventQueue = new Queue<EventObject>();
    //아침 이벤트들: NPCData.csv에서 불러옵니다.
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
            Debug.LogError("DayManager가 2개 이상입니다.");
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
                Debug.Log("어그거: " + path);
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

                //생성했으니 비활성화함
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
            //이벤트가 공석인 경우 처리
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
        //밤 이벤트 큐에 값 넣어주기
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
