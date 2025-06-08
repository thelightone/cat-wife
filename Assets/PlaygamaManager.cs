using UnityEngine;

public class PlaygamaManager : MonoBehaviour
{
    public PetData petData;

    private void Start()
    {
        LoadData();
    }

    public void LoadData()
    {
        petData.LoadGameState();
    }
}
