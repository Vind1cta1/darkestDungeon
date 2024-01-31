using UnityEngine;

public class Coin: MonoBehaviour
{
    private int value;

    public void SetQuantityOfMoney(int value)
    {
        this.value = value;
        Debug.Log(value);
    }

    public int GetMoneyAmount()
    {
        return value;
    }
}
