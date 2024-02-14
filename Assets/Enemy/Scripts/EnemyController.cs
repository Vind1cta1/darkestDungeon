using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public PlayerController playerController;
    public List<PlayerCharacter> playerCharacters;
    public GameObject gameEndMenu;
    public bool enemyIsAttacking;
    public bool enemyIsDied;

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
            }
            foreach (EnemyCharacter child in GetComponentsInChildren<EnemyCharacter>())
            {
                if(child != null)
                {
                    int randomPlayerCharacter = Random.Range(0, playerCharacters.Count);
                    playerCharacters[randomPlayerCharacter].health -= child.damage;
                    playerCharacters[randomPlayerCharacter].morality -= child.damage / 2;
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
