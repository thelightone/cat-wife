using UnityEngine;

[CreateAssetMenu(menuName = "Tamagotchi/PetConfig")]
public class PetConfig : ScriptableObject
{
    [Header("Decrease per minute")]
    public int HungerDecreasePerMinute = 5;
    public int HappinessDecreasePerMinute = 3;
    public int EnergyDecreasePerMinute = 2;

    [Header("Action amounts")]
    [SerializeField] private int _feedAmount = 20;
    public int FeedAmount
    {
        get => _feedAmount;
        private set => _feedAmount = Mathf.Clamp(value, 0, 1000);
    }
    public int PlayAmount = 15;
    public int SleepAmount = 25;

    [Header("Petting settings")]
    public int PettingHappinessIncrease = 10;
    public int PettingExperienceGain = 5;
    public float PettingCooldown = 1f; // В секундах

    private void OnValidate()
    {
        // Применяем ограничение при изменении в инспекторе
        FeedAmount = _feedAmount;
    }
} 