using UnityEngine;
using UnityEngine.UI;

public class PetManager : MonoBehaviour
{
    [SerializeField] private PetData _petData;
    [SerializeField] private Button _petPetButton;

    private void Start()
    {
        _petPetButton.onClick.AddListener(() => OnPet());
    }

    private void OnPet()
    {
        _petData.ChangeParametersPerPet();
        UIManager.instance.UpdateBalance();
    }
}
