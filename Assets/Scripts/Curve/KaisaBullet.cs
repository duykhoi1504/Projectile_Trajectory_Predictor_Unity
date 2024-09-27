using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KaisaBullet : bullet
{

    protected float noiseY;
    protected override void Start()
    {
        base.Start();
          noiseY = Random.Range(-heightY, heightY);
    }
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
            float linearT = time / duration;
            float heightT = curve.Evaluate(linearT);
            float heightNoise = heightT * heightY * noiseY;

            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0, heightNoise);

            Vector2 direction = ((Vector2)transform.position - previousPoint).normalized;
            transform.up = Vector2.Lerp(transform.up, direction, 0.5f);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }



}
