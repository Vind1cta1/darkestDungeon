using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyCharacter : MonoBehaviour
{
    public RectTransform healthBar;
    public Transform character;
    public Canvas canvas;
    public GameObject healthAndMorality;
    public GameObject prefabGameObject;
    public float distanceY;
    public float offsetY;
    public int health;
    public int damage;

    private Animator animator;
    private static int animMoveId;
    private static int animHpId;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animMoveId = Animator.StringToHash("IsInRoom");
        animHpId = Animator.StringToHash("HpAmount");
        UpdateHealthAndMoralityPosition();
    }

    private void Update()
    {
        if(health <=0)
        {
            health = 0;
        }
        healthBar.localScale = new Vector3(health / 100f, 1f, 1f);
        if(health <= 0)
        {
            animator.SetInteger(animHpId, -1);
            StartCoroutine(WaitForAnimation());
        }
    }

    private void UpdateHealthAndMoralityPosition()
    {
        Vector3 characterScreenPosition = Camera.main.WorldToScreenPoint(character.position);
        healthAndMorality.transform.position = characterScreenPosition + new Vector3(0f, distanceY, 0f);
    }

    IEnumerator WaitForAnimation()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            Transform transform = GetComponent<Transform>();
            transform.position = new Vector3(transform.position.x, offsetY, transform.position.z);
        }
        yield return new WaitForSeconds(2);
        Die();
    }

    private void Die()
    {
        Destroy(prefabGameObject);
    }
}
