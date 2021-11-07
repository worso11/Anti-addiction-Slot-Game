using UnityEngine;

public class ChangeValueButton : MonoBehaviour
{
    public GameObject valueObject;
    public bool _isIncreasing;

    private IValueController _value;
    private SymbolController _symbolController;
    void Start()
    {
        _value = valueObject.GetComponent<IValueController>();
        _symbolController = GameObject.FindGameObjectWithTag("Board").GetComponent<SymbolController>();
    }

    private void OnMouseDown()
    {
        if (_symbolController.GetSymbolsMoving()) return;
        _value.ChangeValue(_isIncreasing);
    }
}
