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

    public void GratificatePlayerWithCredits(string flag, float partOfDeposit)
    {
        if (CheckIfFlagUsed(flag)) return;

        float extraCredit;
        
        if (_initialDeposit <= 5f)
        {
            extraCredit = _initialDeposit;
        }
        else if (_initialDeposit <= 5f/partOfDeposit)
        {
            extraCredit = 5f;
        }
        else
        {
            extraCredit  = Mathf.Floor(_initialDeposit/5f) * 5f * partOfDeposit;
        }
        
        _limitController.WageringLimit += extraCredit;

        _financialController.IncreaseWinSum(extraCredit);

        if (flag == "money" || flag == "WageringLimit")
        {
            _messagePanel.SetMessage("GettingCredit", extraCredit);
        }
        else
        {
            _messagePanel.SetMessage("RewardForSlowerPlay", extraCredit);
        }
        
        StartCoroutine(_moneyPanel.UpdateMoneyPanelText(_financialController.ReturnWin()));
    }
    
    public void GratificatePlayerWithTime(string flag, float bonusTime)
    {
        if (CheckIfFlagUsed(flag)) return;

        if (flag == "TimeLimit")
        {
            bonusTime += _limitController.TimeLimit * 60f * 0.1f + 5f;
        }
        
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
