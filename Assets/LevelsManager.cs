using Playgama;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelsManager : MonoBehaviour
{
    public List<Sprite> sprite = new List<Sprite> ();
    public GameObject levelUpScreen;
    public TMP_Text levelText;
    public Image levelUpImage;


    void OnEnable()
    {
       PetData.levelUpEvent += OnLevelUp;
    }

    void OnDisable()
    {
        PetData.levelUpEvent -= OnLevelUp;
    }

    public void OnLevelUp()
    {
        Bridge.storage.Get("CurrentLevel", OnStorageGetCompleted);
    }

    private void OnStorageGetCompleted(bool success, string data)
    {
        var curLevel = Convert.ToInt32(data);
        levelText.text = "спнбемэ "+curLevel.ToString();
        levelUpImage.sprite = sprite[curLevel-2];
        levelUpScreen.SetActive(true);
    }
}
