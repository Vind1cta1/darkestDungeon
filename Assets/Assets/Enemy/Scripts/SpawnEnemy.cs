using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public Transform enemy;
    public List<GameObject> enemies;
    public Vector3 spawnPosition;
    public Vector3 distanceBetweenEnemies;
    public int numberOfEnemies;

    private void Start()
    {
        SpawnEnemyCharacter(); 
    }
    public void SpawnEnemyCharacter()
    {
        for(int i = 0; i < numberOfEnemies; i++)
        {
            GameObject currentEnemy = enemies[Random.Range(0, enemies.Count)]; 
            Instantiate(currentEnemy, spawnPosition, Quaternion.identity, enemy);
            spawnPosition = spawnPosition + distanceBetweenEnemies;
        }
    }
}
