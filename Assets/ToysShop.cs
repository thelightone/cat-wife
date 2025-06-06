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
        var curId = PlayerPrefs.GetInt("selectedToy", 0);
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
            PlayerPrefs.SetInt("selectedToy",selectedItem.toyId);
        }
    }
}
