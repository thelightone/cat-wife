using Playgama;
using System;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class ToysShop : MonoBehaviour
{
    public PetData petData;
    public Image equipedItemImage;

    [SerializeField] private List<ShopToySlot> allItems = new List<ShopToySlot>();

    private ShopToySlot selectedItem;
    private ShopToySlot equipedItem;

    private void Start()
    {
        Bridge.storage.Get("selectedToy", OnStorageGetCompleted);
    }

    private void OnStorageGetCompleted(bool success, string data)
    {
        var curId = Convert.ToInt32(data);
        selectedItem = allItems[curId];
        Equip();
    }

    public void SelectItem(ShopToySlot item)
    {
        foreach (ShopToySlot item2 in allItems)
        {
            item2.UnHighlight();
        }

        item.Highlight();
        selectedItem = item;
    }

    public void Buy()
    {
        if (selectedItem.price <= petData.MoneyBalance)
        {
            petData.MoneyBalance -= selectedItem.price;
            selectedItem.Unlock();
            UIManager.instance.UpdateBalance();
            selectedItem.Buy();
        }
    }

    public void Equip()
    {
        if(selectedItem.bought)
        {
            equipedItem = selectedItem;
            equipedItemImage.sprite = selectedItem.transform.GetChild(0).GetComponent<Image>().sprite;
            Bridge.storage.Set("selectedToy", selectedItem.toyId, OnStorageSetCompleted);
         // PlayerPrefs.SetInt("selectedToy",selectedItem.toyId);
        }
    }

    private void OnStorageSetCompleted(bool obj)
    {
        Debug.Log("select Toy");
    }
}
