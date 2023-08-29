using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NewsPaper : MonoBehaviour
{

    //신문 스크립트. 고정 되어 날짜별로 제공되는 내용이 다르고
    //변수 값을 받아와서 출력 하는 내용이 또 다르다.
    //프로토 타입용으로 (1)우세도 출력 (2) 오늘의 날짜 (3)테스트 이벤트를 하나 넣었음.

    public TextMeshProUGUI dayText;
    public TextMeshProUGUI advantageText;
    //public TextMeshProUGUI tribeText;
    public string tribe;

    int day = 12;
    int advantage = 10;

    //Sell 스크립트 호출

    private void Start()
    {
        SetTodayText();
        SetAdvantageText();
    }

    public void SetTodayText()
    {
        dayText.SetText($"오늘의 날짜 : 3042년 54월" + day + "일");
    }

    public void SetAdvantageText()
    {
        advantageText.SetText($"어제 전투로 인해 " + tribe + " 종족이 " + advantage +
            " 더 우세한 것으로 보입니다. \n 과연 이 전쟁은 언제 끝날까요?");
    }

}
