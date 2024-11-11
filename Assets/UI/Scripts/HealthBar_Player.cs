
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Player : MonoBehaviour
{
    private Slider slider;
    private PlayerStats stats;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        stats = DataDictionary.PlayerStats;

        GetComponent<RectTransform>().sizeDelta = new Vector2(stats.PlayerMaxHealth * 20, 50);
    }

    private void Update()
    {
        slider.value = stats.PlayerHealth / stats.PlayerMaxHealth;
    }
}
