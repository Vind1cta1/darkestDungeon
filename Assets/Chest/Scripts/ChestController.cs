using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Timers;

public class ChestController : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject inventoryUI;
    public TMP_Text moneyQuantity;
    public TMP_Text healthQuantity;
    public TMP_Text moralityQuantity;
    public TextChanger coinTextChanger;
    public TextChanger healthPotionTextChanger;
    public TextChanger moralityPotionTextChanger;
    public bool isOpen;
    public int amountOfOpenChests;

    private float chestSpeed = 5.45f;

    private void Update()
    {
        if(!playerController.isInRoom)
        {
            if (!isOpen && transform.position.x > -10f)
            {
                transform.position = transform.position - new Vector3(Time.deltaTime * chestSpeed, 0f, 0f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            amountOfOpenChests++;
            isOpen = true;
            inventoryUI.SetActive(true);
        }
    }

    public void OnPickupButtonClicked()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject item in items)
        {
            if (item.layer == LayerMask.NameToLayer("Money"))
            {
                int moneyInItem = int.Parse(coinTextChanger.GetText());
                int totalMoney = int.Parse(moneyQuantity.text);
                totalMoney += moneyInItem;
                moneyQuantity.text = totalMoney.ToString();
            }
            else if (item.layer == LayerMask.NameToLayer("HealthPotion"))
            {
                int totalPotion = int.Parse(healthQuantity.text);
                int healthPotionQuantity = int.Parse(healthPotionTextChanger.GetText());
                totalPotion += healthPotionQuantity;
                healthQuantity.text = totalPotion.ToString();
            }
            else if (item.layer == LayerMask.NameToLayer("MoralityPotion"))
            {
                int totalPotion = int.Parse(moralityQuantity.text);
                int moralityPotionQuantity = int.Parse(moralityPotionTextChanger.GetText());
                totalPotion += moralityPotionQuantity;
                moralityQuantity.text = totalPotion.ToString();
            }
        }
        isOpen = false;
        inventoryUI.SetActive(false);
    }

    public void OnLeaveButtonClicked()
    {
        isOpen = false;
        inventoryUI.SetActive(false);
    }
}
