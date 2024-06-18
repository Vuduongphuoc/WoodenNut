using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScrewConnectTrigger : MonoBehaviour
{
    private ScrewHole screwhole;
    [SerializeField] private List<Collider2D> colliIgnore;
    [SerializeField] private Collider2D cir;
    private void Awake()
    {
        screwhole = GetComponentInParent<ScrewHole>();
        cir = this.gameObject.GetComponent<Collider2D>();
    }
    private void OnEnable()
    {
        //cir.isTrigger = true;
        //foreach (var i in screwhole.listWoodSticks)
        //{
        //    //if (i.GetComponent<BoxCollider2D>())
        //    //{
        //    //    Physics2D.IgnoreCollision(cir, i.GetComponent<BoxCollider2D>());
        //    //}
        //    //else if (i.GetComponent<PolygonCollider2D>())
        //    //{
        //    //    Physics2D.IgnoreCollision(cir, i.GetComponent<PolygonCollider2D>());
        //    //}
        //    //else if (i.GetComponent<CircleCollider2D>())
        //    //{
        //    Physics2D.IgnoreCollision(cir, i.GetComponent<Collider2D>());
        //    //}
        //    //else
        //    //{
        //    //    return;
        //    //}
        //    colliIgnore.Add(i.GetComponent<Collider2D>());

        //}
        foreach (var a in screwhole.uniqueShape)
        {
            Physics2D.IgnoreCollision(cir, a.GetComponent<PolygonCollider2D>());
            colliIgnore.Add(a.GetComponent<PolygonCollider2D>());
        }
    }
    private void OnDisable()
    {
        cir.isTrigger = false;
        colliIgnore.Clear();
    }
}
