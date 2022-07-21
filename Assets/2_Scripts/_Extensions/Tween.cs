using UnityEngine;
using System;
using System.Collections;

public static class Tween
{
    public static IEnumerator AddForceTo(this Rigidbody2D r, Vector2 destPosition, float strength = 1)
    {
        Vector2 dir = destPosition - r.position;
        r.drag = 15;
        r.gravityScale = 0;
        while(dir.magnitude > 0.001f)
        {
            r.AddForce((dir) * strength);

            dir = destPosition - r.position;
            yield return new WaitForFixedUpdate();
        }
        r.position = destPosition;
        r.velocity = Vector2.zero;
        yield return 0;
    }

    public static IEnumerator ScaleTo(this Transform t, Vector2 destScale, float percentage = 0.1f)
    {
        while((destScale - (Vector2)t.localScale).magnitude > 0.001f)
        {
            t.localScale = Vector2.Lerp(t.localScale, destScale, percentage);
            yield return new WaitForFixedUpdate();
        }
        t.localScale = destScale;
        yield return 0;
    }

    public static IEnumerator OrthographicSizeTo(this Camera cam, float destination, float percentage = 0.1f)
    {
        while(MathF.Abs(cam.orthographicSize - destination) > 0.001f)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, destination, percentage);
            yield return new WaitForFixedUpdate();
        }
        cam.orthographicSize = destination;
        yield return 0;
    }
}