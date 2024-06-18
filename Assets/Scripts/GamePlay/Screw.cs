using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Screw : MonoBehaviour
{
    private DragDrop drop;
    private Sprite spr;
    private Collider2D col;
    [SerializeField] private Sprite[] sprContainer;
    [SerializeField] private Sprite[] normalSprContainer;
    private void Awake()
    {
        drop = GetComponentInChildren<DragDrop>();
        spr = GetComponent<SpriteRenderer>().sprite;
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (drop.drag)
        {
            ChangeSpr();
        }
        else
        {
            ChangeSprBack();
        }
    }
    public void ChangeSpr()
    {
        spr = sprContainer[2];
        gameObject.GetComponent<SpriteRenderer>().sprite = spr;
    }
    public void ChangeSprBack()
    {
        spr = normalSprContainer[2];
        gameObject.GetComponent<SpriteRenderer>().sprite = spr;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Screw")
        {
            this.gameObject.transform.position = drop.startPos;
        }
    }
}
