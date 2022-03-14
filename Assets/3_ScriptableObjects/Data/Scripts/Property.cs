using UnityEngine;
using System;

public class Property<T>
{
    private T Value;
    public T value
    {
        get
        {
            return this.Value;
        }
        set
        {
            this.Value = value;
            this.onChange?.Invoke(value);
        }
    }
    public Action<T> onChange;
}