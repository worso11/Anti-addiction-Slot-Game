using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DescriptionButton : MonoBehaviour
{
    public string limitName;
    public string buttonTextTag;
    public bool destroyOnClick;
    
    private MessagePanel _messagePanel;
    private TextMeshProUGUI _textMeshPro;
    
    private void Start()
    {
        _messagePanel = GameObject.FindGameObjectWithTag("MessagePanel").GetComponent<MessagePanel>();
        _textMeshPro = buttonTextTag == string.Empty
            ? null
            : GameObject.FindGameObjectWithTag(buttonTextTag).GetComponent<TextMeshProUGUI>();
    }

    private void OnMouseDown()
    {
        _messagePanel.SetMessage(limitName);

        if (destroyOnClick)
        {
            _textMeshPro.enabled = false;
            gameObject.SetActive(false);
        }
    }
}
