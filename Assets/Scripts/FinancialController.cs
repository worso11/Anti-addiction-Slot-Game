using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinancialController : MonoBehaviour
{
    private int[] _symbolsRarity;
    private float[,] _winValues;
    private float _bet = 1f;
    private float _winSum = 1000f;
    
    private void Start()
    {
        _symbolsRarity = new[] {0, 0, 0, 0, 1, 1, 1, 2};
        _winValues = new [,]
        {
            {
                4f,
                30f,
                325f
            },
            {
                15f,
                250f,
                4250f
            },
            {
                400f,
                17000f,
                987000f
            },
        };
    }

    public void GetWin(int[,] lines, int linesNumber)
    {
        _winSum -= _bet;
        
        for (var i = 0; i < lines.GetLength(0); i++)
        {
            if (lines[i, 0] > 0)
            {
                _winSum += _winValues[_symbolsRarity[lines[i, 1]], lines[i, 0] - 1] / linesNumber * _bet;
            }
        }
        
        //Debug.Log($"Wygrana: {_winSum}");
    }

    public float ReturnWin()
    {
        return _winSum;
    }

    public float GetBet()
    {
        return _bet;
    }

    public void UpdateBet(float bet)
    {
        _bet = bet;
    }
}
