using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelDisplay : MonoBehaviour
{
    private TextMeshProUGUI text;
    private GameSettings game;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        game = DataDictionary.GameSettings;
    }

    private void Update()
    {
        text.text = $"Floor: {game.GameLevel.ToString("000")}";
    }
}
