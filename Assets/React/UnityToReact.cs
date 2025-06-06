using System.Runtime.InteropServices;
using UnityEngine;

public static class UnityToReact
{
    [DllImport("__Internal")]
    private static extern void OnScoreUpdate(int score);

    public static void UpdateScore(int score)
    {
        Debug.Log("update score");
#if UNITY_WEBGL && ! UNITY_EDITOR
        OnScoreUpdate(score);
#endif
    }
}