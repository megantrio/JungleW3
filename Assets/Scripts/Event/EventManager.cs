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

    //���� ������ �̺�Ʈ
    private GameEvent _currentEvent;
    private Queue<GameEvent> gameEventQueue = new Queue<GameEvent>();       //������ ����̺�Ʈ�� ������ Queue�� ���� �̺�Ʈ�� �����
    
    private static Stack<GameObject> eventStack = new Stack<GameObject>();
    #endregion

    #region PublicMethod

    private void OnEnable()
    {
        //�ʱ�ȭ
        StartCoroutine(StartEvents());
    }

    private IEnumerator StartEvents()
    {
        if (eventStack.Count>0&&eventStack.Peek() != gameObject)
        {
            gameObject.SetActive(false);
            yield break;
        }
        isEventEnded = false;
        for(int i=0;i<events.Count;i++)
        {
            eventStack.Push(events[i]);
            events[i].SetActive(true);
            //�̺�Ʈ�� ���������� ��ٸ��ϴ�.
            while (events[i].activeSelf)
            {
                yield return null;
            }
            eventStack.Pop();
        }
        //�̺�Ʈ�� ��� �����ٸ�
        isEventEnded = true;
        gameObject.SetActive(false);
    }

    #endregion
}
