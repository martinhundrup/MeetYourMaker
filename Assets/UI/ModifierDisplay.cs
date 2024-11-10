using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModifierDisplay : MonoBehaviour
{
    [SerializeField] private TypewriterEffect nameText;
    [SerializeField] private TypewriterEffect descriptionText;
    [SerializeField] private TypewriterEffect costText;
    private Modifier mod;

    private void Awake()
    {
        Initialize(ModifierGenerator.CreateModifier());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Initialize(ModifierGenerator.CreateModifier());
        }
    }

    public void Initialize(Modifier _mod)
    {
        mod = _mod;
        StartCoroutine(nameText.SetText(mod.name));
        StartCoroutine(descriptionText.SetText(mod.description));
        StartCoroutine(costText.SetText($"Cost: {mod.cost}"));
    }
}
