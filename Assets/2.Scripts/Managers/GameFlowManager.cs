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

    public GameObject newsPaper;
    public GameObject afternoonUI;
    public GameObject nightUI;

    
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
        yield return StartCoroutine(CreateNewsPaper());
    }

    private IEnumerator NightFlow()
    {
        //�㿡 ������ �ڷ�ƾ�� �̰����� �����մϴ�.
        //�켱 ���� �ð��� �����մϴ�.
        currentTime = GameTime.NIGHT;
        Debug.Log("Day " + currentDay + "���� �� ����.");
        yield return StartCoroutine(CreateNightUI());
    }

    //�� ���ϴ� �׽�Ʈ�Դϴ�.
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
