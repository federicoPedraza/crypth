using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerper : MonoBehaviour
{
    public float speed = 1.1f;
    public Color startColor;
    public Color endColor;
    public bool onStartLerp;
    
    private float _startTime;
    private bool _lerping;
    private Renderer _renderer;
    

    private void Start()
    {
        _renderer = GetComponent<Renderer>();

        if (onStartLerp)
            Lerp();
    }

    private void Update()
    {
        Material material = _renderer.material;
        if (!_lerping)
        {
            _startTime = Time.time;
            material.color = Color.Lerp(material.color, startColor, Time.deltaTime * speed);
            return;
        }
        
        float t = (Time.time - _startTime) * speed;
        material.color = Color.Lerp(material.color, endColor, t);

        _lerping = material.color != endColor;
    }

    public void Lerp()
    {
        _startTime = 0;
        _lerping = true;
    }
}
