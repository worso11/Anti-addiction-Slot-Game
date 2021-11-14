using TMPro;
using UnityEngine;

public class PlayButton : Button
{
    private SymbolController _symbolController;
    private TextMeshProUGUI _textMeshPro;

    private void Start()
    {
        _symbolController = GameObject.FindGameObjectWithTag("Board").GetComponent<SymbolController>();
        _textMeshPro = GameObject.FindGameObjectWithTag("PlayButtonText").GetComponent<TextMeshProUGUI>();
    }

    private void OnMouseDown()
    {
        switch (PressedStatus)
        {
            case "Unpressed":
                _symbolController.StartGame();
                break;
            
            case "Pressed":
                PressedStatus = "StopPressed";
                break;
        }
    }

    public void PressPlayButton()
    {
        PressButton();
        _textMeshPro.text = "STOP";
    }
    
    public void UnpressPlayButton()
    {
        UnpressButton();
        _textMeshPro.text = "SPIN";
    }
    
    public string GetPressedStatus()
    {
        return PressedStatus;
    }
}
