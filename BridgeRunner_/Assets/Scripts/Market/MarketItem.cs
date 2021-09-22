using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketItem : MonoBehaviour
{
    public int itemId, wearId;
    public int price;

    public Text priceText;
    public Button buyButton, equipButton, unequipButton;

    public GameObject itemPrefab;

    public bool HasItem()
    {
        //0 : Not bought yet 
        //1 : Buyed but not equiped
        //2 : Buyed and Equiped
        bool hasItem = PlayerPrefs.GetInt("item" + itemId.ToString()) != 0;
        return hasItem;
    }
    public bool IsEquipped()
    {
        
        bool equippedItem = PlayerPrefs.GetInt("item" + itemId.ToString()) == 2;
        return equippedItem;
    }
    public void InitializeItem()
    {
        priceText.text = price.ToString();
        if(HasItem())
        {
            buyButton.gameObject.SetActive(false);
            if(IsEquipped())
            {
                EquipItem();
            }
            else
            {
                equipButton.gameObject.SetActive(true);
            }
        }
        else
        {
            buyButton.gameObject.SetActive(true);
        }
    }
    public void BuyItem()
    {
        if(!HasItem())
        {
            int money = PlayerPrefs.GetInt("money");
            if (money >= price)
            {
                MenuSounds.instance.PlayBuySound();
                LevelController.instance.GiveMoneyToPlayer(-price);
                PlayerPrefs.SetInt("item" + itemId.ToString(),1);
                buyButton.gameObject.SetActive(false);
                equipButton.gameObject.SetActive(true);
            }
        }
                    
    }

    public void EquipItem()
    {
        UnEquipItem();
        MarketController.instance.equippedItems[wearId] = Instantiate(itemPrefab, PlayerEquips.instance.wearSpots[wearId].transform).GetComponent<Item>();
        MarketController.instance.equippedItems[wearId].itemId = itemId;
        equipButton.gameObject.SetActive(false);
        unequipButton.gameObject.SetActive(true);
        PlayerPrefs.SetInt("item" + itemId.ToString(), 2);
    }
    public void UnEquipItem()
    {
        Item equippeItem = MarketController.instance.equippedItems[wearId];
        if(equippeItem != null)
        {
            MarketItem marketItem = MarketController.instance.items[equippeItem.itemId];
            PlayerPrefs.SetInt("item" + marketItem.itemId, 1);
            marketItem.equipButton.gameObject.SetActive(true);
            marketItem.unequipButton.gameObject.SetActive(false);
            Destroy(equippeItem.gameObject);
        }
    }

}
