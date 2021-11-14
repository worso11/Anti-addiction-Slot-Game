using TMPro;
using UnityEngine;

public class InputLimitProvider : LimitProvider
{
    public bool leftLimit;
    
    private LimitController _limitController;
    private TMP_InputField _valueTextMeshPro;
    private GameObject _errorMessage;
    private float _limitValue;

    private void Start()
    {
        var parent = transform.parent;
        
        _limitController = GameObject.FindGameObjectWithTag("LimitController").GetComponent<LimitController>();
        _valueTextMeshPro = parent.GetChild(3).GetComponent<TMP_InputField>();
        _errorMessage = parent.GetChild(4).gameObject;
    }

    public void EstablishLimit()
    {
        _errorMessage.SetActive(false);

        if (!float.TryParse(_valueTextMeshPro.text, out _limitValue) || _limitValue < 1 || _limitValue > 1000)
        {
            _errorMessage.SetActive(true);
            _limitController.SetGameReady(false);
            return;
        }
        
        if (leftLimit)
        {
            _limitController.MoneyDeposit = _limitValue;
        }
        else
        {
            _limitController.WageringLimit = _limitValue;
        }
    }
}
