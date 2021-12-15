using TMPro;
using UnityEngine;

public class AutoPlayButton : Button
{
    private SymbolController _symbolController;
    private TextMeshProUGUI _textMeshPro;

    private void Start()
    {
        _symbolController = GameObject.FindGameObjectWithTag("Board").GetComponent<SymbolController>();
        _textMeshPro = GameObject.FindGameObjectWithTag("AutoPlayButtonText").GetComponent<TextMeshProUGUI>();
    }

    public void OnMouseDown()
    {
        switch (PressedStatus)
        {
            case "Unpressed":
                PressAutoPlayButton();
                break;
            
            case "Pressed":
                UnpressAutoPlayButton();
                break;
        }
    }

    private void PressAutoPlayButton()
    {
        PressButton();
        _textMeshPro.text = "STOP\nAUTO";
        _symbolController.SetAutoPlay(true);
    }
    
    public void UnpressAutoPlayButton()
    {
        UnpressButton();
        _textMeshPro.text = "AUTO\nSPIN";
        _symbolController.SetAutoPlay(false);
    }
    
    public string GetPressedStatus()
    {
        return PressedStatus;
    }
}