using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KaisaBullet : bullet
{

    private float noiseY;

    public int seed; // Giá trị seed
    protected override void Start()
    {
        base.Start();
        Random.InitState(seed);
        float rand = Random.Range(-10, 11);

        // Debug.Log(rand + "random");
        noiseY = rand;
    }

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
            float linearT = time / duration;
            float heightT = curve.Evaluate(linearT);
            float height = heightT * heightY * noiseY;

            transform.position = Vector3.Lerp(start, end, linearT) + new Vector3(0, height,0);

            Vector3 direction = (transform.position - previousPoint).normalized;
            transform.up = direction;
        }
        else
        {
            // Destroy(this.gameObject);
            Destruct();
        }
    }

    public override void DrawGizmos(Vector3 start, Vector3 target)
    {

        Random.InitState(seed);
        float noise = Random.Range(-10, 11);
        Vector3 previousPoint = start;
        float timeStep = 0.01f; // Adjust for smoother curves
        for (float t = 0; t <= duration; t += timeStep)
        {
            float linearT = t / duration;
            float heightT = curve.Evaluate(linearT);
            float height = heightY * heightT * noise;

            Vector3 currentPoint = Vector3.Lerp(start, target, linearT) + new Vector3(0, height,0);

            Gizmos.color = Color.yellow; // Change color if needed
            Gizmos.DrawLine(previousPoint, currentPoint);

            previousPoint = currentPoint;
        }

    }

}





