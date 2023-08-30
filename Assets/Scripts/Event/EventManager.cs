using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    #region PublicVariables
    public bool isEventEnded = false;

    public List<GameObject> events = new List<GameObject>();
    #endregion

    #region PrivateVariables
    private static EventManager _instance;

    //현재 진행중 이벤트
    private GameEvent _currentEvent;
    private Queue<GameEvent> gameEventQueue = new Queue<GameEvent>();       //현재의 모든이벤트가 끝나면 Queue의 다음 이벤트가 실행됨
    #endregion

    #region PublicMethod

    private void OnEnable()
    {
        //초기화
        StartCoroutine(StartEvents());
    }

    private IEnumerator StartEvents()
    {
        isEventEnded = false;
        for(int i=0;i<events.Count;i++)
        {
            events[i].SetActive(true);
            //이벤트가 끝날때까지 기다립니다.
            while (events[i].activeSelf)
            {
                yield return null;
            }
        }
        //이벤트가 모두 끝났다면
        isEventEnded = true;
        gameObject.SetActive(false);
    }

    #endregion
}
