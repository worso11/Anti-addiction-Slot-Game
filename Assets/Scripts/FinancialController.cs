using UnityEngine;

public class FinancialController : MonoBehaviour
{
    private LimitController _limitController;
    private int[] _symbolsRarity;
    private float[,] _winValues;
    private float _bet = 1f;
    private float _winSum = 1000f;

    private void Awake()
    {
        _limitController = GameObject.FindGameObjectWithTag("LimitController").GetComponent<LimitController>();
        _winSum = _limitController.MoneyDeposit;
    }

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

    public bool PutBet()
    {
        if (_limitController.CheckMoney(_winSum, _bet) && _limitController.CheckWageringLimit(_bet))
        {
            _limitController.WageringLimit -= _bet;
            _winSum -= _bet;

            return true;
        }

        return false;
    }
    
    public void GetWin(int[,] lines, int linesNumber)
    {
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

    public void IncreaseWinSum(float increase)
    {
        _winSum += increase;
    }
}
