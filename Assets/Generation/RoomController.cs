using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DungeonGenerator))]
public class RoomController : MonoBehaviour
{
    // this name isn't the best - true means player entered room for first time, false means enemies were just cleared
    public delegate void PlayerEnteredRoom(bool _entered);
    public event PlayerEnteredRoom OnPlayerEnteredRoom;

    private DungeonGenerator generator;
    private Enemy[] enemies;
    private CameraBounder cameraBounder;
    private int borderWidth;
    private int roomWidth;
    private int roomHeight;
    [SerializeField] private GameObject sideDoorPrefab;
    [SerializeField] private GameObject topDoorPrefab;
    private bool roomCleared = false;
    private int enemyCount = 0;

    private void Awake()
    {
        generator = GetComponent<DungeonGenerator>();

        borderWidth = DataDictionary.GameSettings.RoomBorder;
        roomWidth = DataDictionary.GameSettings.RoomSize.x;
        roomHeight = DataDictionary.GameSettings.RoomSize.y;

        cameraBounder = new GameObject().AddComponent<CameraBounder>();
        cameraBounder.transform.parent = this.transform;
        cameraBounder.transform.localPosition = Vector2.zero + new Vector2(0f, -0.5f);
        cameraBounder.transform.localScale = new Vector2(roomWidth - 4, roomHeight - 4);

        GameEvents.OnPlayerEnterRoom += PlayerEnterRoom;
    }

    public void Generate(int _numTiles, bool up, bool down, bool left, bool right)
    {
        generator.Generate(_numTiles, up, down, left, right);
        enemies = GetComponentsInChildren<Enemy>();

        foreach (var enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
        }

        if (down)
        {
            var obj = Instantiate(topDoorPrefab, transform);
            obj.transform.localPosition = new Vector2(-0.5f, 4.5f);
        }
        if (up)
        {
            var obj = Instantiate(topDoorPrefab, transform);
            obj.transform.localPosition = new Vector2(-0.5f, -6f);
        }
        if (left)
        {
            var obj = Instantiate(sideDoorPrefab, transform);
            obj.transform.localPosition = new Vector2(-9.5f, -1);
        }
        if (right)
        {
            var obj = Instantiate(sideDoorPrefab, transform);
            obj.transform.localPosition = new Vector2(9.5f, -1);
        }
    }

    public void PlayerEnterRoom(Transform _transform)
    {
        if (cameraBounder.transform == _transform && enemies != null && !roomCleared)
        {
            if (OnPlayerEnteredRoom != null)
                OnPlayerEnteredRoom(true); // true fo

            foreach (var enemy in enemies)
            {
                enemy.gameObject.SetActive(true);
                enemy.OnEnemyDied += EnemyDeath;
                enemyCount++;
            }

            enemies = null;
        }
    }

    private void EnemyDeath(Enemy _enemy)
    {
        enemyCount--;
        _enemy.OnEnemyDied -= EnemyDeath;

        if (enemyCount == 0)
        {
            roomCleared = true;
            if (OnPlayerEnteredRoom != null)
                OnPlayerEnteredRoom(false);
        }
    }
}
