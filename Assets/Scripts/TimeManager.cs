using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private PetData _petData;
    
    private float _timeSinceLastMinute = 0f;
    private DateTime _lastUpdateTime;
    
    private void Start()
    {
        _lastUpdateTime = DateTime.Now;
        Debug.Log($"TimeManager started at {_lastUpdateTime}");
    }
    
    private void Update()
    {
        // Проверяем время каждую секунду
        _timeSinceLastMinute += Time.deltaTime;
        
        if (_timeSinceLastMinute >= 1f)
        {
            _timeSinceLastMinute = 0f;
            CheckTimeAndUpdateParameters();
        }
    }
    
    private void CheckTimeAndUpdateParameters()
    {
        DateTime currentTime = DateTime.Now;
        TimeSpan timeSpan = currentTime - _lastUpdateTime;
        
        // Если прошла минута или больше
        if (timeSpan.TotalMinutes >= 1)
        {
            int minutesPassed = (int)timeSpan.TotalMinutes;
            Debug.Log($"Updating parameters for {minutesPassed} minutes passed");
            
            // Сохраняем начальное значение любви для расчета денег
            int startLove = _petData.CurrentLove;
            
            // Обновляем параметры
            _petData.CurrentFeed -= _petData.HungerDecreasePerMinute * minutesPassed;
            _petData.CurrentHapiness -= _petData.HappinessDecreasePerMinute * minutesPassed;
            _petData.CurrentSleep -= _petData.SleepnessDecreasePerMinute * minutesPassed;
            _petData.CurrentClean -= _petData.CleanessDecreasePerMinute * minutesPassed;
            _petData.CurrentLove -= _petData.LoveDecreasePerMinute * minutesPassed;
            
            // Начисляем деньги используя CalculateMoneyForTime
            int moneyToAdd = CalculateMoneyForTime(minutesPassed, startLove);
            _petData.ChangeMoney(moneyToAdd);
            
            _lastUpdateTime = currentTime;
        }
    }

    public int CalculateMoneyForTime(int minutes, int startLove)
    {
        int totalMoney = 0;
        int currentLove = startLove;

        // Рассчитываем деньги за каждую минуту отдельно
        for (int i = 0; i < minutes; i++)
        {
            // Рассчитываем счастье для текущей минуты
            currentLove = Mathf.Max(0, currentLove - _petData.LoveDecreasePerMinute);

            // Добавляем деньги за эту минуту
            float LoveMultiplier = currentLove / 1000f;
            totalMoney += Mathf.RoundToInt(_petData.BaseMoneyPerMinute * LoveMultiplier);
        }

        return totalMoney;
    }
} 