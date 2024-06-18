using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static event Action<GameState> GameStateChange;

    public GameState states;
    public bool inGamePlay;
    public SaveLoadData saveLoadData;
    public PlayerData playerData;
    public Items playerItem;

    public int levelNeedToReset;
    private bool isPause;
    private int bonusCoin;
    private int finishLevelReward;
    private bool isAdsBonus;

    public WoodBoard board;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }
    public void Start()
    {
        playerData = new PlayerData();
        playerItem = new Items();
        UpdateGameState(GameState.OpenGameState);
    }

    #region Game States
    public void UpdateGameState(GameState newState)
    {
        states = newState;

        switch (newState)
        {
            case GameState.IdleState:
                IdleState();
                break;

            case GameState.OpenGameState:
                OpenGame();
                break;

            case GameState.MainMenuState:
                MainMenu();
                break;

            case GameState.TakeInformationState:
                MainMenu();
                break;

            case GameState.GamePlayState:
                GamePlay();
                break;
            case GameState.PowerUpState:
                break;

            case GameState.EndGamePlayState:
                EndGame();
                break;

            case GameState.BonusState:
                break;

            case GameState.FinishGamePlaytState:
                FinishGame();
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        GameStateChange?.Invoke(newState);
    }
    #endregion
    private void IdleState()
    {
        inGamePlay = false;
        LoadData();
    }
    private void OpenGame()
    {
        LoadData();
        UpdatePlayerDataToUI();
        UIManager.instance.ShowItemsOnUI();
        UIManager.instance.InitLevelsButton();
    }
    private void MainMenu()
    {

    }
    private void GamePlay()
    {
        inGamePlay = true;
        board = FindObjectOfType<WoodBoard>();
    }
    private void EndGame()
    {
        inGamePlay = false;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.soundEff[5]);
        UIManager.instance.LoseScenePopUp();
        UpdateGameState(GameState.IdleState);
    }
    private void BonusPointThroughTimer()
    {
        if (GameTimer.Instance.conditionTime >= 60)
        {
            bonusCoin = 50;
        }
        else if (GameTimer.Instance.conditionTime >= 120)
        {
            bonusCoin = 150;
        }
        else
        {
            bonusCoin = 0;
        }
        UIManager.instance.coin += bonusCoin;
    }
    private void BonusPointFromAds()
    {
        //Watch Ads to x2 reward
    }
    private void RewardFinishedLevel()
    {
        if (levelNeedToReset % 5 == 0)
        {
            finishLevelReward = 5 * levelNeedToReset; // Money get after finish level 
        }
        else
        {
            finishLevelReward = 2 * levelNeedToReset;
        }

        if (isAdsBonus)
        {
            finishLevelReward = finishLevelReward * 2;
            UIManager.instance.coin += finishLevelReward;
            UIManager.instance.rewardValueTxt.text = "x" + finishLevelReward;
        }
        else
        {
            UIManager.instance.coin += finishLevelReward;
            UIManager.instance.rewardValueTxt.text = "x" + finishLevelReward;
        }
    }
    private void FinishGame()
    {
        inGamePlay = false;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.soundEff[0]);
        RewardFinishedLevel();
        UpdateNewLevelToData(levelNeedToReset);
        ReverseController.Instance.ClearSpawnNut();
        UIManager.instance.WinScenePopUp();
        BonusPointThroughTimer();
        BonusPointFromAds();
        UpdatePlayerDataFromUI();
        SaveData();
        //UIManager.instance.UpdateProgressBar(playerData);
        UpdateGameState(GameState.IdleState);
    }
    #region Data to Json
    public void LoadData()
    {
        string filePath = Application.persistentDataPath + "/PlayerData.json";
        if (!File.Exists(filePath))
        {
            Initial();
        }
        else
        {
            string data = File.ReadAllText(filePath);
            if (data == "")
            {
                Initial();
            }
            else
            {
                playerData = JsonUtility.FromJson<PlayerData>(data);
                playerItem = playerData.Item;
                UpdatePlayerDataToUI();
            }
        }
    }
    public void SaveData()
    {
        string data = JsonUtility.ToJson(playerData, true);
        string filePath = Application.persistentDataPath + "/PlayerData.json";
        File.WriteAllText(filePath, data);
    }
    public void DeleteData()
    {
        string filePath = Application.persistentDataPath + "/PlayerData.json";
        if (!File.Exists(filePath))
        {
            Debug.Log("Warning! No Data found");
        }
        else
        {
            string data = File.ReadAllText(filePath);
            playerData = JsonUtility.FromJson<PlayerData>(data);
            playerData.lvlsUnlocked.Clear();
            File.Delete(filePath);
            LoadData();
            UIManager.instance.UpdateLevelsButton();
        }
    }
    private void UpdateNewLevelToData(int lvl)
    {
        if (lvl == levelNeedToReset)
        {
            print("Duplicate");
        }
        else
        {
            playerData.lvlsUnlocked.Add(lvl);
            if (lvl == LeveLManager.Instance.levels.Last().levelIndex)
            {
                UIManager.instance.claimRewardBtn.gameObject.SetActive(false);
                UIManager.instance.exitBtn.gameObject.SetActive(true);
                UIManager.instance.NoticeOn();
                UIManager.instance.DialogDisplay.text = "You have reached" + "\n the last level";
                UIManager.instance.exitBtn.onClick.AddListener(UIManager.instance.TurnGamePlayOff);
                UIManager.instance.exitBtn.onClick.AddListener(ReturnMainMenu);
            }
        }
    }
    public void UpdatePlayerDataFromUI()
    {
        playerItem.coins = UIManager.instance.coin;
        playerItem.reverseItem = UIManager.instance.reverseValue;
        playerItem.destroyScrewItem = UIManager.instance.destroyScrewValue;
        playerItem.unlockHoleItem = UIManager.instance.unlockHoleValue;
        playerItem.bonusTimeItem = UIManager.instance.bonusTimeTicketValue;
        //playerData.removeScrew = UIManager.instance.ticket;
    }
    private void UpdatePlayerDataToUI()
    {
        UIManager.instance.coin = playerItem.coins;
        UIManager.instance.reverseValue = playerItem.reverseItem;
        UIManager.instance.destroyScrewValue = playerItem.destroyScrewItem;
        UIManager.instance.unlockHoleValue = playerItem.unlockHoleItem;
        UIManager.instance.bonusTimeTicketValue = playerItem.bonusTimeItem;
        //UIManager.instance.ticket = playerData.removeScrew;
        UIManager.instance.UpdatePlayerUI();
    }
    #endregion


    #region Level Manager

    public void QuickPlay()
    {
        foreach (var lvl in LeveLManager.Instance.levels)
        {
            if (CheckLevelIsUnlock(lvl.levelIndex) && lvl.isFinish)
            {
                print("This level " + lvl.levelIndex + " is finish");
            }
            else if (CheckLevelIsUnlock(lvl.levelIndex) && !lvl.isFinish) // Load level is not finish
            {
                print("Current level " + lvl.levelIndex + " is not complete");
                LoadLevel(lvl.levelIndex);
                return;
            }
            else if (!CheckLevelIsUnlock(lvl.levelIndex)) // Load new level
            {
                print("Level " + lvl.levelIndex + " is not in player data. Add level " + lvl.levelIndex + " to player data");
                LoadLevel(lvl.levelIndex);
                return;
            }

        }
    }
    public void Initial()
    {
        LeveLScriptableObject a = LeveLManager.Instance.levels.First();
        playerData.lvlsUnlocked.Add(a.levelIndex);
        playerItem.coins = 300;
        playerItem.unlockHoleItem = 0;
        playerItem.destroyScrewItem = 0;
        playerItem.reverseItem = 0;
        playerItem.bonusTimeItem = 0;
        Debug.Log("Active -> Initial <-");
        SaveData();
    }
    public void LoadLevel(int a)
    {
        LeveLDataManager.instance.levelID = a;
        levelNeedToReset = a;
        LeveLDataManager.instance.LoadLevel();
        DestroyScrewController.instance.DestroyPhaseIsOn = false;
        ReverseController.Instance.FindSpawnNut();
        if (a > 150)
        {
            GameTimer.Instance.conditionTime = 240;
        }
        else
        {
            GameTimer.Instance.conditionTime = 180;
        }
        UIManager.instance.UpdateUILevelText(a);
        Debug.Log("Active -> Load level " + a + " <- ");
        UpdateGameState(GameState.GamePlayState);
    }
    public void NextLevel()
    {
        if (levelNeedToReset == LeveLManager.Instance.levels.Last().levelIndex)
        {
            UIManager.instance.claimRewardBtn.gameObject.SetActive(false);
            UIManager.instance.exitBtn.gameObject.SetActive(true);
            UIManager.instance.NoticeOn();
            UIManager.instance.DialogDisplay.text = "You have reached" + "\n the last level";
        }
        else
        {
            levelNeedToReset = levelNeedToReset + 1;
            LoadLevel(levelNeedToReset);
        }
    }
    public void ResetLevel()
    {
        LoadLevel(levelNeedToReset);
    }
    public void ReturnMainMenu()
    {
        inGamePlay = false;
        LeveLDataManager.instance.ClearLevel();
        UIManager.instance.MainMenuScene();

    }
    public bool CheckLevelToUnlock(int lvl)
    {
        foreach (var lvldata in playerData.lvlsUnlocked)
        {
            if (lvldata == lvl)
            {
                return true;
            }
            else
            {
                continue;
            }
        }
        return false;
    }
    public bool CheckLevelIsUnlock(int lvl)
    {
        if (playerData.lvlsUnlocked.Count > 0)
        {
            foreach (var lvldata in playerData.lvlsUnlocked)
            {
                if (lvldata == lvl)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        else
        {
            print("No Data Found");
            return false;
        }
        return false;
    }
    #endregion

    private void OnApplicationFocus(bool Focus)
    {
        isPause = !Focus;
    }
    private void OnApplicationPause(bool pause)
    {
        isPause = pause;
    }
    private void OnApplicationQuit()
    {
        isPause = true;
    }

}


public enum GameState
{
    IdleState,
    OpenGameState,
    MainMenuState,
    TakeInformationState,
    GamePlayState,
    PowerUpState,
    EndGamePlayState,
    BonusState,
    FinishGamePlaytState,
}