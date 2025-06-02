using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimManager : MonoBehaviour
{
    public Sprite def;
    public Sprite onPet;
    public Sprite onWash;
    public Sprite beforeFood;
    public Sprite afterFood;

    public List<Image> petImages = new List<Image>();

    public static AnimManager instance;

    private void Start()
    {
        instance = this;
    }

    public void SetState(Sprite state)
    {
        foreach (Image image in petImages)
        {
            image.sprite = state;
        }
    }

    public void SetStateWithTime(Sprite state, int time)
    {
        StopAllCoroutines();
        StartCoroutine(DelayedState(state, time));
        foreach (Image image in petImages)
        {
            image.sprite = state;
        }
    }

    private IEnumerator DelayedState(Sprite state, int time)
    {
        foreach (Image image in petImages)
        {
            image.sprite = state;
        }
        yield return new WaitForSeconds(time);
        DefState();
    }

    public void DefState()
    {
        foreach (Image image in petImages)
        {
            image.sprite = def;
        }
    }
}
