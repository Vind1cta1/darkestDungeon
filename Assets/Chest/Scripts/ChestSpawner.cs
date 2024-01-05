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
    public RectTransform canvasRectTransform;
    public float minSpawnTime;
    public float maxSpawnTime;

    private List<GameObject> itemsToRemove = new List<GameObject>();
    private int coinCount = 0;
    private int healthPotionCount = 0;
    private int moralityPotionCount = 0;
    private void Start()
    {
        Invoke("SpawnChest", Random.Range(minSpawnTime, maxSpawnTime));
    }

    private void SpawnChest()
    {

        Vector3 position = new Vector3(10, -3.5f, 0f);
        if(chest.transform.position.x < -9f)
        {
            DeleteOldItems();
            chest.transform.position = position;
            FillInventoryRandomly();
            chest.SetActive(true);
        }
        Invoke("SpawnChest", Random.Range(minSpawnTime, maxSpawnTime));
    }

    public void FillInventoryRandomly()
    {
        coinCount = Random.Range(10, 101);
        healthPotionCount = Random.Range(0, 2);
        moralityPotionCount = Random.Range(0, 2);

        UpdateUIText();

        Vector3 spawnPosition = new Vector3(600f, 800f, 0f);
        
        Instantiate(coinPrefab, spawnPosition, Quaternion.identity, canvasRectTransform);
        if (healthPotionCount != 0)
        {
            spawnPosition = spawnPosition + new Vector3(200f, 0f, 0f);
            Instantiate(healthPotionPrefab, spawnPosition, Quaternion.identity, canvasRectTransform);
        }
        if (moralityPotionCount != 0)
        {
            spawnPosition = spawnPosition + new Vector3(200f, 0f, 0f);
            Instantiate(moralityPotionPrefab, spawnPosition, Quaternion.identity, canvasRectTransform);
        }
        spawnPosition = spawnPosition + new Vector3(200f, 0f, 0f);
        Instantiate(ringPrefab, spawnPosition, Quaternion.identity, canvasRectTransform);
    }

    private void UpdateUIText()
    {
        coinTextChanger.ChangeText(coinCount.ToString());
        healthPotionTextChanger.ChangeText(healthPotionCount.ToString());
        moralityPotionTextChanger.ChangeText(moralityPotionCount.ToString());
    }

    private void DeleteOldItems()
    {
        foreach (Transform child in canvasRectTransform)
        {
            if (child.CompareTag("Item"))
            {
                itemsToRemove.Add(child.gameObject);
            }
        }
        foreach (var item in itemsToRemove)
        {
            Destroy(item);
        }
    }
}
