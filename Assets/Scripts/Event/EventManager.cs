using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //최상위 Event는 켜져있고, 그 아래는 꺼져있어야한다.


    #region PublicVariables
    public bool isEventEnded = false;

    public List<GameObject> events = new List<GameObject>();
    #endregion

    #region PrivateVariables
    private static EventManager _instance;

    //현재 진행중 이벤트
    private GameEvent _currentEvent;
    private Queue<GameEvent> gameEventQueue = new Queue<GameEvent>();       //현재의 모든이벤트가 끝나면 Queue의 다음 이벤트가 실행됨
    
    private static Stack<GameObject> eventStack = new Stack<GameObject>();
    #endregion

    #region PublicMethod
    private void Awake()
    {
    }

    private void OnEnable()
    {
        StartCoroutine(StartEvents());
    }

    private IEnumerator StartEvents()
    {
        if (transform.parent!=null&&eventStack.Peek() != gameObject)
        {
            //부모가 나를 실행한 것이 아니라면 없애버린다.
            gameObject.SetActive(false);
            yield break;
        }
        Debug.Log(gameObject.name + "시작!");
        isEventEnded = false;
        for(int i=0;i<events.Count;i++)
        {
            eventStack.Push(events[i]);
            events[i].SetActive(true);
            while (events[i].activeSelf)
            {
                yield return null;
            }
            eventStack.Pop();
        }
        //이벤트가 모두 끝났다면
        isEventEnded = true;
        gameObject.SetActive(false);
        Debug.Log(gameObject.name + "종료!");
    }

    #endregion
}
