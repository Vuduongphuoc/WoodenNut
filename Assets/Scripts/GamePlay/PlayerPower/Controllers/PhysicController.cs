using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PhysicController : MonoBehaviour
{
    public static PhysicController instance;
    private float pullPower;
    [SerializeField] private GameObject[] pullObjects;
    private float LeftPull;
    private float RightPull;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    //public void CallToPull(GameObject a)
    //{
    //    ReverseController.Instance.FindSpawnNut();
    //    if (ReverseController.Instance.sticks.Count != 0)
    //    {
    //        for (int i = 0; i < ReverseController.Instance.sticks.Count; i++)
    //        {
    //            if (a == ReverseController.Instance.sticks[i].gameObject)
    //            {
    //                PullPhase(ReverseController.Instance.sticks[i]);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        return;
    //    }
    //}
    private void ApplyObjectVector(Vector2 a)
    {
        LeftPull = Vector2.Distance(pullObjects[0].transform.position, a);
        RightPull = Vector2.Distance(pullObjects[1].transform.position, a);
    }
    public void PullPhase(SpawnNut i)
    {
        if (i.noConnectState && GameManager.Instance.inGamePlay)
        {
            ApplyObjectVector(i.transform.position);
            pullPower = 0.15f * Time.deltaTime;
            if (i.transform.rotation.z > 0f && i.transform.rotation.z <= 180f)
            {
                i.gameObject.transform.position = new Vector2(i.gameObject.transform.position.x - pullPower, i.gameObject.transform.position.y);
                return;
            }
            else if (i.transform.rotation.z > -180f && i.transform.rotation.z <= 0f)
            {
                i.gameObject.transform.position = new Vector2(i.gameObject.transform.position.x + pullPower, i.gameObject.transform.position.y);
                return;
            }
        }

    }
}
