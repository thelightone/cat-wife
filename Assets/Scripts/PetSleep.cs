using UnityEngine;
using System;
using System.Collections.Generic;

public class PetSleep : MonoBehaviour
{
    [SerializeField] private PetData _petData;
    [SerializeField] private GameObject _sleepingVisual; // Визуальный эффект сна
    [SerializeField] private float _energyRecoveryPerMinute = 20f; // Восстановление энергии за минуту сна

    private const string SLEEP_START_TIME_KEY = "SleepStartTime";
    private DateTime _sleepStartTime;

    [SerializeField] private List<GameObject> petsInRooms = new List<GameObject>();
    [SerializeField] private List<GameObject> textsSleep = new List<GameObject>();

    private void Start()
    {
        LoadSleepState();
    }

    private void OnApplicationQuit()
    {
        SaveSleepState();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
            SaveSleepState();
        else
            LoadSleepState();
    }

    public void ToggleSleep()
    {
        if (_petData.isSleeping)
            WakeUp();
        else
            StartSleep();
    }

    private void StartSleep()
    {
        _petData.isSleeping = true;
        _sleepStartTime = DateTime.Now;
        _sleepingVisual.SetActive(true);
        SaveSleepState();
        InvokeRepeating(nameof(UpdateSleepEnergy), 0f, 1f);

        foreach(GameObject pet in petsInRooms)
        {
            pet.SetActive(false);
        }
        foreach(GameObject text in textsSleep)
        {
            text.SetActive(true);
        }
    }

    private void WakeUp()
    {
        _petData.isSleeping = false;
        _sleepingVisual.SetActive(false);
        CancelInvoke(nameof(UpdateSleepEnergy));
        
        TimeSpan sleepDuration = DateTime.Now - _sleepStartTime;
        int minutesSlept = (int)sleepDuration.TotalMinutes;
        int energyRecovery = Mathf.RoundToInt(minutesSlept * _energyRecoveryPerMinute);
        
        _petData.CurrentSleep += energyRecovery;
        UIManager.instance.UpdateBalance();
        
        PlayerPrefs.DeleteKey(SLEEP_START_TIME_KEY);
        PlayerPrefs.Save();

        foreach (GameObject pet in petsInRooms)
        {
            pet.SetActive(true);
        }
        foreach (GameObject text in textsSleep)
        {
            text.SetActive(false);
        }
    }

    private void UpdateSleepEnergy()
    {
        if (_petData.isSleeping) return;

        TimeSpan sleepDuration = DateTime.Now - _sleepStartTime;
        float minutesPassed = (float)sleepDuration.TotalMinutes;
        float energyRecovery = minutesPassed * _energyRecoveryPerMinute;
        
        _petData.CurrentSleep += Mathf.RoundToInt(energyRecovery);
        UIManager.instance.UpdateBalance();
        
        _sleepStartTime = DateTime.Now;
    }

    private void SaveSleepState()
    {
        if (_petData.isSleeping)
        {
            PlayerPrefs.SetString(SLEEP_START_TIME_KEY, _sleepStartTime.Ticks.ToString());
            PlayerPrefs.Save();
        }
    }

    private void LoadSleepState()
    {
        if (!PlayerPrefs.HasKey(SLEEP_START_TIME_KEY)) return;

        long sleepStartTicks = long.Parse(PlayerPrefs.GetString(SLEEP_START_TIME_KEY));
        _sleepStartTime = new DateTime(sleepStartTicks);
        _petData.isSleeping = true;
        _sleepingVisual.SetActive(true);

        TimeSpan sleepDuration = DateTime.Now - _sleepStartTime;
        int minutesSlept = (int)sleepDuration.TotalMinutes;
        int energyRecovery = Mathf.RoundToInt(minutesSlept * _energyRecoveryPerMinute);
        
        _petData.CurrentSleep += energyRecovery;
        UIManager.instance.UpdateBalance();

        InvokeRepeating(nameof(UpdateSleepEnergy), 0f, 1f);

        foreach (GameObject pet in petsInRooms)
        {
            pet.SetActive(false);
        }
        foreach (GameObject text in textsSleep)
        {
            text.SetActive(true);
        }
    }
} 