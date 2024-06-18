using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Profiling;

public class SpawnNut : MonoBehaviour
{
    private WoodStick wodstik;
    public List<Vector3> PosReverse = new List<Vector3>();
    public List<Quaternion> RotReverse = new List<Quaternion>();
    public bool noConnectState;
    public List<GameObject> woodNuts = new List<GameObject>();
    private GameObject s;
    public bool isSpawnState;
    public bool pullPhys;
    public Rigidbody2D body;
    int count;
    [SerializeField] private GameObject woodNut;

    private void Start()
    {
        count = 0;
        wodstik = GetComponent<WoodStick>();

    }
    private void OnEnable()
    {
        isSpawnState = true;
    }
    private void Update()
    {
        if (!CheckAllNutIsConnect() && !this.gameObject.CompareTag("Unique"))
        {
            noConnectState = true;
            //PhysicController.instance.CallToPull(this.gameObject);
        }
        else
        {
            noConnectState = false;
            return;
        }

        if (woodNuts.Count < wodstik.screwList.Count)
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

    private void Inition(GameObject a)
    {
        GameObject newNut = Instantiate(woodNut, a.transform.position,Quaternion.identity);
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

