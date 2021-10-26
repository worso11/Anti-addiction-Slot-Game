using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SymbolController : MonoBehaviour
{
    [SerializeField]
    public bool rigged;
    public int[] row1;
    public int[] row2;
    public int[] row3;
    public Sprite[] coins;

    private WinningController _winningController;
    private PlayButton _playButton;
    private int[][] _symbols;
    private bool[,] _symbolsMovingArray = new bool[5,4];
    private bool _symbolsMoving;
    private bool _gameReady;
    private int[] _cols;
    private float _shift;
    private int _spinTime;
    private bool _autoPlay;
    private float[] _extraSpinTime = new float[5];

    private void Start()
    {
        _winningController = GetComponent<WinningController>();
        _playButton = GameObject.FindGameObjectWithTag("PlayButton").GetComponent<PlayButton>();

        _cols = new[] {-1, -1, -1, -1, -1};
    }

    private void Update()
    {
        if (_symbolsMoving)
        {
            if (IsSpin() && _gameReady)
            {
                _gameReady = false;
            }
            
            if (IsSpinOver())
            {
                _winningController.SpawnWinningStars();
                _playButton.UnpressPlayButton();
                _symbolsMoving = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || _autoPlay)
            {
                _playButton.PressPlayButton();
                StartGame();
            }
        }
    }

    private void GenerateSymbols()
    {
        if (!rigged)
        {
            GenerateSymbolsRow(row1 = new int[5]);
            GenerateSymbolsRow(row2 = new int[5]);
            GenerateSymbolsRow(row3 = new int[5]);
        }

        _symbols = new [] {row1, row2, row3};
        _shift = Random.Range(-0.4f, 0.4f);

        _winningController.CheckForWin(_symbols);
    }

    private void GenerateSpinningTime()
    {
        _spinTime = _autoPlay ? 0 : Random.Range(2, 7);
        
        for (int i = 0; i < 5; i++)
        {
            _extraSpinTime[i] = Random.Range(0f, 2f);   
        }
    }

    private void GenerateSymbolsRow(IList<int> row)
    {
        for (var i = 0; i < 5; i++)
        {
            row[i] = GenerateSymbol();
        }
    }

    private int GenerateSymbol()
    {
        var symbol = Random.Range(0, (int) (3.75*coins.Length));

        if (symbol < 20)
        {
            return symbol % 4;
        }
        if (symbol < 29)
        {
            return 4 + (symbol - 20) % 3;
        }

        return coins.Length - 1;
    }
    
    private bool IsSpinOver()
    {
        return _symbolsMovingArray.Cast<bool>().All(status => !status);
    }
    
    private bool IsSpin()
    {
        return _symbolsMovingArray.Cast<bool>().All(status => status);
    }

    public void StartGame()
    {
        WinningController.DestroyStars();
        GenerateSymbols();
        GenerateSpinningTime();

        _gameReady = true;
        _symbolsMoving = true;
    }
    
    public Sprite SetRandomSprite()
    {
        return coins[Random.Range(0, coins.Length)];
    }
    
    public Sprite SetSprite(int row)
    {
        _cols[row] = _cols[row] == 3 ? 0 : ++_cols[row];
        return coins[_symbols[_cols[row]%3][row]];
    }

    public float SetYPosition(int row)
    {
        if (_shift < 0f)
        {
            return 2 * _cols[row] - 2 + _shift;
        }

        return 2 * _cols[row] - 2;
    }

    public int GetSpinTime()
    {
        return _spinTime;
    }

    public float GetExtraSpinTime(int index)
    {
        return _extraSpinTime[index];
    }

    public bool GetGameStatus()
    {
        return _gameReady;
    }
    
    public void SetSymbolAsMoving(int row, int col)
    {
        _symbolsMovingArray[row, col] = true;
    }
    
    public void SetSymbolAsStopped(int row, int col)
    {
        _symbolsMovingArray[row, col] = false;
    }

    public void SetAutoPlay(bool autoPlay)
    {
        _autoPlay = autoPlay;
    }
}
