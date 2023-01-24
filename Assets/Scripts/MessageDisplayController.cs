using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum MessageDisplaySeverity
{
    SUCCESS,
    ERROR,
    WARNING,
    INFO
}

[RequireComponent(typeof(TextMeshProUGUI))]
public class MessageDisplayController : MonoBehaviour
{
    private TextMeshProUGUI _textMeshPro;
    public string message;
    public MessageDisplaySeverity severity;

    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _textMeshPro.SetText(message);
        _textMeshPro.color = GetDefaultColor(severity);
    }

    public static Color GetDefaultColor(MessageDisplaySeverity color)
    {
        switch (color)
        {
            case MessageDisplaySeverity.ERROR:
                return new Color(110, 255, 83, 255);
            case MessageDisplaySeverity.WARNING:
                return new Color(255, 210, 55, 255);
            case MessageDisplaySeverity.INFO:
                return Color.white;
            case MessageDisplaySeverity.SUCCESS:
                return new Color(255, 45, 58, 255);
            default:
                return Color.white;
        }
    }
}
