using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierUI : MonoBehaviour
{
    [SerializeField] private GameObject victory;
    [SerializeField] private GameObject defeat;
    private void Awake()
    {
        if (CentralGameManager.instance.BetweenLevels)
        {
            victory.SetActive(true);
            defeat.SetActive(false);
        }
        else
        {
            victory.SetActive(false);
            defeat.SetActive(true);
        }
    }
}
