using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WoodStick : MonoBehaviour
{
    private GameObject holes;
    private GameObject screws;
    public WoodBoard board;
    public GameObject areaEffectStick;
    [Header("Color Area")]
    [Header("0.Red, 1.Yellow, 2.Green, 3.Blue, 4.Gray" + "\n" + "5.Black, 6.White, 7.Cyan, 8.Unique Color")]
    public TypeOfColor color;

    [Header("Wood Shape Area")] public bool UniqueActive;
    public GameObject[] uniqueShape;
    public int shapeID;

    [Space(3)]
    private Rigidbody2D body;
    private SpriteRenderer objectSprite;
    private GameObject ShapeChildObj;
    private SpawnNut spawnNut;
    public List<GameObject> screwList = new List<GameObject>();
    bool selfDestruct;
    bool delayAfterMoveScrew;
    bool AntiForcePrevent;
    int count;
    int directForce;
    float direc;
    GameObject removeObj;
    public bool stopAddForce;
    Collider2D thisCollider;
    Transform objTransform;
    bool isDestroy;
    // Start is called before the first frame update
    private void Awake()
    {

        body = GetComponent<Rigidbody2D>();
        objectSprite = GetComponent<SpriteRenderer>();
        objTransform = GetComponent<Transform>();
        UpdateStickColors(color);
        selfDestruct = false;
    }
    private void Start()
    {
        thisCollider = GetComponent<Collider2D>();
        spawnNut = GetComponent<SpawnNut>();
        direc = Random.Range(-0.5f, 0.5f);
        body.angularVelocity = 0;
        ActiveUniqueShape(shapeID);
        UpdateStickColors(color);

    }
    private void OnDisable()
    {
        Destroy(this.gameObject);
    }
    public void SetBoard(WoodBoard a)
    {
        board = a;

    }
    private void Update()
    {
        if (isDestroy == false && this.gameObject.transform.position.y <= -6f && board != null)
        {
            directForce++;
            if (directForce == 20)
            {
                board.RemoveStick(this);
            }
            else if(directForce == 25)
            {
                this.gameObject.SetActive(false);
                isDestroy = true;
            }
        }
        ApplyGravity();
        AddForce();
        //if (!stopAddForce && !AntiForcePrevent && screwList.Count == 0)
        //{
        //    directForce++;
        //    if (directForce == 50 )
        //    {
        //        objTransform.position = Vector2.MoveTowards(objTransform.position,
        //            Vector2.Lerp(objTransform.position, new Vector2(objTransform.position.x + direc, objTransform.position.y), 0.1f), 0.1f);
        //        directForce = 0;
        //    }

        //}
    }
    #region ChangeColorArea
    public void UpdateStickColors(TypeOfColor typeColor)
    {
        switch (typeColor)
        {
            case TypeOfColor.Red:
                RedColorState();
                break;
            case TypeOfColor.Yellow:
                YellowColorState();
                break;
            case TypeOfColor.Green:
                GreenColorState();
                break;
            case TypeOfColor.Blue:
                BlueColorState();
                break;
            case TypeOfColor.Gray:
                GrayColorState();
                break;
            case TypeOfColor.Black:
                BlackColorState();
                break;
            case TypeOfColor.White:
                WhiteColorState();
                break;
            case TypeOfColor.Cyan:
                CyanColorState();
                break;
            //case TypeOfColor.Pink:
            //    PinkColorState();
            //    break;
            case TypeOfColor.Unique:
                UniqueColor();
                break;
        }
    }
    private void CyanColorState()
    {
        gameObject.layer = 11;
        objectSprite.color = Color.cyan;
        objectSprite.sortingOrder = -19;
        gameObject.tag = "CyanWood";
    } //CYAN ID 7
    private void WhiteColorState()
    {
        gameObject.layer = 10;
        objectSprite.color = Color.white;
        objectSprite.sortingOrder = -17;
        gameObject.tag = "WhiteWood";
    } // WHITE ID 6
    private void BlackColorState()
    {
        gameObject.layer = 12;
        objectSprite.color = Color.black;
        objectSprite.sortingOrder = -15;
        gameObject.tag = "BlackWood";
    } // BLACK ID 5
    private void GrayColorState()
    {
        gameObject.layer = 14;
        objectSprite.color = Color.gray;
        objectSprite.sortingOrder = -13;
        gameObject.tag = "GrayWood";
    } // GRAY ID 4
    private void BlueColorState()
    {
        gameObject.layer = 13;
        objectSprite.color = Color.blue;
        objectSprite.sortingOrder = -11;
        gameObject.tag = "BlueWood";
    } // BLUE ID 3
    private void RedColorState()
    {
        gameObject.layer = 7;
        objectSprite.color = Color.red;
        objectSprite.sortingOrder = -5;
        gameObject.tag = "RedWood";
    } // RED ID 0
    private void YellowColorState()
    {
        gameObject.layer = 8;
        objectSprite.color = Color.yellow;
        objectSprite.sortingOrder = -7;
        gameObject.tag = "YellowWood";
    } //YELLOW ID 1
    private void GreenColorState()
    {
        gameObject.layer = 9;
        objectSprite.color = Color.green;
        objectSprite.sortingOrder = -9;
        gameObject.tag = "GreenWood";
    } // GREEN ID 2
    //private void PinkColorState()
    //{
    //    gameObject.layer = 6;
    //    objectSprite.color = new Color(255, 104, 220);
    //    objectSprite.sortingOrder = -3;
    //    gameObject.tag = "PinkWood";
    //}
    private void UniqueColor()
    {
        gameObject.layer = 23;
        gameObject.tag = "Unique";
    } //Unique
    #endregion

    #region ChangeWoodShape
    //
    private void ActiveUniqueShape(int ID)
    {
        if (UniqueActive)
        {
            GameObject newObj = Instantiate(uniqueShape[ID], transform.position, Quaternion.identity);
            newObj.transform.parent = gameObject.transform;
            newObj.transform.rotation = gameObject.transform.rotation;
            //childObj = newObj     ;
        }
    }
    #endregion
    public void CountScrew()
    {
        if (screwList.Count > 1)
        {
            for (int i = 0; i < screwList.Count; i++)
            {
                for (int y = i + 1; y < screwList.Count; y++)
                {
                    if (screwList[y].transform.position == screwList[i].transform.position)
                    {
                        screwList.RemoveAt(y);
                    }
                }
            }
        }
    }
    private void AddForce()
    {
        if (spawnNut.woodNuts.Any(c => c.GetComponent<WoodNut>().isConnect))
        {
            stopAddForce = true;
        }
        else
        {
            stopAddForce = false;
            AntiForcePrevent = false;
        }
    }
    public void ApplyGravity()
    {
        if (screwList.Count <= 1 && !delayAfterMoveScrew)
        {
            body.gravityScale = 1f;
            body.bodyType = RigidbodyType2D.Dynamic;
        }
        else if(screwList.Count == 0)
        {
            body.gravityScale = 1.5f;
        }
        else if (this.gameObject.CompareTag("Unique"))
        {
            body.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            body.bodyType = RigidbodyType2D.Static;
        }
    }
    private IEnumerator PreventGlitchWhenAddGravity()
    {
        body.bodyType = RigidbodyType2D.Kinematic;
        delayAfterMoveScrew = true;
        yield return new WaitForSeconds(0.03f);
        delayAfterMoveScrew = false;
        body.bodyType = RigidbodyType2D.Dynamic;
    }
    public void CheckThisIsUniqueOrNot()
    {
        if (!this.gameObject.CompareTag("Unique"))
        {
            StartCoroutine(PreventGlitchWhenAddGravity());
        }
    }
    private void SelfDestruct()
    {
        selfDestruct = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Screw>())
        {
            screwList.Add(collision.gameObject);
            CountScrew();
            CheckThisIsUniqueOrNot();
            AntiForcePrevent = true;
        }
        else if (collision.gameObject.GetComponent<WoodStick>())
        {
            AntiForcePrevent = true;
        }
        else if (collision.gameObject.GetComponent<ScrewConnectTrigger>())
        {
            Physics2D.IgnoreCollision(thisCollider, collision);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TriggerPoint>())
        {
            body.bodyType = RigidbodyType2D.Static;
            gameObject.GetComponent<Collider2D>().enabled = false;
            SelfDestruct();
        }
        else if (collision.gameObject.GetComponent<ScrewConnectTrigger>())
        {
            direc = -direc;
        }
    }

}
public enum TypeOfColor
{
    Red, Yellow, Green, Blue, Gray, Black, White, Cyan, Unique
}
