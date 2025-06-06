using UnityEngine;

public class UnityToReactButton : MonoBehaviour
{
    public void SendScoreUpdate()
    {
        UnityToReact.UpdateScore(1);
    }
}
