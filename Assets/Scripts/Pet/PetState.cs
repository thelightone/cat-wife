using UnityEngine;

[System.Serializable]
public class PetState
{
    public int Hunger = 100;
    public int Happiness = 100;
    public int Energy = 100;
    public int Experience = 0;
    public int Level = 1;

    public void AddExperience(int amount)
    {
        Experience += amount;
        // Каждые 100 опыта повышаем уровень
        if (Experience >= Level * 100)
        {
            Level++;
            Experience = 0;
        }
    }
} 