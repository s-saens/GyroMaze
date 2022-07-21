using UnityEngine;
using System.Collections;

public static class IEnumeratorExt
{
    public static IEnumerator Concat(this IEnumerator first, params IEnumerator[] ienums)
    {
        yield return first;
        for(int i=0 ; i<ienums.Length ; ++i) yield return ienums[i];
    }
}