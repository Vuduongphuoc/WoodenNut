using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodNut : MonoBehaviour
{
    private WoodStick woodStick;
    private SpriteMask mask;
    private SpriteRenderer sprRender;
    public bool isConnect;
    private void Start()
    {
        woodStick = GetComponentInParent<WoodStick>();
        mask = GetComponent<SpriteMask>();
        sprRender = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Screw>())
        {
            isConnect = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Screw>())
        {
            isConnect = false;
            woodStick.screwList.Remove(collision.gameObject);
        }
    }
}
