using TMPro;
using UnityEngine;

public class DescriptionButton : MonoBehaviour
{
    public string limitName;
    public string buttonTextTag;
    public bool destroyOnClick;
    public bool buttonAvailable;
    
    private MessagePanel _messagePanel;
    private TextMeshProUGUI _textMeshPro;
    private int _roundsToVanish;

    private void Start()
    {
        _messagePanel = GameObject.FindGameObjectWithTag("MessagePanel").GetComponent<MessagePanel>();
        _textMeshPro = buttonTextTag == string.Empty
            ? null
            : GameObject.FindGameObjectWithTag(buttonTextTag).GetComponent<TextMeshProUGUI>();
    }

    public void OnMouseDown()
    {
        if (!buttonAvailable) return;
        
        _messagePanel.SetMessage(limitName);

        if (destroyOnClick)
        {
            DisableChildsSprite();
        }
    }

    public void EnableChildsSprite(int roundNumber)
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
        }

        _textMeshPro.enabled = true;
        _roundsToVanish = roundNumber;
        buttonAvailable = true;
    }

    public void DisableChildsSprite()
    {
        buttonAvailable = false;
        _textMeshPro.enabled = false;
            
        for (var i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void CheckForDisable()
    {
        _roundsToVanish -= 1;

        if (_roundsToVanish <= 0)
        {
            DisableChildsSprite();
        }
    }
}
