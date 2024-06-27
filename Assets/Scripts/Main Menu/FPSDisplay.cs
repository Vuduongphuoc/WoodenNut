using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    bool isPause;
    float deltaTime = 0f;
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        if (!isPause)
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        }
    }
    private void OnGUI()
    {
        int w = Screen.width, h = Screen.height;
        GUIStyle style = new GUIStyle();
        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.LowerLeft;
        style.fontSize = h * 2 / 100;
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = new Color(1f, 1f, 1f, 1f);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("FPS:{1:0.} ({0:0.0} ms)",msec, fps);
        GUI.Label(rect, text, style);
    }
    private void OnApplicationPause(bool focus)
    {
        isPause = focus;
    }
}
