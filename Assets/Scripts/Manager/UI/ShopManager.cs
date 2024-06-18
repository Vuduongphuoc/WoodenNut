using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Item_ShopRef item_Shop;
    [SerializeField] private List<Item_Shop> item;
    [SerializeField] private Transform itemContainerGrid;
    private List<Item_ShopRef> item_ShopPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void InitItems()
    {
        item_ShopPrefabs = new List<Item_ShopRef>();
        for (int i = 0; i < item.Count; i++)
        {
            item_ShopPrefabs.Add(Instantiate(item_Shop, itemContainerGrid, false));
        }
    }
}
