using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderList : MonoBehaviour
{
    public string orderer;
    public string orderItem;
    public TextMeshProUGUI ordererText;
    public TextMeshProUGUI orderList;
    // Start is called before the first frame update

    public void ResetList()
    {
        ordererText.text = "";
        orderList.text = "";
        orderList.color = new Color(0, 0, 0, 1f);
        ordererText.color = new Color(0, 0, 0, 1f);
    }

    public void checkItem(string itemName)
    {
        if (orderItem == itemName)
        {
            ordererText.text = $"<s>{ordererText.text}</s>";
            orderList.text = $"<s>{orderList.text}</s>";
            orderList.color = new Color(113, 57, 99, 0.5f);
            ordererText.color = new Color(113, 57, 99, 0.5f);
        }

        else
        {
            return;
        }
    }


}