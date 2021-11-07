using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number : MonoBehaviour
{
    private GameObject _parent;
    private Color _color;
    private Color _hoverColor;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _parent = transform.parent.gameObject;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _color = _spriteRenderer.color;
        _hoverColor = ChangeColorAlpha(_color, 0.8f);
    }

    private void SetLineState(Color color, bool enable)
    {
        for (var i = 0; i < _parent.transform.childCount; i++)
        {
            var child = _parent.transform.GetChild(i);
            
            if (child.CompareTag("Line"))
            {
                child.gameObject.SetActive(enable);
            }
            else
            {
                child.GetComponent<SpriteRenderer>().color = color;
            }
        }
    }
    
    private static Color ChangeColorAlpha(Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }
    
    
    public void OnMouseOver()
    {
        SetLineState(_hoverColor, true);
    }

    public void OnMouseExit()
    {
        SetLineState(_color, false);
    }
}
