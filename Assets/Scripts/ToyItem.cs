using UnityEngine;

[CreateAssetMenu(fileName = "New Toy Item", menuName = "Shop/Toy")]
public class ToyItem : ScriptableObject
{
    [Header("Item Properties")]
    public Sprite itemIcon;
    
    [Header("Item Effects")]
    public int loveAmount;
    public int xpAmount;
} 