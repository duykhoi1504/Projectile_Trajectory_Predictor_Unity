using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KaisaBullet : bullet
{

    [SerializeField] float noiseY;

    public int seed; // Giá trị seed
    protected override void Start()
    {
        base.Start();
        Random.InitState(seed);
        // Tạo một số nguyên ngẫu nhiên trong khoảng từ 0 đến 10
        float rand = Random.Range(-1, 2);
        Debug.Log(rand+"eandom");
        noiseY = rand;
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
            transform.up = direction;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public override void DrawGizmos(Vector2 start, Vector2 target)
    {

        Random.InitState(seed);
        float  noise= Random.Range(-1, 2);
        Vector2 previousPoint = start;
        float timeStep = 0.01f; // Adjust for smoother curves
        for (float t = 0; t <= duration; t += timeStep)
        {
            float linearT = t / duration;
            float heightT = curve.Evaluate(linearT);
            float height = heightY * heightT *  noise;

            Vector2 currentPoint = Vector2.Lerp(start, target, linearT) + new Vector2(0, height);

            Gizmos.color = Color.yellow; // Change color if needed
            Gizmos.DrawLine(previousPoint, currentPoint);

            previousPoint = currentPoint;
        }

    }

}





