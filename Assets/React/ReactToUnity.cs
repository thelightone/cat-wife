using TMPro;
using UnityEngine;

public class ReactToUnity : MonoBehaviour
{
    public TMP_Text text;

    public void SetScore(int id)
    {
        text.text = id.ToString();
    }
}
