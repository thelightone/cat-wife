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
        Debug.Log($"SaveGameState - Before save: happiness = {currentHapiness}");
        // Сохраняем время выхода
        PlayerPrefs.SetString(LAST_EXIT_TIME_KEY, DateTime.Now.Ticks.ToString());

        // Сохраняем все параметры
        PlayerPrefs.SetInt(CURRENT_FEED_KEY, currentFeed);
        PlayerPrefs.SetInt(CURRENT_HAPPINESS_KEY, currentHapiness);
        PlayerPrefs.SetInt(CURRENT_SLEEP_KEY, currentSleep);
        PlayerPrefs.SetInt(CURRENT_CLEAN_KEY, currentClean);
        PlayerPrefs.SetInt(CURRENT_LOVE_KEY, currentLove);
        PlayerPrefs.SetInt(CURRENT_XP_KEY, CurrentXp);
        PlayerPrefs.SetInt(CURRENT_LEVEL_KEY, CurrentLevel);
        PlayerPrefs.SetInt(MONEY_BALANCE_KEY, MoneyBalance);
        PlayerPrefs.SetInt(IS_SLEEPING_KEY, isSleeping ? 1 : 0);

        PlayerPrefs.Save();
        Debug.Log($"SaveGameState - After save: happiness = {currentHapiness}");
    }

    public void LoadGameState()
    {
        Debug.Log($"LoadGameState - Start: happiness = {currentHapiness}");
        if (PlayerPrefs.HasKey(LAST_EXIT_TIME_KEY))
        {
            // Загружаем время последнего выхода
            long lastExitTime = long.Parse(PlayerPrefs.GetString(LAST_EXIT_TIME_KEY));
            DateTime lastExit = new DateTime(lastExitTime);

            // Вычисляем прошедшее время
            TimeSpan timeSpan = DateTime.Now - lastExit;
            int minutesPassed = (int)timeSpan.TotalMinutes;

            Debug.Log($"Last exit time: {lastExit}");
            Debug.Log($"Current time: {DateTime.Now}");
            Debug.Log($"Time span: {timeSpan}");
            Debug.Log($"Minutes passed: {minutesPassed}");

            // Загружаем сохраненные значения
            currentFeed = PlayerPrefs.GetInt(CURRENT_FEED_KEY, currentFeed);
            currentHapiness = PlayerPrefs.GetInt(CURRENT_HAPPINESS_KEY, currentHapiness);
            currentSleep = PlayerPrefs.GetInt(CURRENT_SLEEP_KEY, currentSleep);
            currentClean = PlayerPrefs.GetInt(CURRENT_CLEAN_KEY, currentClean);
            currentLove = PlayerPrefs.GetInt(CURRENT_LOVE_KEY, currentLove);
            CurrentXp = PlayerPrefs.GetInt(CURRENT_XP_KEY, CurrentXp);
            CurrentLevel = PlayerPrefs.GetInt(CURRENT_LEVEL_KEY, CurrentLevel);
            MoneyBalance = PlayerPrefs.GetInt(MONEY_BALANCE_KEY, MoneyBalance);
            isSleeping = PlayerPrefs.GetInt(IS_SLEEPING_KEY, 0) == 1;

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
        currentFeed = 1000;
        currentHapiness = 1000;
        currentSleep = 1000;
        currentClean = 1000;
        currentLove = 1000;
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