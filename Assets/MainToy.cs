using NUnit.Framework;
using Playgama;
using System;
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

        Bridge.storage.Get("selectedToy", OnStorageGetCompleted);
    }


    private void OnStorageGetCompleted(bool success, string data)
    {
   
        var selectedToy = toyDatas[Convert.ToInt32(data)];
        image.sprite = selectedToy.itemIcon;
        petData.SetParametersPerPet(selectedToy.xpAmount, selectedToy.loveAmount);
    }
}
