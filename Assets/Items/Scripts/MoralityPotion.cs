using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoralityPotion : MonoBehaviour
{
    private int moralityAmount;
    private List<PlayerCharacter> partyMembers;

    public void SetQuantityOfMoralityPotion(int moralityAmount)
    {
        this.moralityAmount = moralityAmount;
    }

    public int GetMoralityPotionAmount()
    {
        return moralityAmount;
    }
}
