using TMPro;
using UnityEngine;

public class LimitProvider : MonoBehaviour
{
    private TextMeshProUGUI _textMeshPro;
    private SpriteRenderer _background;

    private void Awake()
    {
        var parent = transform.parent;
        _textMeshPro = parent.GetChild(1).GetComponent<TextMeshProUGUI>();
        _background = parent.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        _background.enabled = true;
        _textMeshPro.fontSize += 10;
    }

    private void OnMouseExit()
    {
        _background.enabled = false;
        _textMeshPro.fontSize -= 10;
    }
}
