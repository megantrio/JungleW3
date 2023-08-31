using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MorningEventManager : MonoBehaviour
{

    public enum GameState
    {
        MORNING,
        NIGHT
    }

    public GameState state;

    public static MorningEventManager instance;
    public static int day = 0;
    public bool isEventEnded = false;
    public int currentEvent = 0;

    public GameObject[] morningObjects;
    public GameObject[] nightObject;  //밤에만 보이게 할 오브젝트 출현

    public List<GameEvent> morningEvents;

    // Timer
    [SerializeField] private GameObject timerObj;
    [SerializeField] private Image lateTime;
    public TextMeshProUGUI timerTxt;
    private float fdt;
    private float nowTime;



    private void Awake()
    {

        instance = this;
        isEventEnded = true;
        currentEvent = 0;
        state = GameState.MORNING;

    }

    public void Update()
    {
        if (state == GameState.MORNING)
        {
            timerObj.SetActive(false);
            if (isEventEnded)
            {
                if (morningEvents.Count > currentEvent)
                {
                    morningEvents[currentEvent].StartEvent();
                    currentEvent += 1;
                    isEventEnded = false;
                }
                else
                {
                    if (SceneManager.GetActiveScene().name.Equals("Day7"))
                    {
                        CallSceneChange();
                    }
                    else
                    {
                        foreach (var v in morningObjects)
                        {
                            v.SetActive(false);
                        }
                        state = GameState.NIGHT;
                        foreach (var v in nightObject)
                        {
                            v.SetActive(true);
                        }
                    }
                    //7일차라면 씬 초기화
                }
            }
        }

        else if (state == GameState.NIGHT)
        {
            if(isEventEnded)
            {
                timerObj.SetActive(true);
                fdt += Time.deltaTime;
                nowTime = (60f - (Mathf.Floor(fdt * 100f) / 100f));
                timerTxt.text = nowTime.ToString();
                lateTime.fillAmount = 1f - (fdt / 60f);

                if(fdt >= 60f)
                {
                    CallSceneChange();
                }
            }            
        }

    }

    public void CallSceneChange()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
