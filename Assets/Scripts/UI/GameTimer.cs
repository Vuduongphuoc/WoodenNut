using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance;
    public Text timerTxt;
    public float conditionTime;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void OnEnable()
    {
        timerTxt.enabled = true;

    }
    private void Update()
    {
        if (!UIManager.instance.isPause && GameManager.Instance.states == GameState.GamePlayState)
        {
            int min = Mathf.FloorToInt(conditionTime / 60);
            int sec = Mathf.FloorToInt(conditionTime % 60);
            if (conditionTime <= 0)
            {
                conditionTime = 0;
                timerTxt.text = "00:00".ToString();
                GameManager.Instance.UpdateGameState(GameState.EndGamePlayState);
                
                //GameManager.Instance.UpdateGameState(GameState.EndGamePlayState);
            }
            else
            {
                conditionTime -= Time.deltaTime;
                timerTxt.text = string.Format("{00:00}:{01:00}", min, sec);
                if(conditionTime == 10)
                {
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.soundEff[4]);
                    timerTxt.color = Color.red;

                }
            }
        }
    }
}
