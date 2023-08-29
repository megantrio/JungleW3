using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TypingManager : MonoBehaviour
{
    public static TypingManager instance;

    public float timeForCharacter;
    public float timeForCharacter_Fast;
    float characterTime;
    string[] dialogsSave;
    TextMeshProUGUI tmpSave;

    public static bool isDialogEnd;

    bool isTypingEnd = false;
    int dialogNumber = 0;

    float timer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        timer = timeForCharacter;
        characterTime = timeForCharacter;
    }

    public void Typing(string[] description, TextMeshProUGUI descriptionObj)
    {
        isDialogEnd = false;
        dialogsSave = description;
        tmpSave = descriptionObj;
        if (dialogNumber < description.Length)
        {

            char[] chars = description[dialogNumber].ToCharArray(); //받아온 다이얼로그 변환
            StartCoroutine(Typer(chars, descriptionObj));
        }
        else
        {
            tmpSave.text = "";
            isDialogEnd = true;
            dialogsSave = null;
            dialogNumber = 0;
            tmpSave = null;
        }

    }
    IEnumerator Typer(char[] chars, TextMeshProUGUI descriptionObj)
    {
        int curruntChar = 0;
        int charLength = chars.Length;
        isTypingEnd = false;

        while (curruntChar < charLength)
        {
            if(timer >= 0)
            {
                yield return null;
                timer -= Time.deltaTime;

            }
            else
            {
                descriptionObj.text += chars[curruntChar].ToString();
                curruntChar++;
                timer = characterTime; //타이머 초기화
            }
        }
        if(curruntChar >= charLength)
        {
            isTypingEnd = true; 
            dialogNumber++;
            yield break;
        }

    }

    public void TextSkip()
    {
        if(dialogsSave != null)
        {
            if (isTypingEnd)
            {
                tmpSave.text = "";
                Typing(dialogsSave, tmpSave);

            }
            else
            {
                characterTime = timeForCharacter_Fast;
            }
        }    
    }

    public void TextSpeedReSet()
    {
        if(dialogsSave != null)
        {
            characterTime = timeForCharacter;
        }
    }

}
