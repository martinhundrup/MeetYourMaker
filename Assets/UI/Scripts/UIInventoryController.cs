using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInventoryController : MonoBehaviour
{
    [Header("Items")]
    [SerializeField] private GameObject inventoryContainer;
    private List<Image> inventoryItems = new List<Image>();
    [SerializeField] private GameObject itemUIPrefab;

    [Header("Description")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private InventoryData playerInventory;

    private void Awake()
    {
        GameEvents.OnGamePaused += Initialize;
        playerInventory = DataDictionary.PlayerInventory;

        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameEvents.OnGamePaused -= Initialize;
    }

    public void Initialize(bool _paused)
    {
        gameObject.SetActive(_paused);

        if (_paused)
        {
            // destroy any item children in the inventory container
            foreach (var item in inventoryItems)
            {
                Destroy(item.gameObject);
            }
            inventoryItems.Clear();

            foreach (var item in playerInventory.InventoryDictionary)
            {
                Image obj = Instantiate(itemUIPrefab, inventoryContainer.transform)
                    .GetComponent<Image>();
                inventoryItems.Add(obj);
                obj.sprite = item.Key.Sprite;
            }
        }        
    }
}
