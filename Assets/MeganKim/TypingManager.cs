using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TypingManager : MonoBehaviour
{
    public static TypingManager instance;

    public float timeForCharacter;
    public float timeForCharacter_Fast;
    float characterTime;
    string[] dialogsSave;

    [Header("ĵ���� ���� UI")]
    public GameObject dialogUI;
    public TextMeshProUGUI speakerNameUI;
    public TextMeshProUGUI dialogTextUI;

    public static bool isDialogEnd;
    private bool isDialogClicked = false;


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

    public Coroutine Typing(string speakerName, string description)
    {
        isDialogEnd = false;
        
        //UI �����ִٸ� ŵ�ϴ�.
        if (!dialogUI.activeSelf)
        {
            dialogUI.SetActive(true);
        }

        //���� ����
        speakerNameUI.text = speakerName;
        dialogTextUI.text = "";
        isDialogClicked = false;
        characterTime = timeForCharacter;
        char[] chars = description.ToCharArray(); //�޾ƿ� ���̾�α� ��ȯ
        return StartCoroutine(Typer(chars, dialogTextUI));
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
                timer = characterTime; //Ÿ�̸� �ʱ�ȭ
            }
            if (isDialogClicked)
            {
                characterTime = timeForCharacter_Fast;
                isDialogClicked = false;
            }
        }
        if (curruntChar >= charLength)
        {
            isTypingEnd = true;

            while (!isDialogClicked)
            { yield return null; }
            isDialogClicked = false;
            yield break;
        }
    }

    public void TextSkip()
    {
        if(dialogsSave != null)
        {
            if (isTypingEnd)
            {
                dialogTextUI.text = "";
                //Typing(dialogsSave);

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

    public void OnDialogClick(InputAction.CallbackContext callback)
    {
        if (callback.started)
        {
            if (dialogUI.activeSelf)
                isDialogClicked = true;
        }
    }

}
