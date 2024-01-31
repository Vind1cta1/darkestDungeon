using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Timers;

public class ChestController : MonoBehaviour
{
    private float chestSpeed = 5.45f;

    public bool isOpen;
    public bool isInRoom = true;
    public PlayerController playerController;
    public GameObject inventoryUI;
    public TMP_Text moneyQuantity;
    public TMP_Text healthQuantity;
    public TMP_Text moralityQuantity;
    public TextChanger coinTextChanger;
    public TextChanger healthPotionTextChanger;
    public TextChanger moralityPotionTextChanger;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    private void Update()
    {
        if(!isInRoom)
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
                Coin moneyItem = item.GetComponent<Coin>();
                if (moneyItem != null)
                {
                    int moneyInItem = moneyItem.GetMoneyAmount();
                    int totalMoney = int.Parse(moneyQuantity.text);
                    totalMoney += moneyInItem;
                    moneyQuantity.text = totalMoney.ToString();
                }
            }
            else if (item.layer == LayerMask.NameToLayer("HealthPotion"))
            {
                HealthPotion healthPotionItem = item.GetComponent<HealthPotion>();
                if (healthPotionItem != null)
                {
                    int totalPotion = int.Parse(healthQuantity.text);
                    int healthPotionQuantity = healthPotionItem.GetHealthPotionAmount();
                    totalPotion += healthPotionQuantity;
                    healthQuantity.text = totalPotion.ToString();
                }
            }
            else if (item.layer == LayerMask.NameToLayer("MoralityPotion"))
            {
                MoralityPotion moralityPotionItem = item.GetComponent<MoralityPotion>();
                if (moralityPotionItem != null)
                {
                    int totalPotion = int.Parse(moralityQuantity.text);
                    int moralityPotionQuantity = moralityPotionItem.GetMoralityPotionAmount();
                    totalPotion += moralityPotionQuantity;
                    moralityQuantity.text = totalPotion.ToString();
                }
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
