using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Image mainBackGround;
    private Sprite boardSpr;


    [Space(3)]
    [Header("---------Pause Scene---------")]
    public GameObject pauseObj;
    [SerializeField] private GameObject pauseBtnArea;

    public bool isPause;

    [Header("--------Wheel Spin--------")]
    public GameObject wheelSpinScene;
    public GameObject wheelInside;

    [Header("---------Gallery---------")]
    public GameObject galleryScene;

    [Header("---------Setting---------")]
    public GameObject settingScene;
    public GameObject settingBG;

    [Header("---------Daily---------")]
    public GameObject dailyScene;
    public Button exitDailyBtn;

    [Header("----------LeveL Select----------")]
    public GameObject LevelScene;
    [SerializeField] private Transform levelContainerPos;
    [SerializeField] private GameObject btnPrefab;
    [SerializeField] private Sprite[] btnSprs;
    private List<GameObject> btnsContainer = new List<GameObject>();

    [Header("---------GamePlay---------")]
    public GameObject gamePlayScene;
    public GameObject noticeSign;
    public GameObject NoticeImg;
    public Button claimRewardBtn;
    public Button exitBtn;
    public Button pauseBtn;
    public Text LevelDisplay;
    public Text NoticeDisplay;
    public Text DialogDisplay;

    [Header("---------MainMenu---------")]
    public GameObject mainMenuBtns;
    public GameObject blackScreen;
    public GameObject transitionScene;
    public GameObject overLay;
    private bool isHide;
    private bool isVibOn;

    [Space(3)]
    [Header("---------- Pop Up ----------")]
    public GameObject popUP;
    [SerializeField] private Button addMoreItemByCoinBtn;
    [SerializeField] private GameObject coinPopUp;
    [SerializeField] private Text popUpTilte;
    [SerializeField] private Image popUpItemImg;
    [SerializeField] private Sprite[] popUpItemSprs;
    [SerializeField] private GameObject rewardPanel;

    [Space(7)]
    [Header("-------- Result ---------")]
    public GameObject resultContainer;
    public GameObject levelFinishPhase;
    public GameObject missionPhase;
    public GameObject rewardGivingPhase;
    public Image fillBar;
    [SerializeField] private Text levelCompleteTxt;

    public Text rewardValueTxt;

    [Space(8)]
    [Header("------Win Pop Up------")]
    public GameObject winScene;
    public GameObject partical;
    public GameObject ribbon;
    [SerializeField] private Image rewardImg;
    [SerializeField] private Sprite[] rewardChest;

    [Space(8)]
    [Header("------Lose Pop Up------")]
    public GameObject loseScene;



    [Header("--------Player Data UI--------")]

    [SerializeField] private Text[] CoinDisplay;
    public int coin;
    public Text[] ItemsInPlayerData;
    public Image[] addMoreItems;
    public Button[] powerBtns;
    public int reverseValue, bonusTimeTicketValue, unlockHoleValue, replayValue, destroyScrewValue;
    public int value;
    public string n;
    int lvla;
    // Start is called before the first frame update
    void Awake()
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
    private void Start()
    {
        isVibOn = true;
        //boardSpr = GameManager.Instance.board.GetComponent<SpriteRenderer>().sprite;
    }
    #region SmallScene
    public void DailyOpen()
    {
        LeanTween.alpha(mainMenuBtns.GetComponent<RectTransform>(), 0f, 0.4f);                      //-> Main menu
        LeanTween.alpha(dailyScene.GetComponent<RectTransform>(), 1f, 0.5f);                      //-> Daily
        LeanTween.scale(dailyScene.GetComponent<RectTransform>(), Vector3.one, 0.2f);
        LeanTween.alpha(overLay.GetComponent<RectTransform>(), 0.5f, 0.3f);
        overLay.GetComponent<Image>().raycastTarget = true;
    }
    public void DailyClose()
    {
        LeanTween.alpha(dailyScene.GetComponent<RectTransform>(), 0f, 0.2f);
        LeanTween.scale(dailyScene.GetComponent<RectTransform>(), Vector3.zero, 0.3f);
        LeanTween.alpha(mainMenuBtns.GetComponent<RectTransform>(), 1f, 0.4f);
        LeanTween.alpha(overLay.GetComponent<RectTransform>(), 0f, 0.2f);
        overLay.GetComponent<Image>().raycastTarget = false;
    }
    public void GalleryOpen()
    {
        galleryScene.SetActive(true);
        LeanTween.alpha(mainMenuBtns.GetComponent<RectTransform>(), 0f, 0.2f);                      //-> Main menu
        LeanTween.alpha(galleryScene.GetComponent<RectTransform>(), 1f, 0.4f);                      //-> Gallery
        LeanTween.scale(galleryScene.GetComponent<RectTransform>(), Vector3.one, 0.3f);
        LeanTween.alpha(overLay.GetComponent<RectTransform>(), 0.5f, 0.2f);
        overLay.GetComponent<Image>().raycastTarget = true;
    }
    public void GalleryOff()
    {
        LeanTween.alpha(galleryScene.GetComponent<RectTransform>(), 0f, 0.2f);
        LeanTween.scale(galleryScene.GetComponent<RectTransform>(), Vector3.zero, 0.4f);
        LeanTween.alpha(mainMenuBtns.GetComponent<RectTransform>(), 1f, 0.3f);
        LeanTween.alpha(overLay.GetComponent<RectTransform>(), 0f, 0.3f);
        overLay.GetComponent<Image>().raycastTarget = false;
    }
    public void WheelSpinOn()
    {
        wheelSpinScene.SetActive(true);
        LeanTween.alpha(wheelSpinScene.GetComponent<RectTransform>(), 1f, 0.1f);
        LeanTween.alpha(mainMenuBtns.GetComponent<RectTransform>(), 0.5f, 0.2f);
        LeanTween.scale(wheelSpinScene.GetComponent<RectTransform>(), Vector2.one, 0.2f);
        LeanTween.moveLocalY(wheelInside, 0f, 0.3f);
        LeanTween.alpha(overLay.GetComponent<RectTransform>(), 0.5f, 0.3f);
        overLay.GetComponent<Image>().raycastTarget = true;
        WheelSpinController.instance.wheelOn = true;
    }
    public void WheelSpinOff()
    {
        LeanTween.alpha(wheelSpinScene.GetComponent<RectTransform>(), 0f, 0.1f);
        LeanTween.scale(wheelSpinScene.GetComponent<RectTransform>(), Vector2.zero, 0.2f);
        LeanTween.alpha(mainMenuBtns.GetComponent<RectTransform>(), 1f, 0.2f);
        LeanTween.alpha(overLay.GetComponent<RectTransform>(), 0f, 0.1f);
        overLay.GetComponent<Image>().raycastTarget = false;
        WheelSpinController.instance.wheelOn = false;
        wheelSpinScene.SetActive(false);

    }

    public void ChoseLevelOn()
    {
        LeanTween.alpha(mainMenuBtns.GetComponentInChildren<RectTransform>(), 0f, 0.2f);
        LeanTween.moveLocalX(mainMenuBtns, -1080f, 0.3f).setEase(LeanTweenType.easeOutQuart);
        LevelScene.SetActive(true);
    }
    public void ChoseLevelOff()
    {
        LevelScene.SetActive(false);
        LeanTween.alpha(mainMenuBtns.GetComponentInChildren<RectTransform>(), 1f, 0.2f);
        LeanTween.moveLocalX(mainMenuBtns, 0f, 0.3f).setEase(LeanTweenType.easeOutQuart);

    }
    public void NoticeOn()
    {
        isPause = true;
        blackScreen.GetComponent<Image>().raycastTarget = true;
        LeanTween.alpha(noticeSign.GetComponentInChildren<RectTransform>(), 1f, 0.5f);
        LeanTween.alpha(blackScreen.GetComponent<RectTransform>(), 0.7f, 0.3f);
        LeanTween.moveLocalX(noticeSign, 0f, 1f).setDelay(0.1f).setEase(LeanTweenType.easeOutQuart);
        LeanTween.moveLocalY(DialogDisplay.gameObject, -90f, 0.5f);
    }
    public void NoticeOnWithoutImg()
    {
        isPause = true;
        blackScreen.GetComponent<Image>().raycastTarget = true;
        LeanTween.alpha(noticeSign.GetComponentInChildren<RectTransform>(), 1f, 0.5f);
        LeanTween.alpha(blackScreen.GetComponent<RectTransform>(), 0.7f, 0.3f);
        LeanTween.moveLocalX(noticeSign, 0f, 1f).setDelay(0.1f).setEase(LeanTweenType.easeOutQuart);
        NoticeImg.gameObject.SetActive(false);
        LeanTween.moveLocalY(DialogDisplay.gameObject, 0f, 0.5f);
        claimRewardBtn.gameObject.SetActive(false);
    }
    public void NoticeOff()
    {
        LeanTween.alpha(blackScreen.GetComponent<RectTransform>(), 0f, 0.3f);
        blackScreen.GetComponent<Image>().raycastTarget = false;
        LeanTween.alpha(noticeSign.GetComponentInChildren<RectTransform>(), 0f, 0.3f);
        LeanTween.moveLocalX(noticeSign, -1080f, 1f).setDelay(0.1f).setEase(LeanTweenType.easeOutQuart);
        PopUpOff();
        NoticeImg.gameObject.SetActive(true);
        claimRewardBtn.gameObject.SetActive(true);
        isPause = false;
    }
    public void PopUpOn()
    {
        isPause = true;
        popUP.SetActive(true);
        LeanTween.alpha(coinPopUp.GetComponent<RectTransform>(), 1f, 0.3f);
        blackScreen.GetComponent<Image>().raycastTarget = true;
        LeanTween.alpha(popUP.GetComponentInChildren<RectTransform>(), 1f, 0.5f);
        LeanTween.alpha(blackScreen.GetComponent<RectTransform>(), 0.7f, 0.3f);
        LeanTween.moveLocalX(popUP, 0f, 1f).setDelay(0.1f).setEase(LeanTweenType.easeOutQuart);
    }
    public void PopUpOff()
    {
        addMoreItemByCoinBtn.onClick.RemoveAllListeners();
        LeanTween.alpha(blackScreen.GetComponent<RectTransform>(), 0f, 0.3f);
        blackScreen.GetComponent<Image>().raycastTarget = false;
        LeanTween.alpha(coinPopUp.GetComponent<RectTransform>(), 0f, 0.3f);
        LeanTween.alpha(popUP.GetComponentInChildren<RectTransform>(), 0f, 0.3f);
        LeanTween.moveLocalX(popUP, -1080f, 1f).setDelay(0.1f).setEase(LeanTweenType.easeOutQuart);
        popUP.SetActive(false);
        ShowItemsOnUI();
        n = null;
        isPause = false;
    }
    #endregion

    #region Create Level Buttons
    public void InitLevelsButton()
    {
        foreach (LeveLScriptableObject lvl in LeveLManager.Instance.levels)
        {
            GameObject lvlbtn = Instantiate(btnPrefab, levelContainerPos);
            lvlbtn.GetComponent<LevelDetail>().levelObject = lvl;
            lvlbtn.GetComponentInChildren<Text>().text = "" + lvl.levelIndex;
            if (lvl.levelIndex % 5 == 0)
            {
                lvlbtn.GetComponent<Image>().sprite = btnSprs[0];
            }
            lvlbtn.GetComponent<Button>().onClick.AddListener(delegate { ChoseLevelOff(); });
            lvlbtn.GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(LoadLevelGamePlay(lvl.levelIndex, lvl)); });

            if (GameManager.Instance.CheckLevelToUnlock(lvl.levelIndex))
            {
                lvlbtn.GetComponent<LevelDetail>().lockedImg.SetActive(false);
                lvlbtn.GetComponent<Button>().interactable = true;
            }
            else
            {
                lvlbtn.GetComponent<LevelDetail>().lockedImg.SetActive(true);
                lvlbtn.GetComponent<Button>().interactable = false;
            }
            btnsContainer.Add(lvlbtn);
        }
    }

    public void UpdateLevelsButton()
    {
        foreach (var a in btnsContainer)
        {
            if (GameManager.Instance.CheckLevelToUnlock(a.GetComponent<LevelDetail>().levelObject.levelIndex))
            {
                a.GetComponent<LevelDetail>().lockedImg.SetActive(false);
                a.GetComponent<Button>().interactable = true;
            }
            else
            {
                a.GetComponent<LevelDetail>().lockedImg.SetActive(true);
                a.GetComponent<Button>().interactable = false;
            }
        }
    }
    #endregion

    #region GamePlayUI

    public void UpdateUILevelText(int id)
    {
        LevelDisplay.text = "" + id;
        levelCompleteTxt.text = "Level " + id;

    }
    public void LoseScenePopUp()
    {
        isPause = true;
        blackScreen.GetComponent<Image>().raycastTarget = true;
        LeanTween.alpha(blackScreen.GetComponent<RectTransform>(), 0.7f, 0.3f);
        LeanTween.scale(loseScene.GetComponent<RectTransform>(), Vector2.one, 0.3f);
        LeanTween.alpha(loseScene.GetComponent<RectTransform>(), 1f, 0.4f).setDelay(0.5f);
    }
    public void LoseSceneHide()
    {
        LeanTween.alpha(blackScreen.GetComponent<RectTransform>(), 0f, 0.3f);
        blackScreen.GetComponent<Image>().raycastTarget = false;
        LeanTween.alpha(loseScene.GetComponent<RectTransform>(), 0f, 0.3f);
        LeanTween.scale(loseScene.GetComponent<RectTransform>(), Vector2.zero, 0.3f).setDelay(0.5f);
        isPause = false;
    }
    public void WinScenePopUp()
    {
        isPause = true;
        pauseBtn.interactable = false;
        LeanTween.alpha(blackScreen.GetComponent<RectTransform>(), 0.7f, 0.2f);
        blackScreen.GetComponent<Image>().raycastTarget = true;
        LeanTween.scale(winScene.GetComponent<RectTransform>(), Vector2.one, 0.3f);
        LeanTween.rotateAround(partical, Vector3.forward, -360, 10f).setLoopClamp();
        LeanTween.scale(levelFinishPhase.GetComponent<RectTransform>(), Vector2.one, 0.3f);
        LeanTween.alpha(winScene.GetComponent<RectTransform>(), 1f, 0.5f);
        PowerButtonActivision(false);
    }
    public void WinSceneHide()
    {
        LeanTween.alpha(blackScreen.GetComponent<RectTransform>(), 0f, 0.5f);
        blackScreen.GetComponent<Image>().raycastTarget = false;
        LeanTween.alpha(winScene.GetComponent<RectTransform>(), 0f, 0.4f);
        LeanTween.scale(winScene.GetComponent<RectTransform>(), Vector2.zero, 0.3f).setDelay(0.4f);
        isPause = false;
        blackScreen.GetComponent<Image>().raycastTarget = false;
        PowerButtonActivision(true);
        pauseBtn.interactable = true;
    }
    public void AddMoreTime()
    {
        isPause = false;
        GameTimer.Instance.conditionTime += 60;
        GameManager.Instance.UpdateGameState(GameState.GamePlayState);
        LoseSceneHide();
    }
    private void PowerButtonActivision(bool isActive)
    {
        foreach (Button a in powerBtns)
        {
            a.interactable = isActive;
        }
    }
    public void AddMoreItem()
    {
        if (coin > 200)
        {
            value += 1;
            coin -= 200;
            UpdateCoin();
            UpdateItem(n);
        }
        else if (coin < 0)
        {
            coin = 0;
        }
        else
        {
            NoticeOnWithoutImg();
            DialogDisplay.text = "You ran out of coins";
        }
    }
    private void AddMoreMoney()
    {
        foreach (var a in CoinDisplay)
        {
            a.GetComponentInParent<Button>().onClick.AddListener(delegate { WatchAdsForMoney(a, 100); });
        }
    }
    private void WatchAdsForMoney(Text cointxt, int coinValue)
    {
        //Run ads, if runtime still countdown, reward can't be obtain
        cointxt.text = "" + coinValue;
    }

    private void UpdateCoin()
    {
        foreach (var i in CoinDisplay)
        {
            i.text = "" + coin;
        }
    }
    private void UpdateItem(string name)
    {
        switch (name)
        {
            case "undo":
                reverseValue = value;
                ItemsInPlayerData[0].text = "" + reverseValue;
                break;
            case "unscrew":
                destroyScrewValue = value;
                ItemsInPlayerData[1].text = "" + destroyScrewValue;
                break;
            case "replay":
                replayValue = value;
                ItemsInPlayerData[2].text = "" + replayValue;
                break;
            case "unlock hole":
                unlockHoleValue = value;
                break;
            case "bonus time":
                bonusTimeTicketValue = value;
                break;
        }
    }
    private void MinusItem(int v)
    {
        v--;
        value = v;
        UpdateItem(n);
        ShowItemsOnUI();
    }
    #endregion

    #region Main menu
    public void MainMenuScene()
    {
        UpdateLevelsButton();
    }
    public void UpdatePlayerUI()
    {
        UpdateCoin();
        ItemsInPlayerData[0].text = "" + reverseValue;
        ItemsInPlayerData[1].text = "" + destroyScrewValue;

    }
    #endregion

    #region Pause
    public void PauseOn()
    {
        isPause = true;
        LeanTween.scale(pauseObj.GetComponentInChildren<RectTransform>(), Vector2.one, 0.3f);
        LeanTween.scale(pauseBtnArea.GetComponent<RectTransform>(), Vector2.one, 0.2f);
        LeanTween.alpha(pauseObj.GetComponentInChildren<RectTransform>(), 0.5f, 0.3f).setDelay(0.3f);
        /*LeanTween.scale(pauseBtnArea.GetComponent<RectTransform>(), Vector3.one, 0.7f).setDelay(0.5f)*/
        ;
        LeanTween.alpha(pauseBtnArea.GetComponent<RectTransform>(), 1f, 0.4f);
        pauseObj.GetComponent<Image>().raycastTarget = true;


    }
    public void PauseOff()
    {
        pauseObj.GetComponent<Image>().raycastTarget = false;
        LeanTween.alpha(pauseObj.GetComponentInChildren<RectTransform>(), 0f, 0.4f);
        /*LeanTween.scale(pauseBtnArea.GetComponent<RectTransform>(), Vector3.zero, 0.5f).setDelay(0.2f)*/
        ;
        LeanTween.alpha(pauseBtnArea.GetComponent<RectTransform>(), 0f, 0.4f);

        LeanTween.scale(pauseObj.GetComponentInChildren<RectTransform>(), Vector2.zero, 0.3f).setDelay(0.5f);
        LeanTween.scale(pauseBtnArea.GetComponent<RectTransform>(), Vector2.zero, 0.2f).setDelay(0.5f);
        isPause = false;

    }
    #endregion

    #region Game Play
    public void TurnGamePlayOn()
    {
        StartCoroutine(GamePlayOn());
    }
    public void ResetGamePlay()
    {
        StartCoroutine(GamePlayReset());
    }
    public void TurnGamePlayOff()
    {
        exitBtn.onClick.RemoveListener(GameManager.Instance.ReturnMainMenu);
        exitBtn.onClick.RemoveListener(TurnGamePlayOff);
        StartCoroutine(GamePlayOff());
    }
    IEnumerator GamePlayOn()
    {
        LeanTween.alpha(transitionScene.GetComponent<RectTransform>(), 1f, 0.3f).setEase(LeanTweenType.easeOutBack);
        LeanTween.alpha(mainMenuBtns.GetComponent<RectTransform>(), 0f, 0.5f);
        LeanTween.scale(mainMenuBtns.GetComponent<RectTransform>(), Vector2.zero, 0.8f);
        yield return new WaitForSeconds(1f);
        gamePlayScene.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.QuickPlay();
        GameManager.Instance.UpdateGameState(GameState.GamePlayState);
        LeanTween.alpha(transitionScene.GetComponent<RectTransform>(), 0f, 0.5f);
        isPause = false;

    }
    IEnumerator LoadLevelGamePlay(int a, LeveLScriptableObject b)
    {
        lvla = a;
        LeveLManager.Instance.levels[GameManager.Instance.levelNeedToReset] = b;
        LeanTween.alpha(transitionScene.GetComponent<RectTransform>(), 1f, 0.3f).setEase(LeanTweenType.easeOutBack);
        LeanTween.alpha(mainMenuBtns.GetComponent<RectTransform>(), 0f, 0.5f);
        LeanTween.scale(mainMenuBtns.GetComponent<RectTransform>(), Vector2.zero, 0.8f);
        yield return new WaitForSeconds(1f);
        gamePlayScene.SetActive(true);
        GameManager.Instance.LoadLevel(a);
        yield return new WaitForSeconds(0.5f);
        LeanTween.alpha(transitionScene.GetComponent<RectTransform>(), 0f, 0.5f);
        isPause = false;

    }
    IEnumerator GamePlayReset()
    {
        isPause = true;
        LeanTween.alpha(transitionScene.GetComponent<RectTransform>(), 1f, 0.3f).setEase(LeanTweenType.easeOutBack);
        GameManager.Instance.UpdateGameState(GameState.IdleState);
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.ResetLevel();
        LeanTween.alpha(transitionScene.GetComponent<RectTransform>(), 0f, 0.5f);
        isPause = false;
    }
    IEnumerator GamePlayOff()
    {
        PauseOff();
        LeanTween.alpha(transitionScene.GetComponent<RectTransform>(), 1f, 0.3f).setEase(LeanTweenType.easeOutBack);
        GameManager.Instance.ReturnMainMenu();
        LeanTween.scale(mainMenuBtns.GetComponent<RectTransform>(), Vector2.one, 0.5f);
        LeanTween.alpha(mainMenuBtns.GetComponent<RectTransform>(), 1f, 1f);
        GameManager.Instance.SaveData();
        yield return new WaitForSeconds(0.5f);
        gamePlayScene.SetActive(false);
        yield return new WaitForSeconds(1f);
        LeanTween.alpha(transitionScene.GetComponent<RectTransform>(), 0f, 0.3f);
        GameManager.Instance.UpdateGameState(GameState.MainMenuState);
        isPause = true;
    }
    private void ReverserItem()
    {
        if (reverseValue <= 0)      //REVERSE
        {
            powerBtns[0].onClick.RemoveAllListeners();
            reverseValue = 0;
            addMoreItems[0].enabled = true;
            powerBtns[0].onClick.AddListener(delegate { ChangeValuePopUp("undo"); });
            ItemsInPlayerData[0].enabled = false;
        }
        else
        {
            powerBtns[0].onClick.RemoveAllListeners();
            n = "undo";
            addMoreItems[0].enabled = false;
            powerBtns[0].onClick.AddListener(ReverseController.Instance.ActiveReverse);
            powerBtns[0].onClick.AddListener(delegate { MinusItem(reverseValue); });
            ItemsInPlayerData[0].enabled = true;
        }
    }
    private void DestroyScrewItem()
    {
        if (destroyScrewValue <= 0)     //DESTROY SCREW
        {
            destroyScrewValue = 0;
            addMoreItems[1].enabled = true;
            powerBtns[1].onClick.RemoveAllListeners();
            powerBtns[1].onClick.AddListener(delegate { ChangeValuePopUp("unscrew"); });
            ItemsInPlayerData[1].enabled = false;
        }
        else
        {
            n = "unscrew";
            addMoreItems[1].enabled = false;
            powerBtns[1].onClick.RemoveAllListeners();
            powerBtns[1].onClick.AddListener(DestroyScrewController.instance.DestroyPhaseActive);
            powerBtns[1].onClick.AddListener(delegate { MinusItem(destroyScrewValue); });
            ItemsInPlayerData[1].enabled = true;
        }
    }
    public void ShowItemsOnUI()
    {
        UpdateCoin();
        DestroyScrewItem();
        ReverserItem();
    }

    #endregion

    #region Result
    public void NextPage()
    {

    }
    ////public void UpdateProgressBar(PlayerData player)
    //{
    //    foreach (var i in player.lvlsUnlocked)
    //    {
    //        if (fillBar.fillAmount < 1 && i.isFinish)
    //        {
    //            fillBar.fillAmount += 0.2f;
    //        }
    //        else if (fillBar.fillAmount == 1)
    //        {
    //            fillBar.fillAmount = 0;
    //            rewardGivingPhase.SetActive(true);
    //        }
    //    }
    //}
    public void RewardPageOff()
    {
        NoticeOff();
    }
    #endregion

    #region Setting
    public void GameVibration()
    {
        if (isVibOn)
        {
            Handheld.Vibrate();
            isVibOn = false;
        }
        else
        {
            isVibOn = true;
            Debug.Log("No Vibrate");
        }
    }
    public void SettingButtonOn()
    {

        settingScene.SetActive(true);
        LeanTween.alpha(settingBG.GetComponent<RectTransform>(), .6f, 1f);
        settingBG.GetComponent<Image>().raycastTarget = true;
        LeanTween.alpha(settingScene.GetComponent<RectTransform>(), 1f, 0.1f);
        LeanTween.scale(settingScene.GetComponent<RectTransform>(), Vector2.one, 0.2f);
    }
    public void SettingButtonOff()
    {

        LeanTween.alpha(settingScene.GetComponent<RectTransform>(), 0f, 0.1f);
        LeanTween.scale(settingScene.GetComponent<RectTransform>(), Vector2.zero, 0.2f);
        LeanTween.alpha(settingBG.GetComponent<RectTransform>(), 0f, 0.1f);

        settingBG.GetComponent<Image>().raycastTarget = false;
        wheelSpinScene.SetActive(false);
    }
    #endregion

    #region Gallery
    public void ChangeBoardColor(int ID)
    {

    }
    #endregion

    #region PopUp
    public void ChangeValuePopUp(string name)
    {
        popUpTilte.text = "" + name.ToUpper();
        switch (name)
        {
            case "undo":
                popUpItemImg.sprite = popUpItemSprs[0];
                value = reverseValue;
                addMoreItemByCoinBtn.onClick.AddListener(delegate { AddMoreItem(); });
                break;
            case "unscrew":
                popUpItemImg.sprite = popUpItemSprs[1];
                value = destroyScrewValue;
                addMoreItemByCoinBtn.onClick.AddListener(delegate { AddMoreItem(); });
                break;
            case "replay":
                popUpItemImg.sprite = popUpItemSprs[2];
                value = replayValue;
                break;
            case "unlock hole":
                popUpItemImg.sprite = popUpItemSprs[3];
                value = unlockHoleValue;
                break;
            case "bonus time":
                popUpItemImg.sprite = popUpItemSprs[4];
                value = bonusTimeTicketValue;
                break;
        }
        n = name;
        PopUpOn();
    }
    #endregion
}
