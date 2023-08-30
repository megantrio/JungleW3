using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager_Fix : MonoBehaviour
{
    //최상위 Event는 켜져있고, 그 아래는 꺼져있어야한다.


    #region PublicVariables
    public static EventManager_Fix _instance;

    [System.Serializable]
    public class dialLogic
    {
        public string speaker;
        public string[] description;
    }

    #endregion

    [HideInInspector] public bool isNightOn;
    [HideInInspector] public bool isEventOn;
    public dialLogic[] dialLogics;

    #region PrivateVariables

    [SerializeField] List<nowEvent> nowEventDay;

    private bool isOutLoad;
    private int nowEventCount;
    private int maxEventCount;
    private int dialNum;
    private int nowDays;


    //현재 진행중 이벤트
    [SerializeField] private GameObject dialouge_Parent;
    [SerializeField] private Dialogue_Fix dialogue_Fix;
    [SerializeField] Transform[] pos;
    [SerializeField] GameObject[] _target;
    [SerializeField] private float npcSpeed;
    [SerializeField] GameObject target;
    #endregion

    #region PublicMethod
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        isOutLoad = false;
        dialNum = 0;
        isEventOn = false;
        isNightOn = false;
        nowDays = 0;
        nowEventCount = 0;
    }

    private void Update()
    {
        CycleOn();
    }

    private void CycleOn()
    {
        if (isEventOn || isNightOn) return;

        switch (nowEventDay[nowEventCount])
        {
            case nowEvent.DayOn:
                nowDays++;
                nowEventCount++;
                break;
            case nowEvent.Dial:
                nowEventCount++;

                dialogue_Fix.speaker = dialLogics[dialNum].speaker;
                dialogue_Fix.description = dialLogics[dialNum].description;
                dialogue_Fix.ActiveSelf();
                isEventOn = true;
                dialNum++;
                break;
            case nowEvent.NPCCome:
                isEventOn = true;
                isOutLoad = false;
                InitPref();
                target.SetActive(true);
                StartCoroutine(Move(pos[0]));
                break;
            case nowEvent.NPCOut:
                isEventOn = true;
                isOutLoad = true;
                Debug.Log($"Now Npc Speed : {npcSpeed}");
                StartCoroutine(Move(pos[1]));
                break;
            case nowEvent.Night:
                nowEventCount++;
                isEventOn = true;
                isNightOn = true;
                isNight();
                break;
        }
    }

    private void InitPref()
    {
        target = Instantiate(_target[nowDays], pos[1]);
    }


    IEnumerator Move(Transform moveTo)
    {
        while (true)
        {
            target.transform.position = Vector3.MoveTowards(target.transform.position, moveTo.position, npcSpeed * Time.deltaTime);

            if ((target.transform.position == moveTo.position))
            {
                nowEventCount++;
                isEventOn = false;
                if(isOutLoad) { target.SetActive(false); }
                break;
            }
            yield return null;
        }
    }

    private void isNight()
    {
        Debug.Log("Night Event On");
    }

    #endregion

    public enum nowEvent
    {
        DayOn,
        Dial,
        NPCCome,
        NPCOut,
        Night
    }
}
