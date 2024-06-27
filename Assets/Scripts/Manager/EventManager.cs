using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager singleton;
    public event Action DragDropEvent;
    public event Action DestroyEvent;
    public event Action ReverseEvent;

    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !UIManager.instance.isPause)
        {
           DragDropEvent?.Invoke(); 
        }
    }
    public void DestroyScrew()
    {
        DestroyEvent?.Invoke();
    }
    public void ReverseAction()
    {
        ReverseEvent?.Invoke();
    }
}
