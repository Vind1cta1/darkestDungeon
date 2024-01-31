using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour 
{
    private int healingAmount;
    private List<Character> partyMembers;

    public void SetQuantityOfHealthPotion(int healingAmount)
    {
        this.healingAmount = healingAmount;
        Debug.Log(healingAmount);
    }

    public void Use()
    {
        foreach (Character character in partyMembers)
        {
            character.ReceiveHealing(healingAmount);
        }
    }

    public int GetHealthPotionAmount()
    {
        return healingAmount;
    }
}