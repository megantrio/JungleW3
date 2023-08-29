using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    #region PublicVariables
    public enum GameTime
    {
        AFTER_NOON,
        NIGHT,
    }


    //�ð� ����
    [Header("About Days")]
    public int currentDay = 1;
    public int endDay = 7;

    
    #endregion

    #region PrivateVariables
    public static GameTime currentTime;   //�ݵ�� Scene ���� �� �ʱ�ȭ�Ұ�!!!!!!!!
    
    #endregion

    #region PublicMethod
    #endregion

    #region PrivateMethod
    private void Start()
    {
        //����, �ʱ�ȭ
        currentTime = GameTime.AFTER_NOON;
        StartCoroutine(MainFlow());

        if (currentTime == GameTime.NIGHT)
        {
            //���� ���� �۵��� �ڵ� �Է�
        }
    }

    private void Update()
    {
    }

    private IEnumerator MainFlow()
    {
        //1. �� ����
        while (currentDay<=endDay)
        {
            //�� ����
            yield return StartCoroutine(AfternoonFlow());
            //�� ����
            yield return StartCoroutine(NightFlow());
            currentDay += 1;
        }
        
    }

    private IEnumerator AfternoonFlow()
    {
        //���� ������ �ڷ�ƾ�� �̰����� �����մϴ�.
        //�켱 ���� �ð��� �����մϴ�.
        currentTime = GameTime.AFTER_NOON;
        Debug.Log("Day "+currentDay+"���� �� ����.");
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator NightFlow()
    {
        //�㿡 ������ �ڷ�ƾ�� �̰����� �����մϴ�.
        //�켱 ���� �ð��� �����մϴ�.
        currentTime = GameTime.NIGHT;
        Debug.Log("Day " + currentDay + "���� �� ����.");
        yield return new WaitForSeconds(1f);
    }

    #endregion
}
