using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class LeveLDataManager : MonoBehaviour, IDataPresistence
{
    public static LeveLDataManager instance;
    public LeveLObjectFactory objectFactory;
    [SerializeField] private List<LeveLObject> objects = new List<LeveLObject>();
    public int levelID;

    private Rigidbody2D[] rgbody;
    private List<GameObject> screwHoleList = new List<GameObject>();
    [SerializeField] GameObject screwHoles, screws, board;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void LoadData(GameData data)
    {
        levelID = data.levelID;
    }
    public void SaveData(ref GameData data)
    {

    }

    #region Save Level
    public void SaveLevel()
    {
        var newlvl = ScriptableObject.CreateInstance<LeveLScriptableObject>();
        newlvl.levelIndex = levelID;
        newlvl.name = $"Level {levelID}";

        objects = FindObjectsOfType(typeof(LeveLObject)).Cast<LeveLObject>().ToList();
        for (int i = 0; i < objects.Count; i++)
        {
            if (newlvl.ObjPrefabs == null)
            {
                newlvl.ObjPrefabs = new List<LevelObjData>();

            }
            newlvl.ObjPrefabs.Add(objects[i].GetData());
        }
        //ScriptableUtility.SaveLevelFile(newlvl);
        LeveLManager.Instance.CallResetAfterFixLevel();
        //string data = JsonUtility.ToJson(newlvl);
        //string filePath = Application.dataPath + Path.AltDirectorySeparatorChar + "/Level " + levelID + ".json";

        //File.WriteAllText(filePath, data);
        //Debug.Log(filePath);
    }
    #endregion

    #region Load Level
    public void LoadLevel()
    {
        ClearLevel();
        var level = Resources.Load<LeveLScriptableObject>("Levels/Level " + levelID);
        if (level == null)
        {
            Debug.LogError($"LeveL {levelID} does not exist");
        }
        else
        {
            for (int i = 0; i < level.ObjPrefabs.Count; i++)
            {
                var levelObj = Resources.Load<LeveLObject>(Config.Instance.gameObjectFactory.ReturnPath(level.ObjPrefabs[i].ObjID));
                var createObj = Instantiate<LeveLObject>(levelObj, level.ObjPrefabs[i].ObjPos, level.ObjPrefabs[i].ObjRotate);
                if (level.ObjPrefabs[i].ObjID > 5 && level.ObjPrefabs[i].ObjID < 20)
                {
                    createObj.GetComponent<WoodStick>().color = level.ObjPrefabs[i].woodColorID;
                    screwHoleList.Add(createObj.gameObject);
                }
                if (level.ObjPrefabs[i].ObjID >= 20)
                {
                    rgbody = createObj.GetComponentsInChildren<Rigidbody2D>();
                    foreach (Rigidbody2D a in rgbody)
                    {
                        a.isKinematic = true;
                    }
                }
                createObj.transform.localScale = level.ObjPrefabs[i].ObjScale;
                createObj.GetComponentInChildren<Transform>().localScale = level.ObjPrefabs[i].ObjScale;
                createObj.SetData(level.ObjPrefabs[i]);
                switch (level.ObjPrefabs[i].ObjID)
                {
                    case 1:
                        createObj.transform.SetParent(screws.transform);
                        break;
                    case 2:
                        createObj.transform.SetParent(screwHoles.transform);
                        break;
                    default:
                        createObj.transform.SetParent(board.transform);
                        break;
                }

            }
        }
    }
    public void LoadLevelTesting()
    {
        ClearLevel();
        var level = Resources.Load<LeveLScriptableObject>("Level_Testing/Level " + levelID);
        if (level == null)
        {
            Debug.LogError($"LeveL {levelID} does not exist");
            return;
        }
        else
        {
            for (int i = 0; i < level.ObjPrefabs.Count; i++)
            {
                var levelObj = Resources.Load<LeveLObject>(Config.Instance.gameObjectFactory.ReturnPath(level.ObjPrefabs[i].ObjID));
                var createObj = Instantiate<LeveLObject>(levelObj, level.ObjPrefabs[i].ObjPos, level.ObjPrefabs[i].ObjRotate);
                if (level.ObjPrefabs[i].ObjID > 5 && level.ObjPrefabs[i].ObjID < 20)
                {
                    createObj.GetComponent<WoodStick>().color = level.ObjPrefabs[i].woodColorID;

                    screwHoleList.Add(createObj.gameObject);
                }
                if (level.ObjPrefabs[i].ObjID >= 20)
                {
                    rgbody = createObj.GetComponentsInChildren<Rigidbody2D>();
                    foreach (Rigidbody2D a in rgbody)
                    {
                        a.isKinematic = true;
                    }
                }
                createObj.transform.localScale = level.ObjPrefabs[i].ObjScale;
                createObj.SetData(level.ObjPrefabs[i]);
                createObj.transform.SetParent(this.transform);
            }
        }
    }
    #endregion

    #region Clear Level
    public void ClearLevel()
    {
        var clearObj = FindObjectsOfType<LeveLObject>();
        foreach (LeveLObject i in clearObj)
        {
            Destroy(i.gameObject);
        }
        screwHoleList.Clear();
    }
    #endregion
}
//#if UNITY_EDITOR
//public static class ScriptableUtility
//{
//    public static void SaveLevelFile(ScriptableObject level)
//    {
//        AssetDatabase.CreateAsset(level, $"Assets/Resources/Levels/" + level.name + ".asset");
//        AssetDatabase.SaveAssets();
//        AssetDatabase.Refresh();
//    }
//}
//public class SaveLevelCompleted
//{
//    public string levelName;
//    public int levelID;
//}
//#endif
