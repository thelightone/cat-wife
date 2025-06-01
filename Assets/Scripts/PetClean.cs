using UnityEngine;

public class PetClean : MonoBehaviour
{
    [SerializeField] private PetData _petData;

    public void Clean(int amount)
    {
        _petData.CurrentClean += amount;
        UIManager.instance.UpdateBalance();
    }
} 