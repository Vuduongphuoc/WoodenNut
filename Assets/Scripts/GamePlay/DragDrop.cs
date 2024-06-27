using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public bool drag = false;
    private Transform currentPos;
    public Screw parentObj;
    private Camera cam; 
    public Vector3 ObjPos;
    public Vector3 startPos;
    [SerializeField] private LayerMask moveable;
    public delegate void DragEndDelegate(DragDrop draggableObject);
    public DragEndDelegate dragEndCallBack;
    public List<Vector3> screwPos = new List<Vector3>();
    private void Start()
    {
        cam = Camera.main;
        parentObj = GetComponentInParent<Screw>();
        EventManager.singleton.DragDropEvent += DetectObjectToDrag;
    }
    private void DetectObjectToDrag()
    {
        ObjPos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (!drag)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, float.PositiveInfinity, moveable);
            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                DragDropController.instance.screwHit = this.gameObject.GetComponent<DragDrop>();
                startPos = parentObj.gameObject.transform.position;
                parentObj.ChangeSpr();
                AudioManager.Instance.PlaySFX(AudioManager.Instance.soundEff[Random.Range(1, 3)]);
                drag = true;
                //this.gameObject.transform.position = ObjPos;
                //Do Animation right here screw pull up
                if (DestroyScrewController.instance.DestroyPhaseIsOn)   // Reset status after Destroy this screw;   
                {
                    DestroyScrewController.instance.ActiveDestroy();
                    DragDropController.instance.ResetValue();
                    DestroyScrew();
                    drag = false;
                    return;
                }
            }
        }
        else if (drag && !DestroyScrewController.instance.DestroyPhaseIsOn)
        {
            ReverseController.Instance.SaveObjectsMove();
            dragEndCallBack(this);
        }
    }
    private void DestroyScrew()
    {
        if(this.gameObject.transform.position == DestroyScrewController.instance.gameObject.transform.position)
        {
            Destroy(this.gameObject);
        }
    }
    public void SavePosition()
    {
        screwPos.Add(transform.position);
    }
    public void ChangePosition()
    {
        Vector2 newVec = parentObj.gameObject.transform.position;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.soundEff[Random.Range(1,2)]);
        parentObj.gameObject.transform.position = new Vector2(newVec.x, newVec.y + 0.3f);
    }

    private void OnDestroy()
    {
        EventManager.singleton.DragDropEvent -= DetectObjectToDrag;
        screwPos.Clear();
        ReverseController.Instance.screws.Remove(this);
    }
    #region Test Drag
    // public delegate DragDrop -> CallBackDragDrop Function to check for other Sccrews if hole already have it;
    //private Transform dragging = null;
    //private Collider2D col;
    //private Vector3 offset;
    //public string destinationTag = "Hole";

    //// Update is called once per frame
    //private void Awake()
    //{
    //    col = GetComponent<Collider2D>();
    //}

    //private void OnMouseDown()
    //{
    //    offset = transform.position - MouseWorldPosition(); 
    //}
    //private void OnMouseDrag()
    //{
    //    transform.position = MouseWorldPosition() + offset;
    //}
    //private void OnMouseUp()
    //{
    //    col.enabled = false;
    //    var rayOrigin = Camera.main.transform.position;
    //    var RayDirection = MouseWorldPosition() - Camera.main.transform.position;
    //    RaycastHit hitInfo;
    //    if(Physics.Raycast(rayOrigin,RayDirection,out hitInfo))
    //    {
    //        if(hitInfo.transform.tag == destinationTag)
    //        {
    //            transform.position = hitInfo.transform.position;
    //        }
    //    }
    //    transform.GetComponent<Collider2D>().enabled = true;
    //}
    //Vector3 MouseWorldPosition()
    //{
    //    var mouseScreenPos = Input.mousePosition;
    //    mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
    //    return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    //}
    #endregion


}
