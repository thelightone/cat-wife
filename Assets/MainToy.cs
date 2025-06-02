using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainToy : MonoBehaviour
{
    public List<ToyItem> toyDatas = new List<ToyItem>();
    public PetData petData;
    private Image image;

    private void OnEnable()
    {
        image = GetComponent<Image>();
        var selectedToy = toyDatas[PlayerPrefs.GetInt("selectedToy", 0)];
        image.sprite = selectedToy.itemIcon;
        petData.SetParametersPerPet(selectedToy.xpAmount, selectedToy.loveAmount);
    }
}
