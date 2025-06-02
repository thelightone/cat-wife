using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text happiness;
    [SerializeField] private TMP_Text hunger;
    [SerializeField] private TMP_Text cleaness;
    [SerializeField] private TMP_Text energy;
    [SerializeField] private TMP_Text love;
    [SerializeField] private TMP_Text money;
    [SerializeField] private TMP_Text moneyShop;
    [SerializeField] private TMP_Text moneyToys;
    [SerializeField] private TMP_Text level;

    [SerializeField] private Image happinessImg;
    [SerializeField] private Image hungerImg;
    [SerializeField] private Image cleanessImg;
    [SerializeField] private Image loveImg;
    [SerializeField] private Image energyImg;
    [SerializeField] private Image xpImg;

    [SerializeField] private PetData _petData;

    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (_petData == null)
        {
            Debug.LogError("PetData reference is missing in UIManager!");
            return;
        }
        UpdateBalance();
    }

    public void UpdateBalance()
    {
        if (_petData == null) return;

        happiness.text = (_petData.CurrentHapiness/10).ToString()+"%";
        hunger.text = (_petData.CurrentFeed / 10).ToString() + "%";
        cleaness.text = (_petData.CurrentClean / 10).ToString() + "%";
        energy.text = (_petData.CurrentSleep / 10).ToString() + "%";
        love.text = (_petData.CurrentLove / 10).ToString() + "%";

        happinessImg.fillAmount = (float)_petData.CurrentHapiness/1000;
        hungerImg.fillAmount = (float)_petData.currentFeed / 1000;
        cleanessImg.fillAmount = (float)_petData.CurrentClean / 1000;
        energyImg.fillAmount = (float)_petData.CurrentSleep / 1000;
        loveImg.fillAmount = (float)_petData.CurrentLove / 1000;

        money.text = _petData.MoneyBalance.ToString();
        moneyShop.text = _petData.MoneyBalance.ToString();
        moneyToys.text = _petData.MoneyBalance.ToString();

        xpImg.fillAmount = (float)_petData.CurrentXp / 1000;
        level.text = _petData.CurrentLevel.ToString();
    }
}
