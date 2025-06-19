using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text _countText;
    [SerializeField] private Text _incomeText;
    [SerializeField] private StoreUpgrade[] _storeUpgrade;
    [SerializeField] private Button _clickButton;
    [SerializeField] private int _updatesPerSecond = 5;

    [SerializeField] public float _count = 0;
    [SerializeField] private float _nextTimeCheck = 1f;
    [SerializeField] private float _lastIncomeValue = 0f;

    private void Awake()
    {
        _clickButton.onClick.AddListener(ClickAction);
    }

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        if (_nextTimeCheck < Time.timeSinceLevelLoad)
        {
            IdleCalculate();
            _nextTimeCheck = Time.timeSinceLevelLoad + (1f / _updatesPerSecond);
        }
    }

    private void IdleCalculate()
    {
        float sum = 0;

        foreach (var storeUpgrade in _storeUpgrade)
        {
            sum += storeUpgrade.CalculateIncomePerSecond();
            storeUpgrade.UpdateUI();
        }

        _lastIncomeValue = sum;
        _count += sum / _updatesPerSecond;
        UpdateUI();
    }

    public void ClickAction()
    {
        _count++;
        _count += _lastIncomeValue * 0.02f;
        UpdateUI();
    }

    public bool PurchaseAction(int cost)
    {
        if (_count >= cost)
        {
            _count -= cost;
            UpdateUI();
            return true;
        }

        return false;
    }

    private void UpdateUI()
    {
        _countText.text = Mathf.RoundToInt(_count).ToString();
        _incomeText.text = _lastIncomeValue.ToString() + " /s";
    }
}
