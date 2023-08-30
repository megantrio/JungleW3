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


    //시간 관련
    [Header("About Days")]
    public int currentDay = 1;
    public int endDay = 7;

    public GameObject newsPaper;
    public GameObject afternoonUI;
    public GameObject nightUI;

    
    #endregion

    #region PrivateVariables
    public static GameTime currentTime;   //반드시 Scene 시작 시 초기화할것!!!!!!!!
    
    #endregion

    #region PublicMethod
    #endregion

    #region PrivateMethod
    private void Start()
    {
        //시작, 초기화
        currentTime = GameTime.AFTER_NOON;
        StartCoroutine(MainFlow());

        if (currentTime == GameTime.NIGHT)
        {
            //밤일 때만 작동할 코드 입력
        }
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
        currentTime = GameTime.AFTER_NOON;
        Debug.Log("Day "+currentDay+"일차 낮 진행.");
        yield return StartCoroutine(CreateNewsPaper());
    }

    private IEnumerator NightFlow()
    {
        //밤에 진행할 코루틴을 이곳에서 실행합니다.
        //우선 현재 시간을 갱신합니다.
        currentTime = GameTime.NIGHT;
        Debug.Log("Day " + currentDay + "일차 밤 진행.");
        yield return StartCoroutine(CreateNightUI());
    }

    //이 이하는 테스트입니다.
    private IEnumerator CreateNewsPaper()
    {
        newsPaper.SetActive(true);
        afternoonUI.SetActive(true);
        yield return new WaitForSeconds(3f);
        afternoonUI.SetActive(false);
    }


    private IEnumerator CreateNightUI()
    {
        nightUI.SetActive(true);
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                nightUI.SetActive(false);
                yield break;
            }
            yield return null;
        }
    }

    #endregion
}
