using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField]
    private float value;

    public float GetValue()
    {
        return value;
    }

    public void Increase(float v)
    {
        value += v;
    }

    public void Decrease(float v)
    {
        value -= v;
    }
}
