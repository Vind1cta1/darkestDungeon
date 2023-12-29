using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private int health;
    private int morale;
    public void ReceiveHealing(int amount)
    {
        health += amount;
    }

    public void RestoreMorale(int amount)
    {
        morale += amount;
    }
}
