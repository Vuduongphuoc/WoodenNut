using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedWoodTrigger : MonoBehaviour
{
    private ScrewHole screwholes;
    private HingeJoint2D hinge;
    private SpringJoint2D spr;

    // Start is called before the first frame update
    void Awake()
    {
        screwholes = GetComponentInParent<ScrewHole>();
        hinge = GetComponent<HingeJoint2D>();
        
    }
    private void OnEnable()
    {
        hinge.connectedBody = screwholes.redWood.GetComponent<Rigidbody2D>();
    }
    private void OnDisable()
    {

        //screwholes.redWood.GetComponent<Rigidbody2D>().AddForce(Vector2.left);
        hinge.connectedBody = null;
    }
}

