using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopItemSlot : MonoBehaviour
{
    public FoodItem foodItem;
    [SerializeField] private GameObject selection;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text price;

    private void Start()
    {
        image.sprite = foodItem.itemIcon;
        price.text = foodItem.price.ToString();
    }

    public void Select()
    {
        selection.SetActive(true);
    }

    public void Deselect()
    {
        selection.SetActive(false);
    }
} 