using NUnit.Framework;
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
    }

    public void OpenRoom (int roomId)
    {
        for (int i = 0; i < roomsButs.Count; i++)
        {
            rooms[i].SetActive(false);
            roomsButs[i].transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        }

        rooms[roomId].SetActive(true);
        roomsButs[roomId].transform.GetChild(0).GetChild(1).gameObject.SetActive(true);

        if(roomId ==4 && PlayerPrefs.GetInt("tutorGost")==0)
        {
            PlayerPrefs.SetInt("tutorGost", 1);
            tutorGost1.SetActive(true);
        }

        if (roomId == 0 && PlayerPrefs.GetInt("tutorShower") == 0)
        {
            PlayerPrefs.SetInt("tutorShower", 1);
            tutorShower1.SetActive(true);
        }

        if (roomId == 3 && PlayerPrefs.GetInt("tutorFeed") == 0)
        {
            PlayerPrefs.SetInt("tutorFeed", 1);
            tutorFeed1.SetActive(true);
        }

    }
}
