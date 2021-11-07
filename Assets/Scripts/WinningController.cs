using System;
using System.Collections.Generic;
using UnityEngine;

public class WinningController : MonoBehaviour
{
    public ParticleSystem stars;

    private FinancialController _financialController;
    private int[,] _winningCoins;
    private int[,] _winningLines;
    private int _linesNumber = 1;

    private void Start()
    {
        _financialController = GetComponent<FinancialController>();
        _winningCoins = new int[3, 5];
        _winningLines = new int[5, 2];
    }
    
    private void CheckStraightLine(IReadOnlyList<int[]> coins, int line)
    {
        for (var j = 1; j < coins[0].Length - 1; j++)
        {
            if (coins[line][j] != coins[line][j - 1] || coins[line][j] != coins[line][j + 1]) continue;
            for (var k = j - 1; k < j + 2; k++)
            {
                _winningCoins[line,k] = 1;
            }

            var index = line < 1 ? 2 : (line - 1) % 3;
            _winningLines[index, 0] += 1;
            _winningLines[index, 1] = coins[line][j];
        }
    }

    private void CheckCurvyLine(IReadOnlyList<int[]> coins, int line)
    {
        var lines = line == 3 ? new[] {2, 1, 0, 1, 2} : new[] {0, 1, 2, 1, 0};

        for (var j = 1; j < coins[0].Length - 1; j++)
        {
            if (coins[lines[j]][j] != coins[lines[j - 1]][j - 1] || coins[lines[j]][j] != coins[lines[j + 1]][j + 1]) continue;
            for (var k = j - 1; k < j + 2; k++)
            {
                _winningCoins[lines[k],k] = 1;
            }
            
            _winningLines[line, 0] += 1;
            _winningLines[line, 1] = coins[lines[j]][j];
        }
    }

    private void PrintLines()
    {
        for (var i = 0; i < _winningLines.GetLength(0); i++)
        {
            if (_winningLines[i,0] > 0)
            {
                Debug.Log($@"Linia {i+1} wygrała {_winningLines[i,0]+2} z symbolem {_winningLines[i,1]}");
            }
        }
    }

    public void CheckForWin(int[][] coins)
    {
        _winningCoins = new int[3, 5];
        _winningLines = new int[5, 2];
        
        CheckStraightLine(coins, 1);
        
        if (_linesNumber > 2)
        {
            CheckStraightLine(coins, 0);
            CheckStraightLine(coins, 2);
        }

        if (_linesNumber > 4)
        {
            CheckCurvyLine(coins, 3);
            CheckCurvyLine(coins, 4);
        }

        //PrintLines();
        _financialController.GetWin(_winningLines, _linesNumber);
    }

    public int GetLinesNumber()
    {
        return _linesNumber;
    }
    
    public void UpdateLinesNumber(int linesNumber)
    {
        _linesNumber = linesNumber;
    }

    public void SpawnWinningStars()
    {
        for (var i = 0; i < _winningCoins.GetLength(0); i++)
        {
            for (var j = 0; j < _winningCoins.GetLength(1); j++)
            {
                if (_winningCoins[i,j] == 1)
                {
                    Instantiate(stars, new Vector3(j*2 - 4, i*2 - 2,0), Quaternion.identity);
                }
            } 
        }
        
        Array.Clear(_winningCoins, 0, _winningCoins.Length);
    }

    public static void DestroyStars()
    {
        var starsToDestroy = GameObject.FindGameObjectsWithTag("Stars");

        foreach (var star in starsToDestroy)
        {
            Destroy(star);
        }
    }
}