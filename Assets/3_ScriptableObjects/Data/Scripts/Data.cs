using UnityEngine;
using System;

public class Data<T>
{
    private T v;
    public T value
    {
        get
        {
            return this.v;
        }
        set
        {
            this.v = value;
            this.onChange?.Invoke(value);
        }
    }
    public Action<T> onChange;
}