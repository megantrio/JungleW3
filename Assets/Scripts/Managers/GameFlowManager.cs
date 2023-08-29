using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    #region PublicVariables
    public enum GameState
    {
        AFTER_NOON,
        NIGHT,
    }


    //시간 관련
    [Header("About Days")]
    public int currentDay = 1;
    public int endDay = 7;

    
    #endregion

    #region PrivateVariables
    private GameState currentState;
    #endregion

    #region PublicMethod
    #endregion

    #region PrivateMethod
    private void Start()
    {
        //시작, 초기화
        StartCoroutine(MainFlow());
    }

    private void Update()
    {
    }

    private IEnumerator MainFlow()
    {
        //1. 낮 진행
        while (currentDay<=endDay)
        {
            //낮 진행
            yield return StartCoroutine(AfternoonFlow());
            //밤 진행
            yield return StartCoroutine(NightFlow());
            currentDay += 1;
        }
        
    }

    private IEnumerator AfternoonFlow()
    {
        //낮에 진행할 코루틴을 이곳에서 실행합니다.
        //우선 현재 시간을 갱신합니다.
        currentState = GameState.AFTER_NOON;
        Debug.Log("Day "+currentDay+"일차 낮 진행.");
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator NightFlow()
    {
        //밤에 진행할 코루틴을 이곳에서 실행합니다.
        //우선 현재 시간을 갱신합니다.
        currentState = GameState.NIGHT;
        Debug.Log("Day " + currentDay + "일차 밤 진행.");
        yield return new WaitForSeconds(1f);
    }

    #endregion
}
