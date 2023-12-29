using UnityEngine;

public class Coin : Item
{
    public int value;

    public Coin(int value)
    {
        itemName = "Coins";
        description = "Precious coins, world currency.";
        this.value = value;
    }

    public override void Use()
    {

    }
}
