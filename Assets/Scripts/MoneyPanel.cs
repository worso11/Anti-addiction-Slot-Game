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
    private GameObject _ldwsDescription;
    private TextMeshProUGUI _ldwsDescriptionText;
    private float _moneyValue;
    private Color _defaultColor;
    private bool _beforeFirstLdw;

    private void Start()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        _financialController = GameObject.FindGameObjectWithTag("Board").GetComponent<FinancialController>();
        _ldwsDescription = GameObject.FindGameObjectWithTag("LDWsDescription").gameObject;
        _ldwsDescriptionText = GameObject.FindGameObjectWithTag("LDWsText").GetComponent<TextMeshProUGUI>();
        _moneyValue = _financialController.ReturnWin();
        _defaultColor = _textMeshPro.color;
        _beforeFirstLdw = true;
        
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
            timeElapsed += Time.fixedDeltaTime;

            yield return null;
        }

        _textMeshPro.SetText(moneyBalance.ToString("F2") + "$");
        _textMeshPro.color = _defaultColor;
        _moneyValue = moneyBalance;

        if (!(moneyDifference > 0f) || !_beforeFirstLdw || !(moneyDifference < _financialController.GetBet())) yield break;
        
        for (var i = 0; i < _ldwsDescription.transform.childCount; i++)
        {
            _ldwsDescription.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
        }

        _ldwsDescriptionText.enabled = true;
        _beforeFirstLdw = false;
    }
}
