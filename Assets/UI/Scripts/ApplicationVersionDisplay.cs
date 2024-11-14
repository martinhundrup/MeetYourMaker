using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ApplicationVersionDisplay : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<TextMeshProUGUI>().text = $"v{Application.version}";
    }
}
