using System.Collections;
using UnityEngine;
using TMPro;


public class PlayerController : MonoBehaviour
{
    public EnemyController enemyController;
    public ChestController chestController;
    public LayerMask enemyLayerMask;
    public TMP_Text amountOfHealthPotion;
    public TMP_Text amountOfMoralityPotion;
    public int healthPower;
    public int moralityPower;
    public bool isInRoom;

    private EnemyCharacter selectedEnemy;

    private void Update()
    {
        if (!enemyController.enemyIsAttacking)
        {
            foreach (PlayerCharacter character in gameObject.GetComponentsInChildren<PlayerCharacter>())
            {
                if (!character.isAttacked && character.health > 0) 
                {
                    bool buttonPressed = false;
                    while (!buttonPressed)
                    {
                        if (Input.GetMouseButtonDown(0) && isInRoom && !enemyController.enemyIsDied)
                        {
                            RaycastHit hit;
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayerMask))
                            {
                                selectedEnemy = hit.collider.GetComponentInChildren<EnemyCharacter>();
                            }
                            if (selectedEnemy != null)
                            {
                                StartCoroutine(WaitForAttack(character));
                            }
                            buttonPressed = true;
                        }
                        return; 
                    }
                }
            }
            enemyController.enemyIsAttacking = true;
        }
    }

    public void UseHealthPotion()
    {
        if (int.Parse(amountOfHealthPotion.text) > 0)
        {
            foreach (PlayerCharacter character in gameObject.GetComponentsInChildren<PlayerCharacter>())
            {
                if(character.health > 0)
                {
                    character.health += healthPower;
                }
            }
            int currentAmountOfPotion = int.Parse(amountOfHealthPotion.text);
            currentAmountOfPotion--;
            amountOfHealthPotion.text = currentAmountOfPotion.ToString();
        }
 
    }

    public void UseMoralityPotion()
    {
        if (int.Parse(amountOfMoralityPotion.text) > 0)
        {
            foreach (PlayerCharacter character in gameObject.GetComponentsInChildren<PlayerCharacter>())
            {
                if (character.health > 0)
                {
                    character.morality += moralityPower;
                }
            }
            int currentAmountOfPotion = int.Parse(amountOfMoralityPotion.text);
            currentAmountOfPotion--;
            amountOfMoralityPotion.text = currentAmountOfPotion.ToString();
        }   
    }

    IEnumerator WaitForAttack(PlayerCharacter character)
    {
        Animator animator = character.GetComponent<Animator>();
        animator.CrossFade("Attack", 1f);
        selectedEnemy.health -= character.damage;
        selectedEnemy = null;
        character.isAttacked = true;
        yield return new WaitForSeconds(4);
    }
}
