
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedManager : MonoBehaviour
{
    [SerializeField] private PetData petData;
    [SerializeField] private List<Image> foodsOnTable = new List<Image>();

    private void Start()
    {
        UpdateInventory(); 
    }

    private void OnEnable()
    {
        UpdateInventory();
    }

    private void UpdateInventory()
    {
        for (var i = 0; i < 3; i++)
        {
            if (petData.foodItems[i] != null)
            {
                foodsOnTable[i].sprite = petData.foodItems[i].itemIcon;
                foodsOnTable[i].gameObject.SetActive(true);
                foodsOnTable[i].GetComponent<DraggableFood>().InitValues(petData.foodItems[i]);
                foodsOnTable[i].transform.parent.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                foodsOnTable[i].gameObject.SetActive(false);
                foodsOnTable[i].transform.parent.GetChild(1).gameObject.SetActive(true);
            }
        }

    }
}
