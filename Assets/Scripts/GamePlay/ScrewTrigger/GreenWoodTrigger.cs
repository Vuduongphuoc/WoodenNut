using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenWoodTrigger : MonoBehaviour
{
    private ScrewHole screwholes;
    private HingeJoint2D hinge;

    // Start is called before the first frame update
    void Awake()
    {
        screwholes = GetComponentInParent<ScrewHole>();
        hinge = GetComponent<HingeJoint2D>();
    }
    private void OnEnable()
    {
        hinge.connectedBody = screwholes.greenWood.GetComponent<Rigidbody2D>();
        //screwholes.greenWood.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 0.1f);
    }
    private void OnDisable()
    {
       
        hinge.connectedBody = null;
    }
}
