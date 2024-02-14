using UnityEngine;

public class Coin: MonoBehaviour
{
    private int value;

    public void SetQuantityOfMoney(int value)
    {
        this.value = value;
    }

    public int GetMoneyAmount()
    {
        return value;
    }
}
