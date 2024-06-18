using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LeveL Object", menuName = "New Object Factory")]

public class LeveLObjectFactory : ScriptableObject
{
    public List<ObjData> ObjectsData;
    private Dictionary<int, string> ObjDictionary;

    public string ReturnPath(int pathID)
    {
        if (ObjDictionary == null)
        {
            ObjDictionary = new Dictionary<int, string>();

            for (int i = 0; i < ObjectsData.Count; i++)
            {
                ObjDictionary.Add(ObjectsData[i].ObjID, ObjectsData[i].ObjPath);
            }

        }
        return ObjDictionary[pathID];
    }
}
[System.Serializable]
public class ObjData
{
    public int ObjID;
    public string ObjPath;
}