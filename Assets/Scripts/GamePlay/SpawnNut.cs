using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnNut : MonoBehaviour
{
    private WoodStick wodstik;
    public List<Vector3> PosReverse = new List<Vector3>();
    public List<Quaternion> RotReverse = new List<Quaternion>();
    public bool noConnectState;
    public List<GameObject> woodNuts = new List<GameObject>();
    public bool isSpawnState;
    [SerializeField] private GameObject woodNut;
    [SerializeField] private bool useBurst;
    float timePull = 0.05f;
    float nextTimePull, nextTimeToChange, pullSpd, rotationAngle;
    Rigidbody2D gravity;
    Quaternion rotationZ;

    private void Start()
    {
        wodstik = GetComponent<WoodStick>();
        gravity = GetComponent<Rigidbody2D>();
        rotationZ = gameObject.transform.rotation;
        pullSpd = 0.005f;
    }
    private void OnEnable()
    {
        if (this.gameObject.tag == "Unique")
        {
            isSpawnState = false;
        }
        else
        {
            isSpawnState = true;
        }
    }
    private void Update()
    {
        if (!CheckAllNutIsConnect() && !this.gameObject.CompareTag("Unique"))
        {
            noConnectState = true;
            if (ShouldPullPhysic() && !UIManager.instance.isPause)
            {
                Pull();
            }
            if (ShouldChangeDirection())
            {
                ChangeDirection();
            }
            //PhysicController.instance.CallToPull(this.gameObject);
        }
        else
        {
            noConnectState = false;
            return;
        }

        if (woodNuts.Count < wodstik.screwList.Count && isSpawnState)
        {
            foreach (GameObject a in wodstik.screwList)
            {
                Inition(a);
            }
        }
        else
        {
            isSpawnState = false;
        }
    }
    private void Pull()
    {
        nextTimePull = Time.time + timePull;
        if (rotationZ.z >= -181f && rotationZ.z <= -91f || rotationZ.z >= -1f && rotationZ.z <= 91f)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x - pullSpd, gameObject.transform.position.y);
        }
        else if (rotationZ.z > -91f && rotationZ.z < -1f  || rotationZ.z > 91f && rotationZ.z < 181f)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x + pullSpd, gameObject.transform.position.y);
        }
    }
    private void ChangeDirection()
    {
        nextTimeToChange = Time.time + 4f;
        pullSpd += 0.0001f;
        rotationZ.z = transform.rotation.z;
    }
    private bool ShouldChangeDirection()
    {
        return Time.time > nextTimeToChange;
    }
    private bool ShouldPullPhysic()
    {
        return Time.time > nextTimePull;
    }
    private void Inition(GameObject a)
    {
        GameObject newNut = Instantiate(woodNut, a.transform.position, Quaternion.identity);
        newNut.transform.parent = gameObject.transform;
        newNut.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder - 1;
        woodNuts.Add(newNut);

    }
    private bool CheckAllNutIsConnect()
    {
        if (GameManager.Instance.inGamePlay)
        {
            if (woodNuts.Any(c => c.GetComponentInChildren<WoodNut>().isConnect))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    public void SavePosition()
    {
        PosReverse.Add(transform.position);
        RotReverse.Add(transform.rotation);
    }
    private void OnDestroy()
    {
        ReverseController.Instance.sticks.Remove(this);
    }
}
//[BurstCompile]
//public struct HandlePullPhysic : IJob
//{
//    float nextTimePull;
//    float timePull;
//    Quaternion rotationZ;
//    Vector2 a;
//    public void Execute()
//    {
//        timePull = 0.05f;
//        nextTimePull = Time.time + timePull;
//        if (rotationZ.z > -180f && rotationZ.z < -90f || rotationZ.z > 0f && rotationZ.z < 90f)
//        {
//            a = new Vector2(a.x - 0.005f, a.y);
//        }
//        else if (rotationZ.z > -90f && rotationZ.z < 0f || rotationZ.z > 90f && rotationZ.z < 180f)
//        {
//            a = new Vector2(a.x + 0.005f, a.y);
//        }
//    }
//}

