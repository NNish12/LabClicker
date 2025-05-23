using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManagerStoreUpgrade : MonoBehaviour
{
    [Header("References")]
    public GameManager gameManager;
    public Button button;
    public Image iconCharacter;
    public TMP_Text nameText;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private TMP_Text incomeText;
    [SerializeField] private Image frame;
    [SerializeField] private float alpha = 0.192f; // 49f / 255f;
    [SerializeField] private PitchSoundUpgrades audioManagerUpgrade;

    [Header("Game values")]
    [SerializeField] private string upgradeName;
    [SerializeField] private int StartPrice = 30;
    [SerializeField] private float PriceMultiplier = 1.5f;
    [SerializeField] private float dropsPerUpgrade = 0.1f;
    private int currentLevel = 0;
    private Color startColor;

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void SetCurrentLevel(int level)
    {
        currentLevel = level;
        UpdateUI();
    }

    private void Start()
    {
        startColor = frame.color;
        UpdateUI();
    }

    private int CalculatePrice() => Mathf.RoundToInt(StartPrice * Mathf.Pow(PriceMultiplier, currentLevel));
    public float CalculateDropsPerUpgrade() => dropsPerUpgrade * currentLevel;

    public void UpdateUI()
    {
        priceText.text = CalculatePrice().ToString();
        incomeText.text = $"{currentLevel} x {dropsPerUpgrade}";
        bool canBuy = gameManager.count >= CalculatePrice();

        startColor.a = canBuy ? 1f : alpha;
        frame.color = startColor;
        button.interactable = canBuy;

        bool isAvailable = currentLevel > 0;
        iconCharacter.color = isAvailable ? Color.white : Color.black;
        nameText.text = isAvailable ? upgradeName : "???";
    }

    public void BuyUpgrade()
    {
        int price = CalculatePrice();
        bool canBuy = gameManager.BuyAction(price);
        if (canBuy)
        {
            currentLevel++;
            UpdateUI();
            audioManagerUpgrade.PlayUpgradeSound();
        }
    }


}