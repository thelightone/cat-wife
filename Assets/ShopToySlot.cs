using TMPro;
using UnityEngine;

public class ShopToySlot : MonoBehaviour
{
    public int toyId;
    public bool bought;
    public int price;

    private void Start()
    {
        PlayerPrefs.SetInt("toy0", 1);

        transform.GetComponentInChildren<TMP_Text>().text = price.ToString();

        if(PlayerPrefs.GetInt("toy"+toyId,0)==1)
        {
            Unlock();
        }
    }

    public void Highlight()
    {
        transform.GetChild(1).gameObject.SetActive(true);
    }
    public void UnHighlight()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void Unlock()
    {
        transform.GetChild(2).gameObject.SetActive(false);
        bought = true;
    }

    public void Buy()
    {
        PlayerPrefs.SetInt("toy" + toyId, 1);
    }

}
