using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadData :MonoBehaviour
{
    public static SaveLoadData instance;
    public PlayerData playerData;
    public Items playerItems;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
[System.Serializable]
public class PlayerData
{
    public List<int> lvlsUnlocked = new List<int>();
    public Items Item;
}
[System.Serializable]
public class Items
{
    public int coins;
    public int reverseItem;
    public int destroyScrewItem;
    public int unlockHoleItem;
    public int bonusTimeItem;
}


//[System.Serializable]
//public class LevelObjects
//{
//    public List<LeveLScriptableObject> lvlContainer =new List<LeveLScriptableObject>();
//    public int LevelID;
//    public bool isFinish;

//}
