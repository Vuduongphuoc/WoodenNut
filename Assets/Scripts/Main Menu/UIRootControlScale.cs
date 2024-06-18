using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRootControlScale : MonoBehaviour
{
    [SerializeField] CanvasScaler cvScaler;
    public float rate;
    private void Start()
    {
        rate = 1920f / 1080f;
        float currentRate = (float)Screen.width / (float)Screen.height;
        float scale = currentRate > rate ? 1 : 0;
        cvScaler.matchWidthOrHeight = scale;
    }
}
