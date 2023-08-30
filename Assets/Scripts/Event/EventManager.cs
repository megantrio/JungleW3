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
    #endregion

    #region PublicMethod

    private void OnEnable()
    {
        //�ʱ�ȭ
        StartCoroutine(StartEvents());
    }

    private IEnumerator StartEvents()
    {
        isEventEnded = false;
        for(int i=0;i<events.Count;i++)
        {
            events[i].SetActive(true);
            //�̺�Ʈ�� ���������� ��ٸ��ϴ�.
            while (events[i].activeSelf)
            {
                yield return null;
            }
        }
        //�̺�Ʈ�� ��� �����ٸ�
        isEventEnded = true;
        gameObject.SetActive(false);
    }

    #endregion
}
