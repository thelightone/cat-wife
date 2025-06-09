using Playgama;
using Playgama.Modules.Advertisement;
using System;
using UnityEngine;

public class RewardedAd : MonoBehaviour
{
    public PetData petData;

    private void Start()
    {
        Bridge.advertisement.rewardedStateChanged += OnRewardedStateChanged;
    }

    private void OnRewardedStateChanged(RewardedState state)
    {
        Debug.Log(state.ToString());

        switch (state)
        {
            case RewardedState.Loading:
                break;
            case RewardedState.Opened:
                AudioManager.instance.musicSource.volume = 0;
                break;
            case RewardedState.Closed:
                AudioManager.instance.musicSource.volume = 1;
                break;
            case RewardedState.Failed:
                AudioManager.instance.musicSource.volume = 1;
                break;
            case RewardedState.Rewarded:
                petData.ChangeMoney(1000);
                break;
        }
    }

    public void ShowAd()
    {
        Bridge.advertisement.ShowRewarded();
    }
}
