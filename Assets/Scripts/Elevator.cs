using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlateController))]
public class Elevator : MonoBehaviour
{
    public float elevatedHeight;

    private PlateController _plateController;
    private float _startingHeight;
    private float _dHeight;

    private void Awake()
    {
        _plateController = GetComponent<PlateController>();
        _startingHeight = transform.position.y;

        _plateController.onPress.AddListener(() => transform.position = Vector3.Lerp(transform.position,
            new Vector3(transform.position.x, elevatedHeight, transform.position.z), Time.deltaTime * 5f));
        _plateController.onExit.AddListener(() => transform.position = Vector3.Lerp(transform.position,
            new Vector3(transform.position.x, _startingHeight, transform.position.z), Time.deltaTime * 5f));
    }
}