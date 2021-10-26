using TMPro;
using UnityEngine;

public class PlayButton : Button
{
    private TextMeshProUGUI _textMeshPro;

    private void Start()
    {
        Initialize();
        
        _textMeshPro = GameObject.FindGameObjectWithTag("PlayButtonText").GetComponent<TextMeshProUGUI>();
    }

    private void OnMouseDown()
    {
        switch (PressedStatus)
        {
            case "Unpressed":
                PressPlayButton();
                SymbolController.StartGame();
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

    public void SetText(string text)
    {
        _textMeshPro.text = text;
    }
}
