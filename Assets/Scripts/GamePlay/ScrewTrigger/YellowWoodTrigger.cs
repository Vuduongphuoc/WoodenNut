using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowWoodTrigger : MonoBehaviour
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
       
        hinge.connectedBody = screwholes.yellowWood.GetComponent<Rigidbody2D>();
    }
    private void OnDisable()
    {
        hinge.connectedBody = null;
    }
}
