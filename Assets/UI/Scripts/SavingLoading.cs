using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingLoading : MonoBehaviour
{
    public void Save()
    {
        DataDictionary.PlayerStats.Save();
    }

    public void Load()
    {
        DataDictionary.PlayerStats.LoadFromFile();
    }
}
