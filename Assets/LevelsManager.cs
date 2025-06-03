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
        var curLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
        levelText.text = "LEVEL "+curLevel.ToString();
        levelUpImage.sprite = sprite[curLevel-2];
        levelUpScreen.SetActive(true);
    }
}
