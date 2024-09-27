using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : bullet
{
    private void Update()
    {
        Apply(start, target);
    }
    public void Apply(Vector2 start, Vector2 end)
    {
        Vector2 previousPoint = transform.position;
        if (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;// 0 to 1
            float heightT = curve.Evaluate(linearT);//value from curve
            float height = Mathf.Lerp(0f, heightY, heightT);
            // float v=Vector2.Distance(start, end)/duration;

            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0, height);
            //get dir

            // float nextLinearT = (time + 0.1f) / duration;
            // float nextHeightT = curve.Evaluate(nextLinearT);
            // float nextHeight = Mathf.Lerp(0f, heightY, nextHeightT);

            // Vector2 nextPosition = Vector2.Lerp(start, end, nextLinearT) + new Vector2(0, nextHeight);


            Vector2 direction = ((Vector2)transform.position - previousPoint).normalized;
            transform.up = Vector2.Lerp(transform.up, direction, 0.1f);
            // yield return null;

        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    //  private void OnDrawGizmos() {
    //     Vector2 previousPoint = transform.position;
    //     float timeStep = 0.01f; // Adjust for smoother curves
    //     for (float t = 0; t <= duration; t += timeStep)
    //     {
    //         // float t = i * config;
    //         float linearT = t / duration;
    //         float heightT = curve.Evaluate(linearT);
    //         float height = Mathf.Lerp(0f, heightY, heightT);

    //         Vector2 currentPoint = Vector2.Lerp(start, target, linearT) + new Vector2(0, height);

    //         Gizmos.color = Color.red;
    //         Gizmos.DrawLine(previousPoint, currentPoint);

    //         previousPoint = currentPoint;
    //     }
    // }

}
