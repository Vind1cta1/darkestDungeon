using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoralityPotion : MonoBehaviour
{
    private int moralityAmount;
    private List<Character> partyMembers;

    public void SetQuantityOfMoralityPotion(int moralityAmount)
    {
        this.moralityAmount = moralityAmount;
        Debug.Log(moralityAmount);
    }

    public void Use()
    {
        foreach (Character character in partyMembers)
        {
            character.RestoreMorale(moralityAmount);
        }
    }

    public int GetMoralityPotionAmount()
    {
        return moralityAmount;
    }
}
