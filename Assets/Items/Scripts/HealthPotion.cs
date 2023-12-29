using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Item
{
    public int healingAmount;
    public List<Character> partyMembers;

    public HealthPotion(int healingAmount)
    {
        itemName = "Health Potion";
        description = "Restores character's health.";
        this.healingAmount = healingAmount;
    }

    public override void Use()
    {
        foreach (Character character in partyMembers)
        {
            character.ReceiveHealing(healingAmount);
        }
    }
}