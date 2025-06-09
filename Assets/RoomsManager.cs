
using Playgama;
using Playgama.Modules.Advertisement;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomsManager : MonoBehaviour
{
    public List<Button> roomsButs = new List<Button>();
    public List<GameObject> rooms = new List<GameObject>();
    public PetData petData;

    [SerializeField] private GameObject tutorGost1;
    [SerializeField] private GameObject tutorGost2;
    [SerializeField] private GameObject tutorShower1;
    [SerializeField] private GameObject tutorFeed1;

    private int _roomId;


    private void Awake()
    {
        if (petData.isSleeping)
        {
            OpenRoom(2);
        }
        else
        {
            OpenRoom(4);
        }
     
        Bridge.advertisement.interstitialStateChanged += OnInterstitialStateChanged;
        OnInterstitialStateChanged(Bridge.advertisement.interstitialState);
    }

    private void OnInterstitialStateChanged(InterstitialState state)
    {
        Debug.Log(state.ToString());

       switch(state)
        {
            case InterstitialState.Loading:
                break;
            case InterstitialState.Opened:
                AudioManager.instance.musicSource.volume = 0;
                break;
            case InterstitialState.Closed:
                AudioManager.instance.musicSource.volume = 1;
                break;
            case InterstitialState.Failed:
                AudioManager.instance.musicSource.volume = 1;
                break;
        }
    }


    public void OpenRoom(int roomId)
    {

        Bridge.advertisement.ShowInterstitial();

        _roomId = roomId;
        for (int i = 0; i < roomsButs.Count; i++)
        {
            rooms[i].SetActive(false);
            roomsButs[i].transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        }

        rooms[roomId].SetActive(true);
        roomsButs[roomId].transform.GetChild(0).GetChild(1).gameObject.SetActive(true);

        Bridge.storage.Get(new List<string>() { "tutorGost", "tutorShower" ,"tutorFeed" }, OnStorageGetCompleted);
    }

    private void OnStorageGetCompleted(bool success, List<string> data)
    {

        if (_roomId ==4 && data[0] ==null)
        {
            Bridge.storage.Set("tutorGost", 1, OnStorageSetCompleted);
            //PlayerPrefs.SetInt("tutorGost", 1);
            tutorGost1.SetActive(true);
        }

        if (_roomId == 0 && data[1] == null)
        {
            Bridge.storage.Set("tutorShower", 1, OnStorageSetCompleted);
            //PlayerPrefs.SetInt("tutorShower", 1);
            tutorShower1.SetActive(true);
        }

        if (_roomId == 3 && data[2] == null)
        {
            Bridge.storage.Set("tutorFeed", 1, OnStorageSetCompleted);
           // PlayerPrefs.SetInt("tutorFeed", 1);
            tutorFeed1.SetActive(true);
        }

    }

    private void OnStorageSetCompleted(bool obj)
    {
        Debug.Log("Open room");
    }
}
