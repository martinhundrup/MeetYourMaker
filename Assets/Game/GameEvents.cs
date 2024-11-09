using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static event Action<Transform> OnPlayerEnterRoom;
    public static event Action<ItemData> OnItemPickedUp;
    public static event Action<bool> OnGamePaused;

    public static void PlayerEnterRoom(Transform roomBounder)
    {
        OnPlayerEnterRoom?.Invoke(roomBounder);
    }
    public static void ItemPickedUp(ItemData item)
    {
        OnItemPickedUp?.Invoke(item);
    }
    public static void GamePaused(bool _paused) 
    {
        OnGamePaused?.Invoke(_paused); 
    }
}
