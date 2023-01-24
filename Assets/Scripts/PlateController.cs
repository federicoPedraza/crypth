using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlateController : MonoBehaviour
{
    public string title = "Button";
    public string[] invalidTags;
    public bool applyZoom = true;
    public bool requiresConfirmation;
    public UnityEvent onPress;
    public UnityEvent onExit;
    
    private bool _pressed;
    private GameObject _trigger;
    private ColorLerper _colorLerper;

    private void Start()
    {
        if (TryGetComponent<ColorLerper>(out ColorLerper colorLerper))
            _colorLerper = colorLerper;
    }

    private void Update()
    {
        if (_pressed)
            HandlePress();
    }

    private void HandlePress()
    {
        if (applyZoom)
            CameraController.Instance.Zoom();
        
        if (_colorLerper)
            _colorLerper.Lerp();

        onPress.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (invalidTags.Contains(other.tag)) return;

        _pressed = true;
        _trigger = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != _trigger) return;
        
        onExit.Invoke();
        
        if (applyZoom)
            CameraController.Instance.Zoom(false);
        _pressed = false;
        _trigger = null;
    }
}
