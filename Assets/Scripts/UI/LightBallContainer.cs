using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LightBallContainer : MonoBehaviour
{
    [SerializeField] private LightBall[] lightBallContainer;
    [SerializeField] private Sprite[] lightballSprs;
    float count;
    private void Awake()
    {
        count = 1f;
    }
    private void OnEnable()
    {
        ChangeSprite();
    }
    private void OnDisable()
    {
        LeanTween.pause(this.gameObject);
    }

    void ChangeSprite()
    {
        for(int i = 0; i< lightBallContainer.Length; i++)
        {
            if(i % 2 == 0)
            {
                lightBallContainer[i].lightBallImg.sprite = lightballSprs[0];
            }
            else
            {
                lightBallContainer[i].lightBallImg.sprite = lightballSprs[1];
            }
            lightBallContainer[i].lightBallImg.SetNativeSize();
        }
        LeanTween.rotate(this.gameObject, Vector3.forward, count).setOnComplete(ChangeSpriteOff);
    }
    void ChangeSpriteOff()
    {
        for (int i = 0; i < lightBallContainer.Length; i++)
        {
            if (i % 2 == 0)
            {
                lightBallContainer[i].lightBallImg.sprite = lightballSprs[1];
            }
            else
            {
                lightBallContainer[i].lightBallImg.sprite = lightballSprs[0];
            }
            lightBallContainer[i].lightBallImg.SetNativeSize();
        }
        LeanTween.rotate(this.gameObject, Vector3.forward, count).setOnComplete(ChangeSprite);
    }
    
}
[System.Serializable]
public class LightBall
{
    public Image lightBallImg;
    public bool isOn;
}

