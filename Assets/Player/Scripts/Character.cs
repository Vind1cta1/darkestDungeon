using UnityEngine;

public class Character : MonoBehaviour
{
    public RectTransform healthBar;
    public RectTransform moraleBar;
    public Transform character;
    public Canvas canvas;
    public GameObject healthAndMorality;
    public float distanceX;
    public float distanceY;

    private int health = 100;
    private int morality = 100;

    private void Start()
    {
        UpdateHealthAndMoralityPosition();
    }

    private void Update()
    {
        healthBar.localScale = new Vector3(health / 100f, 1f, 1f);
        moraleBar.localScale = new Vector3(morality / 100f, 1f, 1f);
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
        healthAndMorality.transform.position = characterScreenPosition + new Vector3(distanceX, distanceY, 0f);
    }

    public void TakeDamage( int damage)
    {
        health -= damage;
        morality -= damage;
    }
}
