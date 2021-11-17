using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class LimitController : MonoBehaviour
{
    private GameObject _messagePanelObject;
    private MessagePanel _messagePanel;
    private TextMeshProUGUI _textMeshPro;
    private bool _gameReady = true;
    private DateTime _timeLimitTime;
    private DateTime _timeAlertTime;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _messagePanelObject = GameObject.FindGameObjectWithTag("MessagePanel");
        _messagePanel = _messagePanelObject.GetComponent<MessagePanel>();
        _textMeshPro = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name != "MainGame") return;
        
        if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.B))
        {
            _textMeshPro.enabled = !_textMeshPro.enabled;
        }

        UpdateLimitsText();
    }

    private void FixedUpdate()
    {
        if(SceneManager.GetActiveScene().name != "MainGame") return;

        UpdateLimitsText();
        
        if (_timeLimitTime < DateTime.Now)
        {
            Debug.Log("Przekroczono limit czasowy");
            _messagePanel.SetMessage(nameof(TimeLimit), TimeLimit);
        }
        
        if (_timeAlertTime < DateTime.Now)
        {
            Debug.Log("Alert czasowy");
            _messagePanel.SetMessage(nameof(TimeAlert), TimeAlert);
            _timeAlertTime = DateTime.Now.AddMinutes(TimeAlert);
        }
    }

    private void UpdateLimitsText()
    {
        var timeLeft = _timeLimitTime - DateTime.Now;
        var alertTimeLeft = _timeAlertTime - DateTime.Now;
        
        _textMeshPro.SetText("WageringLimit:\n" +
                             $"{WageringLimit}\n" +
                             "TimeLimit:\n" +
                             $"{string.Format("{0:D2}:{1:D2}", timeLeft.Minutes, timeLeft.Seconds)}\n" +
                             "TimeAlert:\n" +
                             $"{string.Format("{0:D2}:{1:D2}", alertTimeLeft.Minutes, alertTimeLeft.Seconds)}");
    }

    private void PassLimit(float limit, string limitName)
    {
        _messagePanel.SetMessage(limitName, limit);
        _messagePanelObject.transform.GetChild(0).gameObject.SetActive(true);
        
    }
    
    public float MoneyDeposit { get; set; }
    
    public float WageringLimit { get; set; }
    
    public float TimeLimit { get; set; }
    
    public float TimeAlert { get; set; }

    public bool CheckMoney(float money, float bet)
    {
        if (money < bet)
        {
            Debug.Log("Przekroczono limit kredytów");
            _messagePanel.SetMessage(nameof(money), money);
            return false;
        }

        return true;
    }
    
    public bool CheckWageringLimit(float bet)
    {
        if (WageringLimit < bet)
        {
            Debug.Log("Przekroczono limit zakładów");
            _messagePanel.SetMessage(nameof(WageringLimit), WageringLimit);
            return false;
        }

        return true;
    }
    
    public void OnButtonClick()
    {
        if (_gameReady)
        {
            Debug.Log(MoneyDeposit);
            Debug.Log(WageringLimit);
            Debug.Log(TimeLimit);
            Debug.Log(TimeAlert);

            _timeLimitTime = DateTime.Now.AddMinutes(TimeLimit);
            _timeAlertTime = DateTime.Now.AddMinutes(TimeAlert);
            SceneManager.LoadScene("MainGame");
            transform.GetChild(0).GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
        
        SetGameReady(true);
    }

    public void SetGameReady(bool gameReady)
    {
        _gameReady = gameReady;
    }

    public void IncreaseTimeLimit(float seconds)
    {
        _timeLimitTime = _timeLimitTime.AddSeconds(seconds);
    }
}
