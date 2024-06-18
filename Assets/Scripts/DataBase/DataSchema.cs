using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserData
{
    [SerializeField]
    public UserPlayProgress playProgress;
    [SerializeField]
    public UserInventory inventory;
    [SerializeField]
    public Dictionary<string, MissionData> dic_Mission = new Dictionary<string, MissionData>();
}

[Serializable]
public class UserPlayProgress
{
    public int currentLevel;
    public Dictionary<string, LevelData> levelDic = new Dictionary<string, LevelData>();
    public int totalStar;
    public string lastTimeLogin;
    public bool dailyClaim;
    public int dailyLoginProgress;
    public List<int> dailyMissions;
    public int dailyMissionCount;
    public string lastTimeSpin;
    public float curLevelBtnPos_X;
    public List<float> curLevelBtnAvatarPos;
}

[Serializable]
public class UserInventory
{
    public List<int> charSkinOwned = new List<int>();
    public int totalGold;
    public int currentSkinId;
}

[Serializable]
public class LevelData
{
    public int levelID;
    public int starResult;
}

[Serializable]
public class MissionData
{
    public int id = 0;
    public int number = 0;
    public bool isClaimed = false;
}
