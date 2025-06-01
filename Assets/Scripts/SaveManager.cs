using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private PetData _petData;

    private void Start()
    {
        LoadGame();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveGame();
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void SaveGame()
    {
        if (_petData != null)
        {
            _petData.SaveGameState();
        }
    }

    public void LoadGame()
    {
        if (_petData != null)
        {
            _petData.LoadGameState();
        }
    }
} 