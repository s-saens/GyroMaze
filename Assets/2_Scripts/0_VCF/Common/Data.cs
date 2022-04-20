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

    public Data() {}
    public Data(T val)
    {
        this.v = val;
    }
}