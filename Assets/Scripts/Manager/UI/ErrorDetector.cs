using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorDetector : MonoBehaviour
{
    [SerializeField] private Text errorTxt;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        closeButton.onClick.AddListener(() => Hide());
    }
    private void Start()
    {
        Application.logMessageReceived += Application_logMessageReceived;
        
        Hide();
    }

    private void Application_logMessageReceived(string condition, string stackTrace, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception)
        {
            Show();
            errorTxt.text = "Error: " + condition + "\n" + stackTrace;
        }
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= Application_logMessageReceived;
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
