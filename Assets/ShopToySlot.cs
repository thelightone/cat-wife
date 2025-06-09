using Playgama;
using System;
using TMPro;
using UnityEngine;

public class ShopToySlot : MonoBehaviour
{
    public int toyId;
    public bool bought;
    public int price;

    private void Start()
    {
        Bridge.storage.Set("toy0", 1, OnStorageSetCompleted);
       // PlayerPrefs.SetInt("toy0", 1);

        transform.GetComponentInChildren<TMP_Text>().text = price.ToString();

        Bridge.storage.Get("toy" + toyId, OnStorageGetCompleted);
    }

    private void OnStorageGetCompleted(bool success, string data)
    {
        if (data=="1")
        {
            Unlock();
        }
    }

    private void OnStorageSetCompleted(bool obj)
    {
        Debug.Log("toy saved");
    }

    public void Highlight()
    {
        transform.GetChild(1).gameObject.SetActive(true);
    }
    public void UnHighlight()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void Unlock()
    {
        transform.GetChild(2).gameObject.SetActive(false);
        bought = true;
    }

    public void Buy()
    {
        Bridge.storage.Set("toy"+toyId, 1, OnStorageSetCompleted);
        //PlayerPrefs.SetInt("toy" + toyId, 1);
    }

}
