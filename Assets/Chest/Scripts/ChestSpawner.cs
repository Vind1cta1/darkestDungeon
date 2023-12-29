using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    public GameObject chest;
    public GameObject coinPrefab;
    public GameObject healthPotionPrefab;
    public GameObject moralityPotionPrefab;
    public GameObject ringPrefab;
    public TextChanger coinTextChanger;
    public TextChanger healthPotionTextChanger;
    public TextChanger moralityPotionTextChanger;
    public TextChanger ringTextChanger;
    public Canvas canvas;

    private int coinCount = 0;
    private int healthPotionCount = 0;
    private int moralityPotionCount = 0;
    public float minSpawnTime;
    public float maxSpawnTime;
    private void Start()
    {
        Invoke("SpawnChest", Random.Range(minSpawnTime, maxSpawnTime));
    }

    private void SpawnChest()
    {

        Vector3 position = new Vector3(10, -3.5f, 0f);
        if(chest.transform.position.x < -9f)
        {
            chest.transform.position = position;
            FillInventoryRandomly();
            chest.SetActive(true);
        }
        Invoke("SpawnChest", Random.Range(minSpawnTime, maxSpawnTime));
    }

    public void FillInventoryRandomly()
    {
        coinCount = Random.Range(10, 101);
        healthPotionCount = Random.Range(0, 3);
        moralityPotionCount = Random.Range(0, 3);

        UpdateUIText();

        Vector3 spawnPosition = new Vector3(600f, 800f, 0f);

        Instantiate(coinPrefab, spawnPosition, Quaternion.identity, canvas.transform);
        if (healthPotionCount != 0)
        {
            spawnPosition = spawnPosition + new Vector3(200f, 0f, 0f);
            Instantiate(healthPotionPrefab, spawnPosition, Quaternion.identity, canvas.transform);
        }
        if (moralityPotionCount != 0)
        {
            spawnPosition = spawnPosition + new Vector3(200f, 0f, 0f);
            Instantiate(moralityPotionPrefab, spawnPosition, Quaternion.identity, canvas.transform);
        }
        spawnPosition = spawnPosition + new Vector3(200f, 0f, 0f);
        Instantiate(ringPrefab, spawnPosition, Quaternion.identity, canvas.transform);
    }

    private void UpdateUIText()
    {
        coinTextChanger.ChangeText(coinCount.ToString());
        healthPotionTextChanger.ChangeText(healthPotionCount.ToString());
        moralityPotionTextChanger.ChangeText(moralityPotionCount.ToString());
    }
}
