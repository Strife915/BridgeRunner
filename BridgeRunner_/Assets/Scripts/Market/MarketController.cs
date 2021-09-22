using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketController : MonoBehaviour
{
    public static MarketController instance;
    public GameObject marketMenu;
    public List<MarketItem> items;
    public List<Item> equippedItems;
    
    public void InitializeMarketController()
    {
        instance = this;
        foreach(MarketItem item in items)
        {
            item.InitializeItem();
        }
    }

    public void ActivateMarketMenu(bool active)
    {
        marketMenu.SetActive(active);
    }
    public void DeActivateMarketMenu(bool active)
    {
        marketMenu.SetActive(!active);
    }
}
