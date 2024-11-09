using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DungeonGenerator))]
public class RoomController : MonoBehaviour
{
    private DungeonGenerator generator;
    private Enemy[] enemies;
    private CameraBounder cameraBounder;
    private int borderWidth;
    private int roomWidth;
    private int roomHeight;
    private int enemyCount = 0;
    [SerializeField] private GameObject endOfLevel;

    private void Awake()
    {
        generator = GetComponent<DungeonGenerator>();
        enemyCount = 0;

        borderWidth = DataDictionary.GameSettings.RoomBorder;
        roomWidth = DataDictionary.GameSettings.RoomSize.x;
        roomHeight = DataDictionary.GameSettings.RoomSize.y;

        cameraBounder = new GameObject().AddComponent<CameraBounder>();
        cameraBounder.transform.parent = this.transform;
        cameraBounder.transform.localPosition = Vector2.zero + new Vector2(0f, -0.5f);
        cameraBounder.transform.localScale = new Vector2(roomWidth - 4, roomHeight - 4);
    }

    public void CountEnemies()
    {
        enemies = FindObjectsOfType<Enemy>();
        enemyCount = 0;

        foreach (Enemy enemy in enemies)
        {
            enemyCount++;

            enemy.OnEnemyDied += EnemyDeath;

        }
    }

    private void EnemyDeath(Enemy _enemy)
    {
        Debug.Log("enemy died!");
        enemyCount--;
        _enemy.OnEnemyDied -= EnemyDeath;

        if (enemyCount <= 0)
        {
            Debug.Log("room cleared!");

            Instantiate(endOfLevel).transform.position = _enemy.transform.position;
        }
    }
}
