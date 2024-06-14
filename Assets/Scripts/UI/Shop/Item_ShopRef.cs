using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_ShopRef : MonoBehaviour
{

    [SerializeField] private Sprite[] item_Spr;
    [SerializeField] private Image item_ShopIcon;
    [SerializeField] private Text item_ShopValue;
    // Start is called before the first frame update
    private void SetItemPrice_ShopData(Item_Shop item)
    {
        item_ShopIcon.sprite = item.type == Item_Shop.ItemType.Coins_small ? item_Spr[0] :
            item_ShopIcon.sprite = item.type == Item_Shop.ItemType.Coins_medium ? item_Spr[1] :
            item_ShopIcon.sprite = item.type == Item_Shop.ItemType.Coins_large ? item_Spr[2] :
            item_ShopIcon.sprite = item.type == Item_Shop.ItemType.Chest_small ? item_Spr[3] :
            item_ShopIcon.sprite = item.type == Item_Shop.ItemType.Chest_medium ? item_Spr[4] : item_Spr[5];

        item_ShopValue.text = "x" + item.value.ToString();    
    }
    private void SetItemPack_ShopData(ItemPack_Shop item)
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
