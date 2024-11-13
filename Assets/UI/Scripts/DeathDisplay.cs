using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DeathDisplay : MonoBehaviour
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
        text.text = $"Deaths: {player.Deaths.ToString("000")}";

    }
}
