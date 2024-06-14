using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class ScrewHole : MonoBehaviour
{

    [Header("Position")]

    public bool stickTriggers;
    public bool hasScrew;
    public bool isBlock;

    public List<WoodNut> nutDetected = new List<WoodNut>();
    public List<GameObject> objBlocks = new List<GameObject>();
    public List<GameObject> listWoodSticks = new List<GameObject>();
    public DragDrop screws;
    private CircleCollider2D circol;

    [Header("Wood Detect")]
    public GameObject redWood;
    public GameObject yellowWood;
    public GameObject greenWood;
    public GameObject blueWood;
    public GameObject grayWood;
    public GameObject blackWood;
    public GameObject whiteWood;
    public GameObject cyanWood;
    public List<GameObject> uniqueShape;

    [Header("Trigger")]
    [SerializeField] private GameObject redIgnore;
    [SerializeField] private GameObject yellowIgnore;
    [SerializeField] private GameObject greenIgnore;
    [SerializeField] private GameObject blueIgnore;
    [SerializeField] private GameObject grayIgnore;
    [SerializeField] private GameObject blackIgnore;
    [SerializeField] private GameObject whiteIgnore;
    [SerializeField] private GameObject cyanIgnore;
    public GameObject uniqueIgnore;

    [Header("TriggerIgnoreNotConnectStick")]
    public GameObject noStickConnect;
    private Vector2 pos;

    private HingeJoint2D[] newUniqueContainer;
    private int dupCount = 0;
    private int blockCount = 0;
    public delegate void DragEndDelegate(DragDrop draggableObject);
    public DragEndDelegate dragEndCallBack;
    // Start is called before the first frame update
    private void Start()
    {
        circol = GetComponent<CircleCollider2D>();
    }
    private void Update()
    {
        if (hasScrew)
        {
            isBlock = true;
            CheckForAnyStickConnect();
        }
        else // => No screw detect;
        {
            CheckIsBlockOrNot();
            NoScrewOnHole();
            DeleteUnique();
        }
    }

    void CheckForAnyStickConnect()
    {
        if (nutDetected.Count != 0)
        {
            if (yellowWood)
            {
                yellowIgnore.SetActive(true);
            }
            if (redWood)
            {
                redIgnore.SetActive(true);
            }
            if (blueWood)
            {
                blueIgnore.SetActive(true);
            }
            if (greenWood)
            {
                greenIgnore.SetActive(true);
            }
            if (cyanWood)
            {
                cyanIgnore.SetActive(true);
            }
            if (grayWood)
            {
                grayIgnore.SetActive(true);
            }
            if (whiteWood)
            {
                whiteIgnore.SetActive(true);
            }
            if (blackWood)
            {
                blackIgnore.SetActive(true);
            }
            if (uniqueShape.Count != 0)
            {
                uniqueIgnore.SetActive(true);
            }
            noStickConnect.SetActive(true);
        }
        else
        {
            noStickConnect.SetActive(true);
        }
    }
    void CheckIsBlockOrNot()
    {
        if (nutDetected.Count == 0 && objBlocks.Count == 0)
        {
            isBlock = false;
        }
        else if (nutDetected.Count != 0)
        {
            foreach (WoodNut i in nutDetected)
            {
                if (Vector2.Distance(i.transform.position, this.transform.position) < 0.05f)
                {
                    isBlock = false;
                }
                else
                {
                    isBlock = true;

                }
            }
        }
        else if (objBlocks.Count != 0)
        {
            foreach(GameObject i in objBlocks)
            {
                if(Vector2.Distance(i.transform.position, this.transform.position) < 0.05f)
                {
                    isBlock = false;
                }
                else
                {
                    isBlock = true;
                }
            }
        }
        else
        {
            isBlock = true;
        }
    }

    private void ResetIgnoreObjs()
    {
        redIgnore.SetActive(false);
        yellowIgnore.SetActive(false);
        greenIgnore.SetActive(false);
        blueIgnore.SetActive(false);
        whiteIgnore.SetActive(false);
        blackIgnore.SetActive(false);
        cyanIgnore.SetActive(false);
        grayIgnore.SetActive(false);
        uniqueIgnore.SetActive(false);
    }
    private void DeleteUnique()
    {
        Destroy(uniqueIgnore.GetComponent<HingeJoint2D>());
    }
    void CheckToConnect()
    {
        float disBetween;
        pos = transform.position;
        foreach (WoodNut i in nutDetected)
        {
            disBetween = Vector2.Distance(i.transform.position, pos);
            if (disBetween == Mathf.Clamp(disBetween, -1f, 1f))
            {
                stickTriggers = true;
            }
            else
            {
                stickTriggers = false;
            }
        }
    }
    public void NoScrewOnHole()
    {
        ResetIgnoreObjs();
        noStickConnect.SetActive(false);
    }
    private void CheckForDuplicate(GameObject sticks)
    {
        listWoodSticks.Add(sticks);
        foreach (var i in listWoodSticks)
        {
            if (i.tag == sticks.tag)
            {
                dupCount++;
                if (dupCount == 2)
                {
                    listWoodSticks.Remove(i);
                    dupCount = 0;
                    return;
                }
            }
            else
            {
                continue;
            }
        }
    }
    private void AddColorWood(GameObject a)
    {
        switch (a.tag)
        {
            case "YellowWood":
                yellowWood = a;
                break;
            case "RedWood":
                redWood = a;
                break;
            case "GreenWood":
                greenWood = a;
                break;
            case "CyanWood":
                cyanWood = a;
                break;
            case "BlueWood":
                blueWood = a;
                break;
            case "GrayWood":
                grayWood = a;
                break;
            case "BlackWood":
                blackWood = a;
                break;
            case "WhiteWood":
                whiteWood = a;
                break;
            default:
                break;
        }
    }
    private void CheckForDulpicateObjBlock(GameObject a)
    {

        objBlocks.Add(a);

        foreach (var i in objBlocks)
        {
            if (i == a)
            {
                blockCount++;
                if (blockCount == 2)
                {
                    objBlocks.Remove(i);
                    blockCount = 0;
                    return;
                }
            }
            else
            {
                continue;
            }
        }
    }
    void DeleteAllWoodColorValue(GameObject a)
    {
        switch (a.tag)
        {
            case "YellowWood":
                listWoodSticks.Remove(a);
                objBlocks.Remove(a);
                break;
            case "RedWood":
                listWoodSticks.Remove(a);
                objBlocks.Remove(a);
                break;
            case "GreenWood":
                listWoodSticks.Remove(a);
                objBlocks.Remove(a);
                break;
            case "CyanWood":
                listWoodSticks.Remove(a);
                objBlocks.Remove(a);
                break;
            case "BlueWood":
                listWoodSticks.Remove(a);
                objBlocks.Remove(a);
                break;
            case "GrayWood":
                listWoodSticks.Remove(a);
                objBlocks.Remove(a);
                break;
            case "BlackWood":
                listWoodSticks.Remove(a);
                objBlocks.Remove(a);
                break;
            case "WhiteWood":
                listWoodSticks.Remove(a);
                objBlocks.Remove(a);
                break;
            default:
                listWoodSticks.Remove(a);
                objBlocks.Remove(a);
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<WoodNut>())
        {
            nutDetected.Add(collision.gameObject.GetComponent<WoodNut>());
            CheckToConnect();
        }
        if (collision.gameObject.GetComponent<DragDrop>())
        {
            hasScrew = true;
            screws = collision.gameObject.GetComponent<DragDrop>();
        }
        if (collision.gameObject.CompareTag("YellowWood"))
        {
            AddColorWood(collision.gameObject);
            CheckForDuplicate(collision.gameObject);
            CheckForDulpicateObjBlock(collision.gameObject);

        }
        if (collision.gameObject.CompareTag("RedWood"))
        {
            AddColorWood(collision.gameObject);
            CheckForDuplicate(collision.gameObject);
            CheckForDulpicateObjBlock(collision.gameObject);

        }
        if (collision.gameObject.CompareTag("GreenWood"))
        {
            AddColorWood(collision.gameObject);
            CheckForDuplicate(collision.gameObject);
            CheckForDulpicateObjBlock(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("BlueWood"))
        {
            AddColorWood(collision.gameObject);
            CheckForDuplicate(collision.gameObject);
            CheckForDulpicateObjBlock(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("GrayWood"))
        {
            AddColorWood(collision.gameObject);
            CheckForDuplicate(collision.gameObject);
            CheckForDulpicateObjBlock(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("CyanWood"))
        {
            AddColorWood(collision.gameObject);
            CheckForDuplicate(collision.gameObject);
            CheckForDulpicateObjBlock(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("BlackWood"))
        {
            AddColorWood(collision.gameObject);
            CheckForDuplicate(collision.gameObject);
            CheckForDulpicateObjBlock(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("WhiteWood"))
        {
            AddColorWood(collision.gameObject);
            CheckForDuplicate(collision.gameObject);
            CheckForDulpicateObjBlock(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Unique"))
        {
            uniqueShape.Add(collision.gameObject);
            CheckForDulpicateObjBlock(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<DragDrop>())
        {
            if (Vector2.Distance(collision.gameObject.transform.position, transform.position) > 0.05f)
            {
                hasScrew = false;
                screws = null;
            }
        }
        if (collision.gameObject.GetComponent<WoodNut>())
        {
            nutDetected.Remove(collision.gameObject.GetComponent<WoodNut>());
        }
        if (collision.gameObject.CompareTag("RedWood"))
        {
            DeleteAllWoodColorValue(collision.gameObject);
            redWood = null;
        }
        if (collision.gameObject.CompareTag("YellowWood"))
        {
            DeleteAllWoodColorValue(collision.gameObject);
            yellowWood = null;
        }
        if (collision.gameObject.CompareTag("GreenWood"))
        {
            DeleteAllWoodColorValue(collision.gameObject);
            greenWood = null;
        }
        if (collision.gameObject.CompareTag("BlueWood"))
        {
            DeleteAllWoodColorValue(collision.gameObject);
            blueWood = null;
        }
        if (collision.gameObject.CompareTag("GrayWood"))
        {
            DeleteAllWoodColorValue(collision.gameObject);
            grayWood = null;
        }
        if (collision.gameObject.CompareTag("CyanWood"))
        {
            DeleteAllWoodColorValue(collision.gameObject);
            cyanWood = null;
        }
        if (collision.gameObject.CompareTag("BlackWood"))
        {
            DeleteAllWoodColorValue(collision.gameObject);
            blackWood = null;
        }
        if (collision.gameObject.CompareTag("WhiteWood"))
        {
            DeleteAllWoodColorValue(collision.gameObject);
            whiteWood = null;
        }
        if (collision.gameObject.CompareTag("Unique"))
        {
            DeleteAllWoodColorValue(collision.gameObject);
            uniqueShape.Remove(collision.gameObject);
        }
    }
}
