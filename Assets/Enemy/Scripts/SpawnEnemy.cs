using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public EnemyController enemyController;
    public Transform enemy;
    public List<GameObject> enemies;
    public Vector3 spawnPosition;
    public Vector3 distanceBetweenEnemies;
    public int numberOfEnemies;
    public void SpawnEnemyCharacter()
    {
        Vector3 currentSpawnPosition = spawnPosition;
        for(int i = 0; i < numberOfEnemies; i++)
        {
            GameObject currentEnemy = enemies[Random.Range(0, enemies.Count)]; 
            Instantiate(currentEnemy, currentSpawnPosition, Quaternion.identity, enemy);
            currentSpawnPosition = currentSpawnPosition + distanceBetweenEnemies;
        }
        enemyController.enemyIsDied = false;
    }
}
