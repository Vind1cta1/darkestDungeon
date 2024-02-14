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

    private EnemyCharacter selectedEnemy;

    private void Update()
    {
        if (!enemyController.enemyIsAttacking)
        {
            foreach (PlayerCharacter character in gameObject.GetComponentsInChildren<PlayerCharacter>())
            {
                if (!character.isAttacked) 
                {
                    bool buttonPressed = false;
                    while (!buttonPressed)
                    {
                        if (Input.GetMouseButtonDown(0) && chestController.isInRoom && !enemyController.enemyIsDied)
                        {
                            RaycastHit hit;
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayerMask))
                            {
                                selectedEnemy = hit.collider.GetComponent<EnemyCharacter>();
                            }
                            if (selectedEnemy != null)
                            {
                                
                                selectedEnemy.health -= character.damage;
                                selectedEnemy = null;
                                character.isAttacked = true;
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
                character.health += 15;
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
                character.morality += 10;
            }
            int currentAmountOfPotion = int.Parse(amountOfMoralityPotion.text);
            currentAmountOfPotion--;
            amountOfMoralityPotion.text = currentAmountOfPotion.ToString();
        }   
    }
}
