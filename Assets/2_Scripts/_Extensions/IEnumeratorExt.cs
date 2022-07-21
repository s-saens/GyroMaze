using UnityEngine;
using System;
using System.Collections;

public static class IEnumeratorExt
{
    public static IEnumerator Concat(this IEnumerator first, params IEnumerator[] ienums)
    {
        yield return first;
        for(int i=0 ; i<ienums.Length ; ++i) yield return ienums[i];
    }
    public static IEnumerator Then(this IEnumerator ienum, Action callback)
    {
        yield return ienum;
        callback?.Invoke();
    }
}