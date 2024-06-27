using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DragDropController : MonoBehaviour
{
    public static DragDropController instance;
    public DragDrop screwHit;
    public ScrewHole screwhole;
    public float pullrange = 0.5f;
    public bool moveToConnect;

    [SerializeField] private LayerMask detectHole;

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
    public void CallScrewHole()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, float.PositiveInfinity, detectHole);
        if (hit.collider != null)
        {
            screwhole = hit.collider.gameObject.GetComponent<ScrewHole>();
        }
    
    }
    public void ScrewAndHoles()
    {
        foreach (DragDrop a in ReverseController.Instance.screws)
        {
            a.dragEndCallBack = OnDragEnd;
        }
    }
    private void OnDragEnd(DragDrop dragObjs)
    {
        dragObjs.ChangePosition();
        CallScrewHole();
        if (screwhole && !screwhole.isBlock)
        {
            dragObjs.parentObj.gameObject.transform.position = screwhole.transform.position;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.soundEff[Random.Range(1, 3)]);
            if (Vector2.Distance(dragObjs.parentObj.gameObject.transform.position, screwhole.transform.position) <= pullrange && !screwhole.isBlock)
            {
                dragObjs.parentObj.gameObject.transform.position = screwhole.transform.position;
                // SAVE MOVE HERE -> REVERSE SCRIPT TO HANDLE 
                ResetValue();
            }
            else
            {
                dragObjs.parentObj.gameObject.transform.position = dragObjs.startPos;
                ResetValue();
            }
        }
        else if (dragObjs.parentObj.gameObject.transform.position == DestroyScrewController.instance.transform.position) // Handle position after destroyed screw
        {
            ResetValue();
        }
        else
        {
            dragObjs.parentObj.gameObject.transform.position = dragObjs.startPos;
            ResetValue();
        }
        dragObjs.parentObj.ChangeSprBack();
        dragObjs.drag = false;

    }
    public void ResetValue()
    {
        screwHit = null;
        screwhole = null;
    }
}
