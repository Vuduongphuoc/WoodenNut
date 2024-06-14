using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Gallery")]
    public GameObject galleryScene;
    public Button galleryBtn;

    [Header("Setting")]
    public GameObject settingScene;
    public GameObject settingBG;
    public Button settingBtn;

    [Header("Daily")]
    public GameObject dailyScene;
    public Button dailyBtn;

    [Header("GamePlay")]
    public GameObject gamePlayScene;

    [Header("MainMenu")]
    public GameObject mainMenuBtns;
    public GameObject transitionScene;
    private bool isHide;
    // Start is called before the first frame update

    private bool isVibOn;
    private void Awake()
    {
        isVibOn = true;
    }
    void Start()
    {
        TurnOffSmallScene();
    }

    // Update is called once per frame
    public void TurnOffSmallScene()
    {
        if (galleryScene.activeSelf)
        {
            LeanTween.scale(galleryScene.GetComponent<RectTransform>(), Vector3.zero, 0.5f);   // -> This is Gallery change skins
            LeanTween.alpha(mainMenuBtns.GetComponent<RectTransform>(), 1f, 1f);                          // -> This is all Main menu Buttons
            

        }
        else if (settingScene.activeSelf)
        {
            LeanTween.scale(settingScene.GetComponent<RectTransform>(), Vector3.zero, 0.5f);
            
        }
        else if (dailyScene.activeSelf)
        {

        }

    }
    public void TurnOnGalleryScene()
    {

        galleryScene.SetActive(true);
        LeanTween.alpha(mainMenuBtns.GetComponent<RectTransform>(), 0f, 0.7f);                      //-> Main menu
        LeanTween.alpha(galleryScene.GetComponent<RectTransform>(), 1f, 0.8f);                      //-> Gallery
        LeanTween.scale(galleryScene.GetComponent<RectTransform>(), Vector3.one, 0.5f);
    
    }

    #region Game Play
    public void TurnGamePlayOn()
    {
        StartCoroutine(GamePlayOn());
    }
    public void TurnGamePlayOff()
    {
        StartCoroutine(GamePlayOff());
    }
    IEnumerator GamePlayOn()
    {
        LeanTween.alpha(transitionScene.GetComponent<RectTransform>(), 1f, 0.3f).setEase(LeanTweenType.easeOutBack);
        LeanTween.scale(mainMenuBtns.GetComponent<RectTransform>(), Vector2.zero, 1f);
        LeanTween.alpha(mainMenuBtns.GetComponent<RectTransform>(), 0f, 1f);
        yield return new WaitForSeconds(1f);
        gamePlayScene.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.QuickPlay();
        LeanTween.alpha(transitionScene.GetComponent<RectTransform>(), 0f, 0.5f);
        UIManager.instance.isPause = false;
        StopCoroutine(GamePlayOn());

    }
    IEnumerator GamePlayOff()
    {
        LeanTween.alpha(transitionScene.GetComponent<RectTransform>(), 1f, 0.5f).setEase(LeanTweenType.easeOutBack);
        GameManager.Instance.ReturnMainMenu();
        LeanTween.scale(mainMenuBtns.GetComponent<RectTransform>(), Vector2.one, 0.7f);
        LeanTween.alpha(mainMenuBtns.GetComponent<RectTransform>(), 1f, 0.5f);
        GameManager.Instance.SaveData();
        yield return new WaitForSeconds(0.5f);
        gamePlayScene.SetActive(false);
        yield return new WaitForSeconds(1f);
        LeanTween.alpha(transitionScene.GetComponent<RectTransform>(), 0f, 0.3f);
        UIManager.instance.isPause = true;
        StopCoroutine(GamePlayOff());
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
    public void ExitSettingButtonOn()
    {
        LeanTween.alpha(settingBG.GetComponent<RectTransform>(), .6f, 2f).setDelay(.5f);
        LeanTween.scale(settingScene, new Vector3(1.2f, 1.2f, 1.2f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(settingScene, new Vector3(1f, 1f, 1f), 2f).setDelay(1.5f).setEase(LeanTweenType.easeInOutCubic);
    }
    public void ExitSettingButtonOff()
    {
        LeanTween.scale(settingScene, new Vector3(1.2f, 1.2f, 1.2f), 0.5f).setEase(LeanTweenType.easeOutQuint);
        LeanTween.alpha(settingBG.GetComponent<RectTransform>(), 0f, 1f).setDelay(.5f);
        //LeanTween.scale(settingScene, new Vector3(1f, 1f, 1f), 0.5f).setDelay(.5f).setEase(LeanTweenType.easeInOutBack);
        LeanTween.scale(settingScene, new Vector3(0f, 0f, 0f), 1f).setDelay(0.5f).setEase(LeanTweenType.easeOutCubic);
    }
    #endregion

}
