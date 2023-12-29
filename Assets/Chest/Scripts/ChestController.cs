using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    [SerializeField] private float chestSpeed = 5.45f;

    public bool isopen;
    public GameObject inventoryUI;
    public List<Item> itemsInChest;

    void Update()
    {
        if (transform.position.x < -9f)
        {
            gameObject.SetActive(false);
        }
        if (!isopen)
        {
            transform.position = transform.position - new Vector3(Time.deltaTime * chestSpeed, 0f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isopen = true;
            inventoryUI.SetActive(true);
        }
    }

    public void OnPickupButtonClicked()
    {

        //foreach (Item item in itemsInChest)
        //{
        //    GameManager.Instance.playerInventory.AddItem(item);
        //}
        isopen = false;
        inventoryUI.SetActive(false);
    }

    public void OnLeaveButtonClicked()
    {
        isopen = false;
        inventoryUI.SetActive(false);
    }
}
