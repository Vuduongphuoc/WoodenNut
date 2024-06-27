using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Screw : MonoBehaviour
{
    private DragDrop drop;
    [SerializeField] private Sprite[] sprContainer;
    [SerializeField] private Sprite[] normalSprContainer;
    

    private void OnEnable()
    {
        
    }
    private void Start()
    {
        drop = GetComponentInChildren<DragDrop>();
        ChangeSprBack();
    }
    public void ChangeSpr()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprContainer[2];
    }
    public void ChangeSprBack()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = normalSprContainer[2];
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Screw")
        {
            this.gameObject.transform.position = drop.startPos;
        }
    }
}
