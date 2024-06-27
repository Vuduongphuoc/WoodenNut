using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffectStick : MonoBehaviour
{
    private AreaEffector2D areaEffect;
    private WoodStick gameObj;
    // Start is called before the first frame update
    private void Start()
    {
        gameObj = GetComponentInParent<WoodStick>();
        areaEffect = GetComponent<AreaEffector2D>();
    }
    void OnEnable()
    {
        this.gameObject.layer = gameObj.gameObject.layer;
        StartCoroutine(AreaPullActive());
    }
    // Update is called once per frame
    private IEnumerator AreaPullActive()
    {
        yield return new WaitForSeconds(1f);
        areaEffect.colliderMask = this.gameObject.layer;
        areaEffect.forceAngle = 0;
        areaEffect.forceMagnitude = 95;
        areaEffect.drag = 15;
    }
}
