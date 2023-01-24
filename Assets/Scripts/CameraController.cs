using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    public bool followTarget = true;
    public Transform target;
    public Vector3 offset;
    public float speed;
    public float zoomSpeed;
    public float[] zoomValues = new float[2];
    
    private bool _zoomIn;
    private float _cZoom;
    private float _dZoom;
    private Camera _camera;
    private void Start()
    {
        _camera = GetComponent<Camera>();
        if (!target) return;
        
        GameObject player = GameObject.FindWithTag("Player");
        if (player)
            SetTarget(player.transform);

        _cZoom = _camera.orthographicSize;
    }

    private void FixedUpdate()
    {
        if (!target || !followTarget) return;

        FollowTarget();
        HandleZoom();
    }

    private void FollowTarget()
    {
        if (!target) return;

        Vector3 dPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, dPosition, speed * Time.deltaTime);
    }

    private void HandleZoom()
    {
        _dZoom = zoomValues[_zoomIn ? 1 : 0];
        _cZoom = Mathf.Lerp(_cZoom, _dZoom, zoomSpeed * Time.deltaTime);

        _camera.orthographicSize = _cZoom;
    }
    
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void Zoom(bool mode = true, float duration = -1)
    {
        _zoomIn = mode;
        if (duration > 0 && mode)
            StartCoroutine(ZoomOut(duration));
    }

    private IEnumerator ZoomOut (float delay)
    {
        _zoomIn = true;
        yield return new WaitForSeconds(delay);
        _zoomIn = false;
    }
}
