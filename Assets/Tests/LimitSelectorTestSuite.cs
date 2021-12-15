using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class LimitSelectorTestSuite
{
    private LimitController _limitController;
    private GameObject _messagePanel;
    private GameObject _moneyDeposit;
    private GameObject _wageringLimit;

    [UnitySetUp]
    public IEnumerator UnitySetUp()
    {
        SceneManager.LoadScene("LimitSelector");

        yield return new WaitForSeconds(1f);
        
        _limitController = GameObject.FindGameObjectWithTag("LimitController").GetComponent<LimitController>();
        _messagePanel = GameObject.FindGameObjectWithTag("MessagePanel");
        _moneyDeposit = GameObject.Find("MoneyDeposit");
        _wageringLimit = GameObject.Find("WageringLimit");
        
        _limitController.MoneyDeposit = 100;
        _limitController.WageringLimit = 100;
        _limitController.TimeLimit = 15;
        _limitController.TimeAlert = 5;
    }
    
    [UnityTearDown]
    public IEnumerator UnityTearDown()
    {
        Object.Destroy(_limitController.gameObject);
        Object.Destroy(_messagePanel);
        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckMoneyDepositUpperLimit()
    {
        var moneyDepositLimitProvider = _moneyDeposit.transform.GetChild(0).GetComponent<InputLimitProvider>();
        var moneyDepositInput = _moneyDeposit.transform.GetChild(3).GetComponent<TMP_InputField>();

        moneyDepositInput.text = 1001.ToString();
        
        moneyDepositLimitProvider.EstablishLimit();

        Assert.IsFalse(_limitController.GetGameReady(), "Game is ready");
    
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckMoneyDepositLowerLimit()
    {
        var moneyDepositLimitProvider = _moneyDeposit.transform.GetChild(0).GetComponent<InputLimitProvider>();
        var moneyDepositInput = _moneyDeposit.transform.GetChild(3).GetComponent<TMP_InputField>();

        moneyDepositInput.text = 0.ToString();
        
        moneyDepositLimitProvider.EstablishLimit();

        Assert.IsFalse(_limitController.GetGameReady(), "Game is ready");
    
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckWageringLimitUpperLimit()
    {
        var wageringLimitLimitProvider = _wageringLimit.transform.GetChild(0).GetComponent<InputLimitProvider>();
        var wageringLimitInput = _wageringLimit.transform.GetChild(3).GetComponent<TMP_InputField>();

        wageringLimitInput.text = 1001.ToString();
        
        wageringLimitLimitProvider.EstablishLimit();

        Assert.IsFalse(_limitController.GetGameReady(), "Game is ready");
    
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckWageringLimitLowerLimit()
    {
        var wageringLimitLimitProvider = _wageringLimit.transform.GetChild(0).GetComponent<InputLimitProvider>();
        var wageringLimitInput = _wageringLimit.transform.GetChild(3).GetComponent<TMP_InputField>();

        wageringLimitInput.text = 0.ToString();
        
        wageringLimitLimitProvider.EstablishLimit();

        Assert.IsFalse(_limitController.GetGameReady(), "Game is ready");
    
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckLimitControllerWithCorrectValues()
    {
        var moneyDepositLimitProvider = _moneyDeposit.transform.GetChild(0).GetComponent<InputLimitProvider>();
        var wageringLimitLimitProvider = _wageringLimit.transform.GetChild(0).GetComponent<InputLimitProvider>();
        var moneyDepositInput = _moneyDeposit.transform.GetChild(3).GetComponent<TMP_InputField>();
        var wageringLimitInput = _wageringLimit.transform.GetChild(3).GetComponent<TMP_InputField>();

        moneyDepositInput.text = 100.ToString();
        wageringLimitInput.text = 100.ToString();

        moneyDepositLimitProvider.EstablishLimit();
        wageringLimitLimitProvider.EstablishLimit();
        
        Assert.IsTrue(_limitController.GetGameReady(), "Game is not ready");
    
        yield return null;
    }
}
