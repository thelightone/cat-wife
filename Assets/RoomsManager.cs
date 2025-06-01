using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomsManager : MonoBehaviour
{
    public List<Button> roomsButs = new List<Button>();
    public List<GameObject> rooms = new List<GameObject>();
    public PetData petData;

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
    }
}
