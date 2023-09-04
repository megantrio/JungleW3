using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static Dictionary<string, bool> canUseMap = new Dictionary<string, bool>();

    public static void Clear()
    {
        canUseMap.Clear();
        canUseMap[""] = true;
    }

    public static void SetNPCCondition(string name, bool value)
    {
        canUseMap[name] = value;
    }

    public static bool GetNPCCondition(string name)
    {
        bool condition = false;
        if (canUseMap.ContainsKey(name)) { condition = canUseMap[name]; }
        return condition;
    }

    public static void LoadAndCreateNPCData(List<EventObject>[] morningEvents, NPCEvent prefab, Vector3 position)
    {
        //NPCData Load
        List<Dictionary<string, object>> _rawNPCData = CSVReader.Read("Database/NPCData");
        SetNPCCondition("", true);
        for (int i = 0; i < morningEvents.Length; i++)
        {
            morningEvents[i] = new List<EventObject>();
        }
        foreach (var npcData in _rawNPCData)
        {
            object temp;
            npcData.TryGetValue("MoveType", out temp);
            if (temp.ToString().Equals("0"))
            {
                NPCEvent cur = Instantiate(prefab, position, Quaternion.identity);

                //Day                
                temp = npcData["Day"];
                int day = int.Parse(temp.ToString());
                morningEvents[day].Add(cur);
                //Sprite
                npcData.TryGetValue("Sprite", out temp);
                temp = "npc/" + temp;
                cur.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(temp.ToString());
                //Script
                npcData.TryGetValue("ScriptFile", out temp);
                string path = "Database/Descriptions/" + temp.ToString();
                Debug.Log("path: " + path);
                npcData.TryGetValue("ScriptNum", out temp);
                string scriptType = temp.ToString();
                List<Dictionary<string, object>> descriptionRawData = CSVReader.Read(path);
                foreach (var row in descriptionRawData)
                {
                    object t;
                    row.TryGetValue("ID", out t);
                    if (scriptType.Equals(t.ToString()))
                    {
                        row.TryGetValue("Description", out t);
                        cur.description.Add(t.ToString());

                        row.TryGetValue("Speaker", out t);
                        cur.speaker.Add(t.ToString());
                    }
                }
                //Condition
                npcData.TryGetValue("Condition", out temp);
                cur.condition = temp.ToString();
                if (!GetNPCCondition(cur.condition))
                {
                    //GetNPCCondition이 True면 이미 설정된것이므로 하지 않음
                    SetNPCCondition(cur.condition, false);
                }

                //생성했으니 비활성화함
                cur.gameObject.SetActive(false);
            }

        }
    }
}
