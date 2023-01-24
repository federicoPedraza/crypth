using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class EditorUIController : MonoBehaviour
{
    public Toggle toggleBound;
    public Toggle togglePlayer;

    public EditorMeshesCombiner combiner;
    public GameObject confirmPanel;
    public Slider confirmSlider;
    
    public MeshRenderer boundsRenderer;
    public MeshRenderer playerRenderer;

    public TextMeshProUGUI serverStatusText;

    private void Update()
    {
        boundsRenderer.enabled = toggleBound.isOn;
        playerRenderer.enabled = togglePlayer.isOn;

        confirmPanel.SetActive(combiner.IsConfirming);
        confirmSlider.value = combiner.confirmDelay;
        confirmSlider.maxValue = combiner.confirmTotalDelay;
        
    }
}
