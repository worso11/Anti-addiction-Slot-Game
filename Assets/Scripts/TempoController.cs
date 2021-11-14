using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TempoController : MonoBehaviour
{
    private FinancialController _financialController;
    private MessagePanel _messagePanel;
    private TextMeshProUGUI _textMeshPro;
    private float _moneyBalance;
    private DateTime _startRoundTime;
    private int _clicksNumber;
    private List<(TimeSpan RoundTime,int ClicksNumber,bool IsWon)> _tempoList = new List<(TimeSpan RoundTime, int ClicksNumber, bool isWon)>();
    private bool _isPlaying = true;

    private void Start()
    {
        _financialController = GameObject.FindGameObjectWithTag("Board").GetComponent<FinancialController>();
        _messagePanel = GameObject.FindGameObjectWithTag("MessagePanel").GetComponent<MessagePanel>();
        _moneyBalance = _financialController.ReturnWin();
        _textMeshPro = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        _messagePanel.SetTempoController();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.B))
        {
            _textMeshPro.enabled = !_textMeshPro.enabled;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            PrintTempoInfo();
        }

        if (_isPlaying && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)))
        {
            _clicksNumber++;
        }

        UpdateTempoControllerText();
    }
    
    private void UpdateTempoControllerText()
    {
        var timeDifference = DateTime.Now - _startRoundTime;


        _textMeshPro.text = "RoundTime(s):\n" +
                            $"{string.Format("{0:D2}:{1:D2}", timeDifference.Seconds, timeDifference.Milliseconds)}\n" +
                            "RoundClicks:\n" +
                            $"{_clicksNumber}\n" +
                            "ClicksPerMin:\n" +
                            $"{(timeDifference.Seconds > 0 ? _clicksNumber*60/timeDifference.Seconds : 99999999)}";
    }

    private void PrintTempoInfo()
    {
        Debug.Log(
            $"Average clicks when won: {_tempoList.Where(x => x.IsWon).Select(x => x.ClicksNumber).DefaultIfEmpty(0).Average()}");
        Debug.Log(
            $"Average clicks when not won: {_tempoList.Where(x => !x.IsWon).Select(x => x.ClicksNumber).DefaultIfEmpty(0).Average()}");
        Debug.Log(
            $"Average time when won: {_tempoList.Where(x => x.IsWon).Select(x => x.RoundTime.Milliseconds).DefaultIfEmpty(0).Average()}");
        Debug.Log(
            $"Average time when not won: {_tempoList.Where(x => !x.IsWon).Select(x => x.RoundTime.Milliseconds).DefaultIfEmpty(0).Average()}");
    }

    private bool CheckIfPlayedFast()
    {
        var lastFiveRoundsTime = _tempoList.Select(x => x.RoundTime).Reverse().Take(5).Reverse().ToList();
        
        if (lastFiveRoundsTime.Sum(x => x.Seconds) == 0)
        {
            if (lastFiveRoundsTime.Sum(x => x.Milliseconds) < 1500f)
            {
                _messagePanel.SetMessage("TooFast");
                return true;
            }
        }

        return false;
    }

    private bool CheckIfClickedFast()
    {
        var lastFiveClicksNumber = _tempoList.Select(x => x.ClicksNumber).Reverse().Take(5).Reverse().ToList();

        if (lastFiveClicksNumber.Sum() > 25)
        {
            _messagePanel.SetMessage("TooMuchClicks");
            return true;
        }

        return false;
    }

    public void SetSpinAsFinished()
    {
        _isPlaying = false;
        _startRoundTime = DateTime.Now;
    }
    
    public void SetNewRound(bool spaceTrigger, bool autoPlay)
    {
        if (!_isPlaying && !autoPlay)
        {
            _tempoList.Add((DateTime.Now - _startRoundTime, _clicksNumber, _moneyBalance < _financialController.ReturnWin()));   
        }

        if (_tempoList.Count > 4)
        {
            if (!CheckIfPlayedFast())
            {
                CheckIfClickedFast();
            }
        }

        _moneyBalance = _financialController.ReturnWin();
        _clicksNumber = spaceTrigger ? 1 : 0;
        _startRoundTime = DateTime.Now;
        _isPlaying = true;
    }

    public void SetIsPlaying(bool isPlaying)
    {
        _isPlaying = isPlaying;
    }
}
