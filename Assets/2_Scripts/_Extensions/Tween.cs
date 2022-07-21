using UnityEngine;
using System;
using System.Collections;

public static class Tween
{
    public static IEnumerator AddForceTo(this Rigidbody2D r, Transform destPosition, int strength = 1)
    {
        yield return r.AddForceTo((Vector2)r.position, strength);
    }
    public static IEnumerator AddForceTo(this Rigidbody2D r, Vector2 destPosition, int strength = 1)
    {
        Vector2 dir = r.position - destPosition;
        while(dir.magnitude < 0.001f)
        {
            r.AddForce(dir.normalized * strength);

            if(Vector2.Dot(destPosition - (r.position + r.velocity), destPosition - r.position) < 0) break; // 지나가버릴 연산이라면 break하기.

            dir = r.position - destPosition;
            yield return new WaitForFixedUpdate();
        }
        r.position = destPosition;
        yield return 0;
    }

    public static IEnumerator ScaleTo(this Transform t, Vector2 destScale, float time = 0.4f)
    {
        while((destScale - (Vector2)t.localScale).magnitude < 0.001f)
        {
            Vector2.Lerp(t.localScale, destScale, time);
            yield return new WaitForFixedUpdate();
        }
        t.localScale = destScale;
        yield return 0;
    }

    public static IEnumerator OrthographicSizeTo(this Camera cam, int destination, float time = 0.4f)
    {
        while(MathF.Abs(cam.orthographicSize - destination) < 0.001f)
        {
            Mathf.Lerp(cam.orthographicSize, destination, time);
            yield return new WaitForFixedUpdate();
        }
        cam.orthographicSize = destination;
        yield return 0;
    }

    public static IEnumerator Then(this IEnumerator ienum, Action callback)
    {
        yield return ienum;
        callback?.Invoke();
    }
}