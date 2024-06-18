using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewHoleObjBlock : MonoBehaviour
{
    private ScrewHole screwhole;
    public bool isNut;
    public bool isStick;
    private void Awake()
    {
        screwhole = GetComponentInParent<ScrewHole>();
    }
    private void Update()
    {
        if(isNut)
        {
            screwhole.isBlock = true;
        }
        else
        {
            screwhole.isBlock = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<WoodStick>())
        {
            isStick= true;
        }
        if (collision.gameObject.GetComponent<WoodNut>())
        {
            isNut = false;
        }
    }
}
