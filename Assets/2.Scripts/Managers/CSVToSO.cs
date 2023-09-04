using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;


public class CSVToSO
{
    private static string itemCSVPath = "/Resources/Database/ItemList.csv";

    [MenuItem("Utilities/Generate Items")]
    public static void GenerateItem()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath + itemCSVPath);

        Debug.Log("CSVToSO");

        foreach (string s in allLines)
        {
            string[] splitData = s.Split(',');
            if (splitData[0] == "isMixItem") continue;

            Item item = ScriptableObject.CreateInstance<Item>();
            if (splitData[0] == "TRUE") item.isMixItem = true;
            else item.isMixItem = false;

            if (splitData[1] == "TRUE") item.isSpecialItem = true;
            else item.isSpecialItem = false;

            item.itemCount = int.Parse(splitData[2]);
            item.applyDay = int.Parse(splitData[3]);

            item.itemName = splitData[4];
            item.itemInfo = splitData[5];

            // 실제 생성 시 폴더 위치 조정할 것
            item.itemImage = Resources.Load<Sprite>($"TestImage/{splitData[6]}");

            if (item.itemImage == null) { Debug.Log("Sprite가 적용 안됨"); }
            Debug.Log("Item CheckOK");

            // 실제 생성 시 Test 변경할 것
            AssetDatabase.CreateAsset(item, $"Assets/Items/Test/{item.itemName}.asset");
            Debug.Log("Item Create");
        }

        AssetDatabase.SaveAssets();
    }

}