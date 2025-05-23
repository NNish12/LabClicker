using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [SerializeField] private TMP_Text countText;
    [SerializeField] private TMP_Text incomeText;
    [SerializeField] private ManagerStoreUpgrade[] stores;
    [SerializeField] private int updatesPerSecond = 5;

    [HideInInspector] public float count = 0;
    private float nextTimeCheck = 1;
    private float lastIncomeValue = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        if (nextTimeCheck < Time.timeSinceLevelLoad)
        {
            IdleUpdate();
            nextTimeCheck = Time.timeSinceLevelLoad + (1f / updatesPerSecond);
        }
    }

    private void IdleUpdate()
    {
        float sum = 0;
        foreach (var store in stores)
        {
            sum += store.CalculateDropsPerUpgrade();
            store.UpdateUI();
        }

        lastIncomeValue = sum;
        count += sum / updatesPerSecond;
        UpdateUI();
    }

    public void ClickAction()
    {
        count++;
        count += lastIncomeValue + 0.02f;
        UpdateUI();
    }

    private void UpdateUI()
    {
        countText.text = Mathf.RoundToInt(count).ToString();
        incomeText.text = lastIncomeValue.ToString("F2") + "/s";
    }

    public bool BuyAction(int price)
    {
        if (count >= price)
        {
            count -= price;
            UpdateUI();
            return true;
        }
        return false;
    }
}