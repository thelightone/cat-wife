using NUnit.Framework;
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
        var curLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
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
