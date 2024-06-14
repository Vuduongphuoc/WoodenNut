using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TabGroup tabGroup;
    public Sprite[] btnStatus;
    public Image backGround;

    [Space(3)]
    [Header("Gallery button")]

    [SerializeField] private GameObject itemGallery;
    [SerializeField] private Transform pageGrid;
    [SerializeField] private Sprite[] itemSprs;
    [SerializeField] private GameObject TargetItemSpr;
    [SerializeField] private Sprite[] sprsToChange;
    private Sprite itemSprToChange;
    private string nameOfTarget;
    public UnityEvent onTabsSelected;
    public UnityEvent onTabsDeselected;


    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }

    void Start()
    {
        tabGroup.AddTabBtns(this);
        CheckWhichGroupIsSelect(this.name);
        InitGalleryItems();
    }
    private void InitGalleryItems()
    {
        for(int i = 0; i < itemSprs.Length; i++)
        {
            GameObject newItem = Instantiate(itemGallery, pageGrid, false);
            newItem.name = itemSprs[i].name;
            newItem.GetComponent<Image>().sprite = itemSprs[i];
            newItem.GetComponent<Button>().onClick.AddListener(delegate { ChangeSprItem(newItem); }) ;
        }
    }
    private void ChangeSprItem(GameObject a)
    {
        for(int i = 0; i < itemSprs.Length; i++)
        {
            if(a.GetComponent<Image>().sprite == itemSprs[i])
            {
                if (nameOfTarget == "TabScrew")
                {
                    itemSprToChange = sprsToChange[i];
                }
                else
                {
                    itemSprToChange = itemSprs[i];
                }
                UpdateItemSpr(itemSprToChange);
            }
        }
    }
    private void UpdateItemSpr(Sprite b)
    {
        if(nameOfTarget == "TabBoard")
        {
            TargetItemSpr.GetComponent<SpriteRenderer>().sprite = b;
            return;
        }
        else if( nameOfTarget == "TabScrew")
        {
            TargetItemSpr.GetComponent<SpriteRenderer>().sprite = b;
        }
        else
        {
            TargetItemSpr.GetComponentInChildren<Image>().sprite = b;
           
        }
    }
    private void CheckWhichGroupIsSelect(string tabname)
    {
        switch (tabname)
        {
            case "TabBoard":
                TargetItemSpr = Resources.Load("Prefabs/GamePlay/GamePlayBoard") as GameObject;
                itemSprToChange = null;
                nameOfTarget = tabname;
                break;
            case "TabScrew":
                TargetItemSpr = Resources.Load("Prefabs/GamePlay/Screw") as GameObject;
                itemSprToChange = TargetItemSpr.GetComponent<SpriteRenderer>().sprite;
                nameOfTarget = tabname;
                break;
            case "TabBackGround":
                TargetItemSpr = UIManager.instance.mainBackGround.gameObject;
                itemSprToChange = TargetItemSpr.GetComponent<Image>().sprite;
                nameOfTarget = tabname;
                break;
        }
    }
    public void Select()
    {
        if (onTabsSelected != null)
        {
            onTabsSelected.Invoke();
        }
        
    }
    public void Deselect()
    {
        if (onTabsDeselected != null)
        {
            onTabsDeselected.Invoke();
        }

    }
}
