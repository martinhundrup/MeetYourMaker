using UnityEngine;
using TMPro;
using UnityEditor;

[RequireComponent(typeof(TextMeshProUGUI))]
public class AmmoDisplay : MonoBehaviour
{
    private TextMeshProUGUI text;
    private PlayerStats player;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        player = DataDictionary.PlayerStats;
    }

    private void Update()
    {
        text.text = $"Ammo: {player.AmmoCount.ToString("000")}/{player.MaxAmmo.ToString("000")}";

    }
}
