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
            transform.up =  direction;
            // yield return null;

        }
        else
        {
            Destroy(this.gameObject);
        }
    }


}
