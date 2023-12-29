using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoralityPotion : Item
{
    public int moralityAmount;
    public List<Character> partyMembers;

    public MoralityPotion(int moralityAmount)
    {
        itemName = "Morality Potion";
        description = "Restores character's morality.";
        this.moralityAmount = moralityAmount;
    }

    public override void Use()
    {
        foreach (Character character in partyMembers)
        {
            character.RestoreMorale(moralityAmount);
        }
    }
}
