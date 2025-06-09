using NUnit.Framework;
using Playgama;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GalleryManager : MonoBehaviour
{
    public List<Image> allImages = new List<Image>();
    public List<Sprite> allSprites = new List<Sprite>();

    private void OnEnable()
    {
        Bridge.storage.Get("CurrentLevel", OnStorageGetCompleted);
    }

    private void OnStorageGetCompleted(bool success, string data)
    {
        var curLevel = Convert.ToInt32(data);
        Debug.Log("CURRENT LEVEL: "+curLevel);
        for (int i = 0; i<curLevel-1; i++)
        {
            allImages[i].sprite = allSprites[i];
            allImages[i].color = Color.white;
        }
    }

    public void ForceUpdate()
    {
        OnEnable();
    }
}
