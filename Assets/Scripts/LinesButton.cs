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

        foreach (var number in _numbers)
        {
            var numberInstance = number.GetComponent<Number>();
            
            if (numberInstance.lineNumber <= _activeNumbersCount)
            {
                numberInstance.OnMouseOver();
            }
        }
    }

    public void OnMouseExit()
    {
        foreach (var number in _numbers)
        {
            var numberInstance = number.GetComponent<Number>();
            
            if (numberInstance.lineNumber <= _activeNumbersCount)
            {
                numberInstance.OnMouseExit();
            }
        }
    }
}
