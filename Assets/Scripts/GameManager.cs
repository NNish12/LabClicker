using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField] private TMP_Text countText;
    [SerializeField] private TMP_Text incomeText;
    [SerializeField] private ManagerStoreUpgrade[] stores;
    [SerializeField] private int updatesPerSecond = 5;
    [HideInInspector] public float count = 0;
    private float nextTimeCheck = 1;
    private float lastIncomeValue = 0;


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
        UpdateUI();
    }

    private void UpdateUI()
    {
        countText.text = Mathf.RoundToInt(count).ToString();
        incomeText.text = lastIncomeValue.ToString();

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