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
        //일자별로 정렬이 되어있다고 가정하고 집어넣습니다.
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
                NPCEvent offer = Instantiate(prefab, position, Quaternion.identity);
                NPCEvent success = Instantiate(prefab, position, Quaternion.identity);
                NPCEvent fail = Instantiate(prefab, position, Quaternion.identity);

                //Day                
                temp = npcData["Day"];
                int day = int.Parse(temp.ToString());
                
                morningEvents[day].Add(offer);

                //Condition
                npcData.TryGetValue("Condition", out temp);
                if (!temp.ToString().Equals(""))
                {
                    morningEvents[day + 1].Add(success);
                    morningEvents[day + 1].Add(fail);
                }
                //Sprite
                npcData.TryGetValue("Sprite", out temp);
                temp = "npc/" + temp;
                offer.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(temp.ToString());
                success.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(temp.ToString());
                fail.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(temp.ToString());
                //Script
                npcData.TryGetValue("ScriptFile", out temp);
                string path = "Database/Descriptions/" + temp.ToString();
                //npcData.TryGetValue("ScriptNum", out temp);
                //string scriptType = temp.ToString();
                List<Dictionary<string, object>> descriptionRawData = CSVReader.Read(path);
                foreach (var row in descriptionRawData)
                {
                    object t;
                    row.TryGetValue("ID", out t);
                    if ("Offer".Equals(t.ToString()))
                    {
                        row.TryGetValue("Description", out t);
                        offer.description.Add(t.ToString());

                        row.TryGetValue("Speaker", out t);
                        offer.speaker.Add(t.ToString());
                    }
                    if ("Success".Equals(t.ToString()))
                    {
                        row.TryGetValue("Description", out t);
                        success.description.Add(t.ToString());

                        row.TryGetValue("Speaker", out t);
                        success.speaker.Add(t.ToString());
                    }
                    if ("Fail".Equals(t.ToString()))
                    {
                        row.TryGetValue("Description", out t);
                        fail.description.Add(t.ToString());

                        row.TryGetValue("Speaker", out t);
                        fail.speaker.Add(t.ToString());
                    }
                }
                //Condition
                npcData.TryGetValue("Condition", out temp);
                //Offer 오브젝트는 반드시 실행되어야 하므로, ""로 수정
                offer.condition = "";
                SetNPCCondition("", true);
                //Success, Fail 오브젝트는 실행되거나 말거나이므로, 세팅해준다.
                success.condition = temp.ToString();
                fail.condition = temp.ToString();
                if (!temp.ToString().Equals(""))
                {
                    SetNPCCondition(success.condition, false);
                }
                //if (!GetNPCCondition(cur.condition))
                {
                    //GetNPCCondition이 True면 이미 설정된것이므로 하지 않음
                    //SetNPCCondition(success.condition, false);
                }
                success.conditionValue = true;
                fail.conditionValue = false;

                //생성했으니 비활성화함
                offer.gameObject.SetActive(false);
                success.gameObject.SetActive(false);
                fail.gameObject.SetActive(false);
            }

        }
    }

}
