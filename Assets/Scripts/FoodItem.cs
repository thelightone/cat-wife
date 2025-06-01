using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop/Item")]
public class FoodItem : ScriptableObject
{
    [Header("Item Properties")]
    public string itemName;
    public Sprite itemIcon;
    public int price;
    
    [Header("Item Effects")]
    public int feedAmount;
    public int xpAmount;
    
    [Header("Item Type")]
    public ItemType itemType;


    public enum ItemType
    {
        Food,
        Toy,
        Medicine
    }
} 