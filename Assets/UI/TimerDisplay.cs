using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerDisplay : MonoBehaviour
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
        float elapsedTime = player.GameTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000);

        text.text = $"{minutes:00}:{seconds:00}:{milliseconds:000}";
    }
}
