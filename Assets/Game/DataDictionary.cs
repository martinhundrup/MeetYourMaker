using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDictionary : MonoBehaviour
{
    private static DataDictionary instance;

    private PlayerStats playerStats;
    private GameSettings gameSettings;
    private InventoryData playerInventory;

    private void Initialize()
    {
        playerStats = Resources.Load<PlayerStats>("Player Stats");
        if (playerStats == null)
        {
            Debug.LogError("Player Stats not found in Resources folder.");
        }

        gameSettings = Resources.Load<GameSettings>("Game Settings");
        if (gameSettings == null)
        {
            Debug.LogError("Game Settings not found in Resources folder.");
        }

        playerInventory = Resources.Load<InventoryData>("Player Inventory");
        if (playerInventory == null)
        {
            Debug.LogError("Player Inventory not found in Resources folder.");
        }

    }

    public static PlayerStats PlayerStats
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("DataDictionaryManager").AddComponent<DataDictionary>();
                instance.Initialize();
            }
            return instance.playerStats;
        }
    }

    public static GameSettings GameSettings
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("DataDictionaryManager").AddComponent<DataDictionary>();
                instance.Initialize();
            }
            return instance.gameSettings;
        }
    }

    public static InventoryData PlayerInventory
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("DataDictionaryManager").AddComponent<DataDictionary>();
                instance.Initialize();
            }
            return instance.playerInventory;
        }
    }

}
