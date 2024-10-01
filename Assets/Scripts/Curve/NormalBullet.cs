using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : bullet
{

    protected override void Update()
    {
        base.Update();
        Apply(start, target);
    
       
    }
    public void Apply(Vector3 start, Vector3 end)
    {
        Vector3 previousPoint = transform.position;
        if (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;// 0 to 1
            float heightT = curve.Evaluate(linearT);//value from curve
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector3.Lerp(start, end, linearT) + new Vector3(0, height,0);

            //get dir

            // float nextLinearT = (time + 0.1f) / duration;
            // float nextHeightT = curve.Evaluate(nextLinearT);
            // float nextHeight = Mathf.Lerp(0f, heightY, nextHeightT);

            // Vector2 nextPosition = Vector2.Lerp(start, end, nextLinearT) + new Vector2(0, nextHeight);
            Vector3 direction = (transform.position - previousPoint).normalized;
            transform.up = direction;
            // yield return null;

        }
        else
        {
             Destruct();
        }
    }


    
}
