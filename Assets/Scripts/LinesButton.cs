using UnityEngine;

public class LinesButton : MonoBehaviour
{
    private GameObject[] _numbers;
    private WinningController _winningController;
    private int _activeNumbersCount;
    
    private void Start()
    {
        _numbers = GameObject.FindGameObjectsWithTag("LineNumber");
        _winningController = GameObject.FindGameObjectWithTag("Board").GetComponent<WinningController>();
        _activeNumbersCount = _winningController.GetLinesNumber();
    }

    public void OnMouseOver()
    {
        _activeNumbersCount = _winningController.GetLinesNumber();
        
        for (var i = 0; i < _activeNumbersCount; i++)
        {
            _numbers[i].GetComponent<Number>().OnMouseOver();
        }
    }

    public void OnMouseExit()
    {
        for (var i = 0; i < _activeNumbersCount; i++)
        {
            _numbers[i].GetComponent<Number>().OnMouseExit();
        }
    }
}
