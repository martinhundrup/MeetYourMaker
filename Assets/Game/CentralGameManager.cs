using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CentralGameManager : MonoBehaviour
{
    // places we'll need likely access to
    private InventoryData playerInventory;
    private bool isPaused = false;

    private void Awake()
    {
        GetComponents();
        SubscribeGameEvents();
    }

    private void OnDestroy()
    {
        UnSubscribeGameEvents();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            GameEvents.GamePaused(isPaused);
        }
    }

    private void ItemPickedUp(ItemData _item)
    {
        playerInventory.AddItem(_item);
    }

    #region UTILITIES

    private void GetComponents()
    {
        playerInventory = DataDictionary.PlayerInventory;
    }

    private void SubscribeGameEvents()
    {
        GameEvents.OnItemPickedUp += ItemPickedUp;
    }

    private void UnSubscribeGameEvents()
    {
        GameEvents.OnItemPickedUp -= ItemPickedUp;
    }

    #endregion
}
