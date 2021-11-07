using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class MoneyPanel : MonoBehaviour
{
    private TextMeshProUGUI _textMeshPro;
    private FinancialController _financialController;
    private float _moneyValue;
    private Color _defaultColor;

    private void Start()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        _financialController = GameObject.FindGameObjectWithTag("Board").GetComponent<FinancialController>();
        _moneyValue = _financialController.ReturnWin();
        _defaultColor = _textMeshPro.color;
        
        _textMeshPro.SetText(_financialController.ReturnWin().ToString("F2") + "$");
    }

    public IEnumerator UpdateMoneyPanelText(float moneyBalance)
    {
        float timeElapsed = 0;
        float moneyDifference = _moneyValue - moneyBalance;
        float totalTime = Mathf.Floor(Mathf.Log10(Mathf.Abs(moneyDifference)) + 1);
        _textMeshPro.color = moneyDifference > 0 ? Color.red : Color.green;

        while (timeElapsed < (totalTime == 0 ? 1 : totalTime))
        {
            _textMeshPro.SetText(Mathf.Lerp(_moneyValue, moneyBalance, timeElapsed / totalTime).ToString("F2") + "$");
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        _textMeshPro.SetText(moneyBalance.ToString("F2") + "$");
        _textMeshPro.color = _defaultColor;
        _moneyValue = moneyBalance;
    }
}
