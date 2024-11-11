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
    [SerializeField] private bool defaultMod;

    private void Awake()
    {
        Initialize(ModifierGenerator.CreateModifier());
        GameEvents.OnPlayerRespawn += Disable;
    }

    private void Disable()
    {
        GetComponent<Button>().interactable = false;
    }

    public void Chosen()
    {
        DataDictionary.PlayerStats.ApplyModifier(mod);
        GetComponent<Button>().interactable = false;
        DataDictionary.PlayerStats.EXP -= mod.cost;

        if (defaultMod)
            GameEvents.PlayerRespawn();

        // destroy this to prevent multiple purchases
        SFXManager.instance.PlayAmmoCollect();
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        GameEvents.OnPlayerRespawn -= Disable;
    }

    private void Update()
    {
        GetComponent<Button>().interactable = mod.cost <= DataDictionary.PlayerStats.EXP;
    }

    public void Initialize(Modifier _mod)
    {
        if (!defaultMod)
        {
            mod = _mod;
            StartCoroutine(nameText?.SetText(mod.name));
            StartCoroutine(descriptionText?.SetText(mod.description));
            StartCoroutine(costText?.SetText($"Cost: {mod.cost}"));
        }

        if (defaultMod) mod = ModifierGenerator.GetModifier();

        if (mod.cost > DataDictionary.PlayerStats.EXP) GetComponent<Button>().interactable = false;
    }
}
