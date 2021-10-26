using System;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Color unpressedColor;

    protected SymbolController SymbolController;
    private SpriteRenderer _spriteRendererBottom;
    private SpriteRenderer _spriteRendererTop;
    private Color _pressedColor;
    private Color _pressedOuterColor;
    protected string PressedStatus;

    internal void Initialize()
    {
        SymbolController = GameObject.FindGameObjectWithTag("Board").GetComponent<SymbolController>();
        
        _pressedColor = unpressedColor;
        _pressedOuterColor = unpressedColor;
        _pressedColor.r -= 0.2f;
        _pressedOuterColor.r -= 0.4f;
        PressedStatus = "Unpressed";

        _spriteRendererBottom = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        _spriteRendererBottom.color = _pressedColor;
        
        _spriteRendererTop = gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
        _spriteRendererTop.color = unpressedColor;
    }
    

    public void PressButton()
    {
        _spriteRendererTop.color = _pressedColor;
        _spriteRendererBottom.color = _pressedOuterColor;
        PressedStatus = "Pressed";
    }

    public void UnpressButton()
    {
        _spriteRendererTop.color = unpressedColor;
        _spriteRendererBottom.color = _pressedColor;
        PressedStatus = "Unpressed";
    }
}
