using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class RewardPanel : MonoBehaviour
{
    public static RewardPanel instance;
    [SerializeField] private Image playerReward;
    [SerializeField] private Text valueOfReward;

    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        
    }
    public void RewardPerEveryHardLevel(Sprite spr)
    {
        playerReward.sprite = spr ;
    }
    public void RewardPerEveryHardLevel(int value)
    {
        valueOfReward.text = "x" + value;
    }
    public void RewardPopUp(Sprite spr,int value)
    {
        playerReward.sprite = spr;
        valueOfReward.text = "x" + value;
    }
}
