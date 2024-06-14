
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class DataAPIController : MonoBehaviour
{
    public static DataAPIController instance;

    [SerializeField]
    private DataModel dataModel;

    private void Awake()
    {
        instance = this;
    }

    public void InitData(Action callback)
    {
        Debug.Log("(BOOT) // INIT DATA");

        dataModel.InitData(() =>
        {
            //CheckDailyLogin();
            callback();
        });

        Debug.Log("==========> BOOT PROCESS SUCCESS <==========");
    }

    #region Get Data
    //Get current level btn avatar position
    /*
    public Vector2 GetCurLevelBtnAvatarPos()
    {
        List<float> listPos = dataModel.ReadData<List<float>>(DataPath.CURLEVELBTNAVATARPOS);
        Vector2 avatarPos = new Vector2((float)listPos[0], (float)listPos[1]);
        return avatarPos;
    }*/

    //Get current level btn position
    /*
    public float GetCurLevelBtnPos_X()
    {
        float xPos = dataModel.ReadData<float>(DataPath.CURLEVELBTNPOS_X);
        return xPos;
    }*/

    //Get total gold
    /*
    public int GetTotalGold()
    {
        int totalGold = dataModel.ReadData<int>(DataPath.TOTALGOLD);
        return totalGold;
    }*/

    //Get current level
    /*
    public int GetCurrentLevel()
    {
        int currentLevel = dataModel.ReadData<int>(DataPath.CURRENTLEVEL);
        return currentLevel;
    }*/

    //Get level data
    /*
    public LevelData GetLevelData(LevelConfigRecord levelConfig)
    {
        LevelData levelData = dataModel.ReadDictionary<LevelData>(DataPath.LEVELDATA, levelConfig.LevelNum.ToKey());
        return levelData;
    }*/

    //Get skin ID
    /*
    //public int GetCurrentSkinId()             => Skin 
    //{
    //    int curSkinId = dataModel.ReadData<int>(DataPath.CURRENTSKINID);
    //    return curSkinId;
    //}*/

    //Get Character Skin already owned
    /*
    //public List<int> GetCharSkinOwned()       => Character skin
    //{
    //    List<int> skinList = dataModel.ReadData<List<int>>(DataPath.CHARSKINOWNED);
    //    return skinList;
    //}*/

    //Star
    /*
    //public int GetTotalStar()         => Star
    //{
    //    int totalStar = dataModel.ReadData<int>(DataPath.TOTALSTAR);
    //    return totalStar;
    //}*/

    //Spin wheel
    /*
    //public string GetLastTimeSpin()       => Spin? more like HELIKOPTER! WOOOOOOOOOOOO
    //{
    //    string lastTime = dataModel.ReadData<string>(DataPath.LASTTIMESPIN);
    //    return lastTime;
    //}*/

    //Get last Time Login
    /*
    public string GetLastTimeLogin()
    {
        string lastTime = dataModel.ReadData<string>(DataPath.LASTTIMELOGIN);
        return lastTime;
    }
    */

    //Get daily login progress
    /*
    public int GetDailyLoginProgress()
    {
        int loginProgress = dataModel.ReadData<int>(DataPath.DAILYLOGINPROGRESS);
        return loginProgress;
    }*/

    //Mission Data
    /*
    //public MissionData GetMissionData(int id)                 => Mission (Game slave quest area)
    //{
    //    MissionData data = dataModel.ReadDictionary<MissionData>(DataPath.DIC_MISSION, id.ToKey());
    //    if (data == null)
    //    {
    //        data = new MissionData();
    //        data.id = id;
    //        data.isClaimed = false;
    //        data.number = 0;
    //        dataModel.UpdateDataDictionary(DataPath.DIC_MISSION, id.ToKey(), data);
    //    }
    //    return data;
    //}*/

    //Get Daily Claim
    /*
    public bool GetDailyClaim()
    {
        bool value = dataModel.ReadData<bool>(DataPath.DAILYCLAIM);
        return value;
    }
    */

    //Daily mission
    /*
    //public List<int> GetDailyMissions()                                           => Daily mission for reward
    //{
    //    List<int> list = dataModel.ReadData<List<int>>(DataPath.DAILYMISSION);
    //    return list;
    //}

    //public int GetDailyMissionCount()
    //{
    //    int count = dataModel.ReadData<int>(DataPath.DAILYMISSIONCOUNT);
    //    return count;
    //}*/
    #endregion

    #region Update Data

    //Update current level btn avatar position
    public void UpdateCurLevelBtnAvatarPos(Vector2 avatarPos)
    {
        List<float> listPos = new List<float> { avatarPos.x, avatarPos.y};
        dataModel.UpdateData(DataPath.CURLEVELBTNAVATARPOS, listPos);
    }
    //update current level btn pos
    public void UpdateCurLevelBtnPos_X(float xPos)
    {
        dataModel.UpdateData(DataPath.CURLEVELBTNPOS_X, xPos);
    }

    //Change skin for character
    /*
    //public void UpdateCharSkin(int id)                       => Change skin but NOT power up
    //{
    //    List<int> skinList = GetCharSkinOwned();
    //    skinList.Add(id);
    //    dataModel.UpdateData(DataPath.CHARSKINOWNED, skinList);
    //}*/

    //Update current level  
    /*
    public void UpdateCurrentLevel(int curLv)
    {
        dataModel.UpdateData(DataPath.CURRENTLEVEL, curLv);
    }*/
    
    //Update level result status
    /*
    //public void UpdateLevelResult(LevelConfigRecord levelConfig, int starAmount)          => Update level result status
    //{
    //    LevelData newLevelData = new LevelData();
    //    newLevelData.levelID = levelConfig.LevelNum;
    //    newLevelData.starResult = starAmount;

    //    LevelData curLevelData = GetLevelData(levelConfig);

    //    if (curLevelData == null)
    //    {
    //        dataModel.UpdateDataDictionary(DataPath.LEVELDATA, levelConfig.LevelNum.ToKey(), newLevelData);
    //        AddTotalStar(starAmount);
    //    }
    //    else
    //    {
    //        if (newLevelData.starResult > curLevelData.starResult)
    //        {
    //            dataModel.UpdateDataDictionary(DataPath.LEVELDATA, levelConfig.LevelNum.ToKey(), newLevelData);
    //            int tempAmount = newLevelData.starResult - curLevelData.starResult;
    //            AddTotalStar(tempAmount);
    //        }
    //    }
    //}*/

    //Update skin
    /*
    //public void UpdateCurrentSkin(int skinIndex)
    //{
    //    dataModel.UpdateData(DataPath.CURRENTSKINID, skinIndex);   => Update skin
    //}*/

    //Update total gold
    /*
    //public void UpdateTotalGold(int goldAmount)
    //{
    //    dataModel.UpdateData(DataPath.TOTALGOLD, goldAmount);
    //    int totalGold = GetTotalGold();
    //}*/

    //Update Stars
    /*
    //public void UpdateTotalStar(int starAmount)
    //{
    //    dataModel.UpdateData(DataPath.TOTALSTAR, starAmount);     => Update Stars;
    //    int totalStar = GetTotalStar();
    //}
    */

    //Update Spin wheel
    /*
    //public void UpdateLastTimeSpin(string time)
    //{
    //    dataModel.UpdateData(DataPath.LASTTIMESPIN, time);        => Update HELIKOPTER!!!!
    //}
    */

    //Login
    /*
    //public void UpdateLastTimeLogin(string time)
    //{
    //    dataModel.UpdateData(DataPath.LASTTIMELOGIN, time);         
    //    string lastTime = GetLastTimeLogin();
    //}

    //public void UpdateDailyLoginProgress(int progress)
    //{
    //    dataModel.UpdateData(DataPath.DAILYLOGINPROGRESS, progress);
    //    int loginProgress = GetDailyLoginProgress();
    //}
    */

    //Update mission Data
    /*
    //public void UpdateMissionData(int id, int number)
    //{
    //    MissionData data = dataModel.ReadDictionary<MissionData>(DataPath.DIC_MISSION, id.ToKey());           => Update mission data

    //    if (data == null)
    //    {
    //        data = new MissionData();
    //        data.id = id;
    //        data.isClaimed = false;
    //        data.number = 0;
    //    }
    //    data.number = number;
    //    dataModel.UpdateDataDictionary(DataPath.DIC_MISSION, id.ToKey(), data);
    //}
    */

    //Update daily claim
    /*
    public void UpdateDailyClaim(bool value)
    {
        dataModel.UpdateData(DataPath.DAILYCLAIM, value);
    }*/

    //Update daily mission
    /*
    //public void UpdateDailyMissions(List<int> missions)
    //{
    //    dataModel.UpdateData(DataPath.DAILYMISSION, missions);                    => Update daily mission
    //}

    //public void UpdateDailyMissionCount(int count)
    //{
    //    dataModel.UpdateData(DataPath.DAILYMISSIONCOUNT, count);
    //}
    */

    #endregion

    #region Others
    //Check daily login
    /*
    //public void CheckDailyLogin()
    //{
    //    string timeLastLoginString = GetLastTimeLogin();

    //    if (string.IsNullOrEmpty(timeLastLoginString))
    //    {
    //        Debug.Log("(EVENT) // FIRST TIME LOGIN");
    //        timeLastLoginString = DateTime.Now.ToString();
    //        UpdateLastTimeLogin(timeLastLoginString);
    //    }

    //    DateTime timeLastLogin = DateTime.Parse(timeLastLoginString);
    //    DateTime timeNow = DateTime.Now;

    //    if (timeLastLogin.Year < timeNow.Year)
    //    {
    //        ResetDailyData();
    //    }
    //    else
    //    {
    //        if (timeLastLogin.DayOfYear < timeNow.DayOfYear)
    //        {
    //            ResetDailyData();
    //        }
    //    }
    //}*/

    //Reset Daily Data
    /*
    //private void ResetDailyData()
    //{
    //    UpdateLastTimeLogin(DateTime.Now.ToString());

    //    Dictionary<string, MissionData> dic = new Dictionary<string, MissionData>();
    //    dataModel.UpdateData(DataPath.DIC_MISSION, dic);

    //    bool value = true;
    //    UpdateDailyClaim(value);

    //    List<int> curDailyMissions = GetDailyMissions();
    //    List<int> newDailyMissions = new List<int>();

    //    int count = 0;
    //    do
    //    {
    //        // Careful with Random.Range with int
    //        // Remember to +1 maxValue if you want it random to your desire maxValue
    //        int index = UnityEngine.Random.Range(1, Config.Instance.MissionConfig.GetAllRecord().Count + 1);
    //        if (!newDailyMissions.Contains(index) && !curDailyMissions.Contains(index))
    //        {
    //            newDailyMissions.Add(index);
    //            count++;
    //        }
    //    }
    //    while (count < curDailyMissions.Count);

    //    UpdateDailyMissions(newDailyMissions);

    //    int missionCount = 0;
    //    UpdateDailyMissionCount(missionCount);

    //    Debug.Log("(EVENT) // RESET DAILY DATA");
    //}*/

    //Reset mission data
    /*
    //public void ResetMissionData(int id)
    //{
    //    MissionData data = new MissionData();
    //    data.id = id;
    //    data.isClaimed = false;
    //    data.number = 0;
    //    dataModel.UpdateDataDictionary<MissionData>(DataPath.DIC_MISSION, id.ToKey(), data);
    //}*/

    //Add gold
    /*
    //public void AddGold(int goldAmount)
    //{
    //    int currentGold = GetTotalGold();
    //    currentGold += goldAmount;            => Add gold
    //    UpdateTotalGold(currentGold);
    //}
    */

    //Minus gold;
    /*
    //public void MinusGold(int goldAmount, Action callback = null)
    //{
    //    int currentGold = GetTotalGold();
    //    currentGold -= goldAmount;                    => Minus gold;
    //    UpdateTotalGold(currentGold);
    //}
    */

    //Add Star
    /*
    //public void AddTotalStar(int starAmount)
    //{
    //    int currentStarAmount = GetTotalStar();
    //    currentStarAmount += starAmount;
    //    UpdateTotalStar(currentStarAmount);
    //}
    */

    //Claim mission
    /*
    //public void ClaimMission(MissionConfigRecord record, Action<bool> callback)
    //{
    //    MissionData data = GetMissionData(record.Id);

    //    if (data == null)
    //    {
    //        callback(false);
    //    }
    //    else
    //    {
    //        if (data.number < record.Number)
    //        {
    //            callback(false);
    //        }
    //        else
    //        {
    //            int missionCount = GetDailyMissionCount();
    //            missionCount++;
    //            UpdateDailyMissionCount(missionCount);
    //            AddGold(record.Reward);
    //            data.isClaimed = true;
    //            dataModel.UpdateDataDictionary(DataPath.DIC_MISSION, record.Id.ToKey(), data);
    //            callback(true);
    //        }
    //    }
    //}
    */

    #endregion
}
