using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyCharacter : MonoBehaviour
{
    public RectTransform healthBar;
    public RectTransform moraleBar;
    public Transform character;
    public Canvas canvas;
    public GameObject healthAndMorality;
    public float distanceY;
    public int health;
    public int damage;

    private void Start()
    {
        UpdateHealthAndMoralityPosition();
    }

    private void Update()
    {
        healthBar.localScale = new Vector3(health / 100f, 1f, 1f);
        if(health <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthAndMoralityPosition()
    {
        Vector3 characterScreenPosition = Camera.main.WorldToScreenPoint(character.position);
        healthAndMorality.transform.position = characterScreenPosition + new Vector3(0f, distanceY, 0f);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
