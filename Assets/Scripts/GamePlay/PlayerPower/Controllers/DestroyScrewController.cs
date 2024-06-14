using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyScrewController : MonoBehaviour
{
    public static DestroyScrewController instance;
    [SerializeField] private List<GameObject> destroyedScrews = new List<GameObject>();
    int count;
    public bool DestroyPhaseIsOn;

    private void Awake()
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

    public void ActiveDestroy()
    {
        DragDropController.instance.screwHit.parentObj.transform.position = gameObject.transform.position;
        DestroyPhaseIsOn = false;
    }
    public void DestroyPhaseActive()
    {
        DestroyPhaseIsOn = true;
    }
}
