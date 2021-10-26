using TMPro;
using UnityEngine;

public class AutoPlayButton : Button
{
    private SymbolController _symbolController;
    private TextMeshProUGUI _textMeshPro;
    private PlayButton _playButton;

    private void Start()
    {
        Initialize();
        
        _symbolController = GameObject.FindGameObjectWithTag("Board").GetComponent<SymbolController>();
        _textMeshPro = GameObject.FindGameObjectWithTag("AutoPlayButtonText").GetComponent<TextMeshProUGUI>();
        _playButton = GameObject.FindGameObjectWithTag("PlayButton").GetComponent<PlayButton>();
    }

    private void OnMouseDown()
    {
        switch (PressedStatus)
        {
            case "Unpressed":
                PressAutoPlayButton();
                _symbolController.SetAutoPlay(true);
                break;
            
            case "Pressed":
                UnpressAutoPlayButton();
                _symbolController.SetAutoPlay(false);
                break;
        }
    }

    private void PressAutoPlayButton()
    {
        PressButton();
        _textMeshPro.text = "STOP\nAUTO";
    }
    
    private void UnpressAutoPlayButton()
    {
        UnpressButton();
        _textMeshPro.text = "AUTO\nSPIN";
    }
}