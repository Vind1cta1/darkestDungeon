using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public RectTransform healthBar;
    public RectTransform moraleBar;
    public Transform character;
    public Canvas canvas;
    public GameObject healthAndMorality;
    public float distanceY;
    public int health;
    public int morality;
    public int damage;
    public bool isAttacked;

    private void Start()
    {
        UpdateHealthAndMoralityPosition();
    }

    private void Update()
    {
        if(health > 100)
        {
            health = 100;
        }
        if(morality > 100)
        {
            morality = 100;
        }
        healthBar.localScale = new Vector3(health / 100f, 1f, 1f);
        moraleBar.localScale = new Vector3(morality / 100f, 1f, 1f);
        if(health <= 0)
        {
            Die();
        }
    }

    public void ReceiveHealing(int healthAmount)
    {
        health += healthAmount;
    }

    public void RestoreMorale(int healthAmount)
    {
        morality += healthAmount;
    }

    private void UpdateHealthAndMoralityPosition()
    {
        Vector3 characterScreenPosition = Camera.main.WorldToScreenPoint(character.position);
        healthAndMorality.transform.position = characterScreenPosition + new Vector3(0f, distanceY, 0f);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        morality -= damage;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
