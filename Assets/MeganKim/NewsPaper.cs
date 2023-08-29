using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NewsPaper : MonoBehaviour
{

    //�Ź� ��ũ��Ʈ. ���� �Ǿ� ��¥���� �����Ǵ� ������ �ٸ���
    //���� ���� �޾ƿͼ� ��� �ϴ� ������ �� �ٸ���.
    //������ Ÿ�Կ����� (1)�켼�� ��� (2) ������ ��¥ (3)�׽�Ʈ �̺�Ʈ�� �ϳ� �־���.

    public TextMeshProUGUI dayText;
    public TextMeshProUGUI advantageText;
    //public TextMeshProUGUI tribeText;
    public string tribe;

    int day = 12;
    int advantage = 10;

    //Sell ��ũ��Ʈ ȣ��

    private void Start()
    {
        SetTodayText();
        SetAdvantageText();
    }

    public void SetTodayText()
    {
        dayText.SetText($"������ ��¥ : 3042�� 54��" + day + "��");
    }

    public void SetAdvantageText()
    {
        advantageText.SetText($"���� ������ ���� " + tribe + " ������ " + advantage +
            " �� �켼�� ������ ���Դϴ�. \n ���� �� ������ ���� �������?");
    }

}
