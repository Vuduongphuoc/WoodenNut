using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueTrigger : MonoBehaviour
{
    private ScrewHole screwholes;
    [SerializeField] private List<HingeJoint2D> newHinge = new List<HingeJoint2D>();
    bool triggerInActive;
    // Start is called before the first frame update
    void Awake()
    {
        screwholes = GetComponentInParent<ScrewHole>();
    }
    private void OnEnable()
    {
        foreach (GameObject a in screwholes.uniqueShape)
        {
            gameObject.AddComponent<HingeJoint2D>().connectedBody = a.GetComponent<Rigidbody2D>();
        }
    }
}
