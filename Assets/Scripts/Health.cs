using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    public Health(int value)
    {
        Value = value;
    }

    public int Value { get; private set; }

    public void Reduce(int value)
    {
        if (value < 0)
        {
            Debug.LogError("value < 0");
            return;
        }

        Value -= value;

        if (Value < 0)
        {
            Value = 0;

        }
    }
}
