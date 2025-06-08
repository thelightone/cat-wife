using Playgama;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Tamagotchi/PetConfig")]
public class PetData : ScriptableObject
{
    // Ключи для PlayerPrefs
    private const string LAST_EXIT_TIME_KEY = "LastExitTime";
    private const string CURRENT_FEED_KEY = "CurrentFeed";
    private const string CURRENT_HAPPINESS_KEY = "CurrentHappiness";
    private const string CURRENT_SLEEP_KEY = "CurrentSleep";
    private const string CURRENT_CLEAN_KEY = "CurrentClean";
    private const string CURRENT_LOVE_KEY = "CurrentLove";
    private const string CURRENT_XP_KEY = "CurrentXp";
    private const string CURRENT_LEVEL_KEY = "CurrentLevel";
    private const string MONEY_BALANCE_KEY = "MoneyBalance";
    private const string IS_SLEEPING_KEY = "IsSleeping";

    public bool isSleeping;

    public int HungerDecreasePerMinute = 5;
    public int HappinessDecreasePerMinute = 3;
    public int SleepnessDecreasePerMinute = 2;
    public int CleanessDecreasePerMinute = 2;
    public int LoveDecreasePerMinute = 2;

    public int XpPerPet = 1;
    public int CurrentXp = 0;
    public int CurrentLevel = 1;
    public int LoveIncreasePerPet = 3;

    [Header("Money Settings")]
    public int BaseMoneyPerMinute = 1;
    public int MoneyBalance;

    [SerializeField] private int currentFeed = 1000;
    public int currentHapiness = 1000;
    public int currentSleep = 1000;
    public int currentClean = 1000;
    public int currentLove = 1000;

    public List<FoodItem> foodItems = new List<FoodItem>();

   public static event Action levelUpEvent;

    public int CurrentFeed
    {
        get => currentFeed;
        set
        {
            currentFeed = Mathf.Clamp(value, 0, 1000);
        }
    }

    public int CurrentHapiness
    {
        get => currentHapiness;
        set
        {
            currentHapiness = Mathf.Clamp(value, 0, 1000);
        }
    }
    public int CurrentSleep
    {
        get => currentSleep;
        set
        {
            currentSleep = Mathf.Clamp(value, 0, 1000);
        }
    }
    public int CurrentClean
    {
        get => currentClean;
        set
        {
            currentClean = Mathf.Clamp(value, 0, 1000);
        }
    }
    public int CurrentLove
    {
        get => currentLove;
        set
        {
            currentLove = Mathf.Clamp(value, 0, 1000);
        }
    }


    public void ChangeParametersPerPet()
    {
        CurrentXp += XpPerPet;
        CheckLevelUp();

        CurrentLove += LoveIncreasePerPet;
    }

    public void ChangeMoney(int amount)
    {
        MoneyBalance += amount;
        if (MoneyBalance < 0) MoneyBalance = 0;
    }

    public void SaveGameState()
    {

        var keys = new List<string>() { LAST_EXIT_TIME_KEY, CURRENT_FEED_KEY, CURRENT_HAPPINESS_KEY, CURRENT_SLEEP_KEY, CURRENT_CLEAN_KEY, CURRENT_LOVE_KEY, CURRENT_XP_KEY, CURRENT_LEVEL_KEY, MONEY_BALANCE_KEY, IS_SLEEPING_KEY };
        var data = new List<object>() { DateTime.Now.Ticks.ToString(), currentFeed,currentHapiness,currentSleep,currentClean,currentLove,CurrentXp,CurrentLevel,MoneyBalance, isSleeping ? 1 : 0 };
        Bridge.storage.Set(keys, data, OnStorageSetCompleted);
    }

    private void OnStorageSetCompleted(bool success)
    {
        Debug.Log($"OnStorageSetCompleted, success: {success}");
    }
    public void LoadGameState()
    {
        Bridge.storage.Get(new List<string>() { LAST_EXIT_TIME_KEY, CURRENT_FEED_KEY, CURRENT_HAPPINESS_KEY, CURRENT_SLEEP_KEY, CURRENT_CLEAN_KEY, CURRENT_LOVE_KEY, CURRENT_XP_KEY, CURRENT_LEVEL_KEY, MONEY_BALANCE_KEY, IS_SLEEPING_KEY }, OnStorageGetCompleted);
        
    }

    private void OnStorageGetCompleted(bool success, List<string> data)
    {
        if (success)
        {
            if (data[0]!=null)
            {
                // Загружаем время последнего выхода
                long lastExitTime = long.Parse(data[0]);
                DateTime lastExit = new DateTime(lastExitTime);

                // Вычисляем прошедшее время
                TimeSpan timeSpan = DateTime.Now - lastExit;
                int minutesPassed = (int)timeSpan.TotalMinutes;


                // Загружаем сохраненные значения
                currentFeed = Convert.ToInt32(data[1]);
                currentHapiness = Convert.ToInt32(data[2]);
                currentSleep = Convert.ToInt32(data[3]);
                currentClean = Convert.ToInt32(data[4]);
                currentLove = Convert.ToInt32(data[5]);
                CurrentXp = Convert.ToInt32(data[6]);
                CurrentLevel = Convert.ToInt32(data[7]);
                MoneyBalance = Convert.ToInt32(data[8]);
                isSleeping = Convert.ToInt32(data[9]) == 1;

                Debug.Log($"LoadGameState - After loading: happiness = {currentHapiness}, isSleeping = {isSleeping}");

                if (minutesPassed > 0)
                {
                    Debug.Log($"Updating parameters for {minutesPassed} minutes");
                    // Обновляем параметры с учетом прошедшего времени
                    UpdateParametersForTimePassed(minutesPassed);
                    Debug.Log($"LoadGameState - After time update: happiness = {currentHapiness}");
                }
            }
            else
            {
                Debug.Log("No saved data found, resetting to default values");
                // Если нет сохраненных данных, устанавливаем начальные значения
                ResetToDefaultValues();
                Debug.Log($"LoadGameState - After reset: happiness = {currentHapiness}");
            }
        }
    }

    private void UpdateParametersForTimePassed(int minutes)
    {
        // Сохраняем начальное значение любви
        int initialLove = currentLove;

        // Уменьшаем параметры за прошедшее время
        currentFeed = Mathf.Max(0, currentFeed - (HungerDecreasePerMinute * minutes));
        currentHapiness = Mathf.Max(0, currentHapiness - (HappinessDecreasePerMinute * minutes));

        // Обработка параметра сна в зависимости от состояния
        if (isSleeping)
        {
            // Если питомец спит, увеличиваем сон на 1% в минуту
            int sleepIncrease = Mathf.RoundToInt(1000f * 0.01f * minutes); // 1% от максимума (1000) в минуту
            currentSleep = Mathf.Min(1000, currentSleep + sleepIncrease);
            Debug.Log($"Pet is sleeping. Sleep increased by {sleepIncrease} points over {minutes} minutes");
        }
        else
        {
            // Если не спит, уменьшаем как обычно
            currentSleep = Mathf.Max(0, currentSleep - (SleepnessDecreasePerMinute * minutes));
        }

        currentClean = Mathf.Max(0, currentClean - (CleanessDecreasePerMinute * minutes));
        currentLove = Mathf.Max(0, currentLove - (LoveDecreasePerMinute * minutes));

        // Начисляем деньги за прошедшее время с учетом снижения любви
        float totalMoney = 0;
        for (int i = 0; i < minutes; i++)
        {
            // Рассчитываем значение любви для каждой минуты
            float loveForMinute = Mathf.Max(0, initialLove - (LoveDecreasePerMinute * i));
            float loveMultiplier = loveForMinute / 1000f;
            totalMoney += BaseMoneyPerMinute * loveMultiplier *CurrentLevel;
            Debug.Log(loveMultiplier + " " + totalMoney);
        }

        MoneyBalance += Mathf.RoundToInt(totalMoney);
        Debug.Log($"Money calculation: Initial love = {initialLove}, Final love = {currentLove}, Minutes = {minutes}, Money earned = {Mathf.RoundToInt(totalMoney)}");
    }

    private void ResetToDefaultValues()
    {
        Debug.Log($"ResetToDefaultValues - Before reset: happiness = {currentHapiness}");
        currentFeed = 500;
        currentHapiness = 500;
        currentSleep = 500;
        currentClean = 500;
        currentLove = 500;
        CurrentXp = 0;
        CurrentLevel = 1;
        MoneyBalance = 0;
        Debug.Log($"ResetToDefaultValues - After reset: happiness = {currentHapiness}");
    }

    public void SetParametersPerPet(int xp, int love)
    {
        XpPerPet = xp;
        LoveIncreasePerPet = love;
    }

    public void CheckLevelUp()
    {
        if (CurrentXp > CurrentLevel * CurrentLevel * 100 && CurrentLevel<=10)
        {
            CurrentLevel++;
            CurrentXp = 0;
            SaveGameState();
            levelUpEvent.Invoke();
        }
    }
}