using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class EXPDisplay : MonoBehaviour
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
        text.text = $"exp: {player.EXP.ToString("000")}";

    }
}
