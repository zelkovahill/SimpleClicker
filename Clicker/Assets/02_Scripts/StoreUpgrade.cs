using UnityEngine;
using UnityEngine.UI;

public class StoreUpgrade : MonoBehaviour
{
    [Header("[컴포넌트]")]
    public Text PriceText;
    public Text IncommInfoText;
    public Button UpgradeButton;
    public Image CharacterImage;
    public Text UpgradeNameText;

    public string UpgradeName;
    public int StartPrice = 15;
    public float UpdatePriceMultipliter;
    public float CookiesPerUpdate;

    private int _level = 0;

    [Header("[매니저]")]
    public GameManager GameManager;

    private void Awake()
    {
        UpgradeButton.onClick.AddListener(ClickAction);
    }

    private void Start()
    {
        UpdateUI();
    }

    public void ClickAction()
    {
        int price = CalculatePrice();

        bool purchaseSuccess = GameManager.PurchaseAction(price);

        if (purchaseSuccess)
        {
            _level++;
            UpdateUI();
        }


    }

    public void UpdateUI()
    {
        PriceText.text = CalculatePrice().ToString();
        IncommInfoText.text = _level.ToString() + " x " + CookiesPerUpdate + "/s";

        bool canAfford = GameManager._count >= CalculatePrice();
        UpgradeButton.interactable = canAfford;

        bool isPurchased = _level > 0;
        CharacterImage.color = isPurchased ? Color.white : Color.black;
        UpgradeNameText.text = isPurchased ? UpgradeName : "???";
    }

    private int CalculatePrice()
    {
        int price = Mathf.RoundToInt(StartPrice * Mathf.Pow(UpdatePriceMultipliter, _level));
        return price;
    }

    public float CalculateIncomePerSecond()
    {
        return CookiesPerUpdate * _level;
    }
}
