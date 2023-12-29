using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
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

    private void Start()
    {
        FillInventoryRandomly();
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
            Instantiate(moralityPotionPrefab,spawnPosition, Quaternion.identity, canvas.transform);
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
