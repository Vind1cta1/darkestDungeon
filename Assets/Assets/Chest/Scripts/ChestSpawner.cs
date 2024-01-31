using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestSpawner : MonoBehaviour
{
    public GameObject chest;
    public GameObject coinPrefab;
    public GameObject healthPotionPrefab;
    public GameObject moralityPotionPrefab;
    public TextChanger coinTextChanger;
    public TextChanger healthPotionTextChanger;
    public TextChanger moralityPotionTextChanger;
    public Transform plane;
    public ChestController chestController;
    public Vector3 position;
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

    public void SpawnChest()
    {

        if(chest.transform.position.x <= -10f && !chestController.isInRoom)
        {
            DeleteOldItems();
            chest.transform.position = position;
            FillInventoryRandomly();
            chest.SetActive(true);
        }
        Invoke("SpawnChest", Random.Range(minSpawnTime, maxSpawnTime));
    }

    private void FillInventoryRandomly()
    {
        coinCount = Random.Range(10, 101);
        healthPotionCount = Random.Range(0, 2);
        moralityPotionCount = Random.Range(0, 2);


        UpdateUIText();

        Vector3 spawnPosition = new Vector3(0f, 0f, 0f);
        
        Instantiate(coinPrefab, spawnPosition, Quaternion.identity, plane);
        Coin moneyItem = coinPrefab.GetComponent<Coin>();
        moneyItem.SetQuantityOfMoney(coinCount);
        if (healthPotionCount != 0)
        { 
            Instantiate(healthPotionPrefab, spawnPosition, Quaternion.identity, plane);
            HealthPotion healthPotionItem = healthPotionPrefab.GetComponent<HealthPotion>();
            healthPotionItem.SetQuantityOfHealthPotion(healthPotionCount);
            
        }
        if (moralityPotionCount != 0)
        {
            Instantiate(moralityPotionPrefab, spawnPosition, Quaternion.identity, plane);
            MoralityPotion moralityPotionItem = moralityPotionPrefab.GetComponent<MoralityPotion>();
            moralityPotionItem.SetQuantityOfMoralityPotion(moralityPotionCount);
        }
    }

    private void UpdateUIText()
    {
        coinTextChanger.ChangeText(coinCount.ToString());
        healthPotionTextChanger.ChangeText(healthPotionCount.ToString());
        moralityPotionTextChanger.ChangeText(moralityPotionCount.ToString());
    }

    private void DeleteOldItems()
    {
        foreach (Transform child in plane)
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
