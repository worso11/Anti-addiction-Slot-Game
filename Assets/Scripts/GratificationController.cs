using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GratificationController : MonoBehaviour
{
    private FinancialController _financialController;
    private LimitController _limitController;
    private MessagePanel _messagePanel;
    private MoneyPanel _moneyPanel;
    private float _initialDeposit;
    private List<string> _usedFlags;
    
    void Start()
    {
        _financialController = GameObject.FindGameObjectWithTag("Board").GetComponent<FinancialController>();
        _limitController = GameObject.FindGameObjectWithTag("LimitController").GetComponent<LimitController>();
        _messagePanel = GameObject.FindGameObjectWithTag("MessagePanel").GetComponent<MessagePanel>();
        _moneyPanel = GameObject.FindGameObjectWithTag("MoneyPanel").GetComponent<MoneyPanel>();
        _initialDeposit = _limitController.MoneyDeposit;
        _usedFlags = new List<string>();
    }

    public void GratificatePlayerWithCredit(string flag, float partOfDeposit)
    {
        if (CheckIfFlagUsed(flag)) return;
        
        var extraCredit = Mathf.Floor(_initialDeposit/5f) * 5f * partOfDeposit;
        _limitController.WageringLimit += extraCredit;

        _financialController.IncreaseWinSum(extraCredit);
        _messagePanel.SetMessage("GettingCredit", extraCredit);
        StartCoroutine(_moneyPanel.UpdateMoneyPanelText(_financialController.ReturnWin()));
    }
    
    public void GratificatePlayerWithTime(string flag, float bonusTime)
    {
        if (CheckIfFlagUsed(flag)) return;
        
        _limitController.IncreaseTimeLimit(bonusTime);
        _messagePanel.SetMessage("GettingTime", bonusTime);
    }

    private bool CheckIfFlagUsed(string flag)
    {
        if (_usedFlags.Contains(flag))
        {
            return true;
        }

        _usedFlags.Add(flag);
        return false;
    }
}
