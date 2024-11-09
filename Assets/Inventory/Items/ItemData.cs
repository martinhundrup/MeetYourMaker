using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] protected string itemName;
    [SerializeField] protected Sprite sprite;

    [TextArea]
    [SerializeField] protected string description;

    public string ItemName
    {
        get { return this.itemName; }
    }
    public Sprite Sprite
    {
        get { return this.sprite; }
    }
    public string Description
    {
        get { return this.description; }
    }
}