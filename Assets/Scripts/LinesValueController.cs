using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LinesValueController : MonoBehaviour, IValueController
{
    private int _linesNumber;
    private WinningController _winningController;
    private TextMeshProUGUI _textMeshPro;
    void Start()
    {
        _winningController = GameObject.FindGameObjectWithTag("Board").GetComponent<WinningController>();
        _linesNumber = _winningController.GetLinesNumber();
        _textMeshPro = GameObject.FindGameObjectWithTag("LinesValue").GetComponent<TextMeshProUGUI>();
        
        _textMeshPro.SetText(_linesNumber.ToString("F0"));
    }

    public void ChangeValue(bool isIncreasing)
    {
        if (isIncreasing && _linesNumber == 5 || !isIncreasing && _linesNumber == 1) return;

        _linesNumber = isIncreasing ? _linesNumber + 2 : _linesNumber - 2;
        _winningController.UpdateLinesNumber(_linesNumber);
        _textMeshPro.SetText(_linesNumber.ToString("F0"));
    }
}
