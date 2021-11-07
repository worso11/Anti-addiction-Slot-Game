using TMPro;
using UnityEngine;

public class BetValueController : MonoBehaviour, IValueController
{
    private float _betValue;
    private FinancialController _financialController;
    private TextMeshProUGUI _textMeshPro;
    void Start()
    {
        _financialController = GameObject.FindGameObjectWithTag("Board").GetComponent<FinancialController>();
        _betValue = _financialController.GetBet();
        _textMeshPro = GameObject.FindGameObjectWithTag("BetValue").GetComponent<TextMeshProUGUI>();
        
        _textMeshPro.SetText(_betValue.ToString("F0") + "$");
    }

    public void ChangeValue(bool isIncreasing)
    {
        if (isIncreasing && _betValue > 8.5f || !isIncreasing && _betValue < 1.5f) return;

        _betValue = isIncreasing ? _betValue + 1 : _betValue - 1;
        _financialController.UpdateBet(_betValue);
        _textMeshPro.SetText(_betValue.ToString("F0") + "$");
    }
}
