using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Breakable))]
public class HealthBar : MonoBehaviour
{
    private Breakable breakable;
    private Canvas canvas;
    private Slider slider;

    private void Awake()
    {
        breakable = GetComponent<Breakable>();
        canvas = GetComponentInChildren<Canvas>();
        slider = GetComponentInChildren<Slider>();
        breakable.OnDamageTaken += UpdateDisplay;
        if (canvas) canvas.enabled = false;
    }

    private void UpdateDisplay()
    {
        if (!canvas) return;
        canvas.enabled = true;
        if (slider == null) return;
        slider.value = breakable.Health / breakable.MaxHealth;
    }
}
