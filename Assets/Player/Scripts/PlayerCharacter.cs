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
    public GameObject prefabGameObject;
    public float distanceY;
    public float offsetY;
    public int health;
    public int morality;
    public int normalDamage;
    public int damage;
    public bool isAttacked;

    private PlayerController playerController;
    private Animator animator;
    private static int animMoveId;
    private static int animHpId;

    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        animator = GetComponent<Animator>();
        animMoveId = Animator.StringToHash("IsInRoom");
        animHpId = Animator.StringToHash("HpAmount");
        UpdateHealthAndMoralityPosition();
    }

    private void Update()
    {
        if(!playerController.isInRoom && !playerController.chestController.isOpen)
        {
            animator.SetBool(animMoveId, true);
        }
        else
        {
            animator.SetBool(animMoveId, false);
        }
        if(health > 100)
        {
            health = 100;
        }
        else if(health < 0)
        {
            health = 0;
        }
        if(morality > 100)
        {
            morality = 100;
        }
        else if(morality < 0)
        {
            morality = 0;
        }
        healthBar.localScale = new Vector3(health / 100f, 1f, 1f);
        moraleBar.localScale = new Vector3(morality / 100f, 1f, 1f);
        damage = (int)(normalDamage * ((float)morality / 100f));
        if (health <= 0)
        {
            animator.SetInteger(animHpId, -1);
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
            {
                Transform transform = GetComponent<Transform>();
                transform.position = new Vector3(transform.position.x, offsetY, transform.position.z);
            }
        }
    }

    private void UpdateHealthAndMoralityPosition()
    {
        Vector3 characterScreenPosition = Camera.main.WorldToScreenPoint(character.position);
        healthAndMorality.transform.position = characterScreenPosition + new Vector3(0f, distanceY, 0f);
    }

    public void Die()
    {
        Destroy(prefabGameObject);
    }
}
