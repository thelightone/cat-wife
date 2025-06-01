using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class FoodShop : MonoBehaviour
{
    [SerializeField] private List<ShopItemSlot> allItems = new List<ShopItemSlot>();
    [SerializeField] private List<Image> inventoryItems = new List<Image>();
    [SerializeField] private PetData petData;
    private ShopItemSlot selectedItem;

    private void Start()
    {
        UpdateInventory();    
    }

    private void OnEnable()
    {
        UpdateInventory();
    }

    public void SelectItem(ShopItemSlot item)
    {
        foreach (ShopItemSlot item2 in allItems)
        {
            item2.Deselect();
        }

        item.Select();
        selectedItem = item;
    }

    public void BuyItem()
    {
        if (selectedItem.foodItem.price <= petData.MoneyBalance && (petData.foodItems[0] == null || petData.foodItems[1] == null || petData.foodItems[2] == null) )
        {
            petData.MoneyBalance-=selectedItem.foodItem.price;
            UIManager.instance.UpdateBalance();

            if (petData.foodItems[0] == null)
            {
                petData.foodItems[0] = selectedItem.foodItem;
            }
            else if (petData.foodItems[1] == null)
            {
                petData.foodItems[1] = selectedItem.foodItem;
            }
            else if (petData.foodItems[2] == null)
            {
                petData.foodItems[2] = selectedItem.foodItem;
          
            }

            UpdateInventory();
        }
    }

    private void UpdateInventory()
    {
        for (var i = 0; i < 3; i++)
        {
            if (petData.foodItems[i] != null)
            { 
                inventoryItems[i].sprite = petData.foodItems[i].itemIcon;
                inventoryItems[i].color = Color.white;
        }
            else
            {
                inventoryItems[i].color = new Color (0,0,0,0);
            }
        }

    }
}