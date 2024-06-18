using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiSetting : MonoBehaviour
{
    public Button pauseBtn;

    private void Start()
    {
        pauseBtn = GetComponent<Button>();
    }
}
