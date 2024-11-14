
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Player : MonoBehaviour
{
    private Slider slider;
    private PlayerStats stats;
    [SerializeField] private RectTransform thingToExpand = null;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        stats = DataDictionary.PlayerStats;

        thingToExpand.sizeDelta = new Vector2(stats.PlayerMaxHealth * 20, 50);
    }

    private void Update()
    {
        slider.value = stats.PlayerHealth / stats.PlayerMaxHealth;
        thingToExpand.sizeDelta = new Vector2(26f, stats.PlayerMaxHealth * 20);

        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponentInParent<RectTransform>());
    }
}
