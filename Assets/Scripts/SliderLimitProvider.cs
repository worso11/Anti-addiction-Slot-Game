using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderLimitProvider : LimitProvider
{
    public bool leftLimit;
    
    private LimitController _limitController;
    private TextMeshProUGUI _valueTextMeshPro;
    private int _limitValue;
    private int _limitMultiplier = 5;

    private void Start()
    {
        var parent = transform.parent;
        
        _limitController = GameObject.FindGameObjectWithTag("LimitController").GetComponent<LimitController>();
        _valueTextMeshPro = parent.GetChild(3).GetChild(2).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        _limitMultiplier = int.Parse(_valueTextMeshPro.text);
    }

    public void EstablishLimit()
    {
        _limitValue = int.Parse(_valueTextMeshPro.text);
        
        if (leftLimit)
        {
            _limitController.TimeLimit = _limitValue;
        }
        else
        {
            _limitController.TimeAlert = _limitValue;
        }
    }

    public void SetSliderText(float value)
    {
        _valueTextMeshPro.text = ((int)value * _limitMultiplier).ToString("F0");
    }
}
