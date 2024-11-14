using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable, CreateAssetMenu(menuName = "Generators/Enemy Generator")]
public class EnemyGenerator : ScriptableObject
{
    public delegate void SpawnEnemy(int index, GameObject enemy);
    public event SpawnEnemy OnSpawnEnemy;

    [SerializeField] private List<GameObject> enemyPrefabs;
    private float balance;
    protected int roomWidth;
    protected int roomHeight;
    protected int index;
    public int Index
    {
        get { return index; }
    }

    // finds the amount of points the room has to spend on enemies
    private void CalculateBalance()
    {
        int gameLevel = DataDictionary.GameSettings.GameLevel;
        balance = 2 + gameLevel * 1.1f + UnityEngine.Random.Range(-gameLevel, gameLevel);
    }

    private GameObject GetRandomEnemy()
    {
        if (balance <= 0) return null; // we don't need more enemies

        else
        {
            var enemy = enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Count)];
            balance -= enemy.GetComponent<Enemy>().Cost;
            return enemy;
        }
    }

    public void PlaceEnemies(int[] obstacleTiles, int[] wallTiles, int _index)
    {
        Debug.Log(wallTiles[279 + 18]);
        CalculateBalance();
        index = _index;

        roomHeight = DataDictionary.GameSettings.RoomSize.y;
        roomWidth = DataDictionary.GameSettings.RoomSize.x;

        float offsetX = UnityEngine.Random.Range(0f, 100f);
        float offsetY = UnityEngine.Random.Range(0f, 100f);

        List<int> availableTiles = new List<int>();

        for (int y = 0; y < roomHeight; y++)
        {
            for (int x = 0; x < roomWidth; x++)
            {
                int i = Convert2DTo1DIndex(y, x);

                if (obstacleTiles[i] == 0 && wallTiles[i] != 0 &&  // if empty tile and not next to a wall
                    (Vector2.Distance(ConvertToWorldPosition(i), ConvertToWorldPosition(Convert2DTo1DIndex(roomHeight / 2, roomWidth / 2))) > 2.5f)) // far away from playerf
                {
                    // Check neighboring tiles for obstacles
                    bool hasNeighboringObstacle = false;
                    int[] neighborOffsets = { i - 1, i + 1, i - roomWidth, i + roomWidth }; // left, right, up, down

                    foreach (int offset in neighborOffsets)
                    {                        
                        if (obstacleTiles[offset] != 0 || wallTiles[offset] == 0)
                        {
                            hasNeighboringObstacle = true;
                            break;
                        }
                    }

                    if (!hasNeighboringObstacle)
                    {
                        availableTiles.Add(i);
                    }
                }
            }
        }

        var enemy = GetRandomEnemy();
        int enemyCount = 0;
        while (enemy != null && availableTiles.Count > 0)
        {
            int i = availableTiles[UnityEngine.Random.Range(0, availableTiles.Count)];
            availableTiles.Remove(i);

            if (OnSpawnEnemy != null)
                OnSpawnEnemy(i, enemy);

            enemy = GetRandomEnemy();
            enemyCount++;
        }
    }

    private Vector2 ConvertToWorldPosition(int index)
    {
        Vector2 gridPosition = Convert1DTo2DIndex(index);
        // Ensure x corresponds to columns and y to rows for consistent positioning
        float x = gridPosition.x - roomWidth / 2f + 0.5f;
        float y = gridPosition.y - roomHeight / 2f + 0.5f;
        return new Vector2(x, y);
    }

    private Vector2 Convert1DTo2DIndex(int index)
    {
        int row = index / roomWidth;
        int column = index % roomWidth;
        // Return (column, row) to match (x, y) coordinates
        return new Vector2(column, row);
    }

    protected int Convert2DTo1DIndex(int row, int column)
    {
        // Keep as is to match the row-major layout
        return row * this.roomWidth + column;
    }
}
