using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    [SerializeField] private float spawnDistance = 15f;
    [SerializeField] private float minSpawnInterval = 5f;
    [SerializeField] private float maxSpawnInterval = 15f;
    [SerializeField] private int minEnemyCount = 3;
    [SerializeField] private int maxEnemyCount = 6;
    [SerializeField] private float buffInterval = 20f;
    [SerializeField] private int enemyCountBuff = 1;
    [SerializeField] private float enemyHealthBuff = 0.20f;
    [SerializeField] private Enemy[] enemyPrefabs;

    private int currEnemyCountBuff = 0;
    private float currEnemyHealthBuff = 1f;

    private void Start()
    {
        StartCoroutine(_SpawnLoop());
    }

    private IEnumerator _SpawnLoop()
    {
        while (true)
        {
            Vector2 playerPos = GameManager.Player.transform.position;

            int enemyCount = Random.Range(minEnemyCount, maxEnemyCount + currEnemyCountBuff + 1);
            for (int i = 0; i < enemyCount; i++)
            {
                Enemy enemyType = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
                Vector2 spawnPos = playerPos + Random.insideUnitCircle.normalized * spawnDistance;
                Enemy enemy = Instantiate(enemyType, spawnPos, Quaternion.identity);
                enemy.UpgradeMaxHealth(currEnemyHealthBuff);
            }

            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
        }
    }

    private IEnumerator _DifficultyLoop()
    {
        while (true)
        {
            currEnemyCountBuff += enemyCountBuff;
            currEnemyHealthBuff += enemyHealthBuff;
            yield return new WaitForSeconds(buffInterval);
        }
    }
}
