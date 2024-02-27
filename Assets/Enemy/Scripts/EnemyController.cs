using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyController : MonoBehaviour
{
    public PlayerController playerController;
    public List<PlayerCharacter> playerCharacters;
    public GameObject gameEndMenu;
    public bool enemyIsAttacking;
    public bool enemyIsDied;

    private int randomPlayerCharacter;
    private List<PlayerCharacter> aliveCharacters;

    private void Update()
    {
        if(enemyIsAttacking)
        {
            foreach (PlayerCharacter child in playerController.GetComponentsInChildren<PlayerCharacter>())
            {
                playerCharacters.Add(child);
            }
            if(playerCharacters.Count == 0) 
            {
                gameEndMenu.SetActive(true);
                return;
            }
            foreach (EnemyCharacter child in GetComponentsInChildren<EnemyCharacter>())
            {
                if(child != null && child.health > 0)
                {
                    aliveCharacters = playerCharacters.FindAll(character => character.health > 0);
                    if(aliveCharacters.Count > 0)
                    {
                        randomPlayerCharacter = Random.Range(0, aliveCharacters.Count);
                        if (aliveCharacters[randomPlayerCharacter] != null)
                        {
                            Animator animator = child.GetComponent<Animator>();
                            animator.CrossFade("Attack", 1f);
                            aliveCharacters[randomPlayerCharacter].health -= child.damage;
                            aliveCharacters[randomPlayerCharacter].morality -= child.damage / 2;
                        }
                    }
                    else
                    {
                        gameEndMenu.SetActive(true);
                        return;
                    }
                }
            }
            enemyIsAttacking = false;
            foreach (PlayerCharacter child in playerCharacters)
            {
                child.isAttacked = false;
            }
            playerCharacters.Clear();
        }
        if(GetComponentsInChildren<EnemyCharacter>().Length == 0)
        {
            enemyIsDied = true;
        }
    }
}
