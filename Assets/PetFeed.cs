using Unity.VisualScripting;
using UnityEngine;

public class PetFeed : MonoBehaviour
{
    [SerializeField] private PetData data;
    [SerializeField] private UIManager uiManager;

    public void Feed(FoodItem foodItem, int foodId)
    {
        data.CurrentFeed += foodItem.feedAmount;
        data.CurrentXp+= foodItem.xpAmount;
        data.CheckLevelUp();
        uiManager.UpdateBalance();

        data.foodItems[foodId] = null;
    }
}
