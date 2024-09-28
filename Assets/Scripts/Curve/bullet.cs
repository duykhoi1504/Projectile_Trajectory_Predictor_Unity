using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Mathematics;
using DG.Tweening;

using Random = UnityEngine.Random;
using UnityEngine.Events;
using Unity.VisualScripting;

public enum SkillStat
{
    NormalParabol,
    KaisaParabol,
}
public abstract class bullet : MonoBehaviour
{

    protected Rigidbody2D rig;
    protected Vector2 start, target;
    protected Vector2 startInput, targetInput;
    public AnimationCurve curve;
    public LayerMask layer;

    [SerializeField] public float duration, heightY;
    protected float time;

    // [SerializeField] Vector2 minNoise, maxNoise;
    // [SerializeField] protected SkillStat skillStat;

    public void init(Vector2 start, Vector2 _target)
    {
        startInput = start;
        targetInput = _target;
        // curve = _curve;
        // skillStat = _skillStat;
        // duration = _duration;
        // heightY = _height;
        //fix lỗi ko đúng dir khi mới sinh ra
        // transform.up = (targetInput - startInput).normalized;
    }


    protected virtual void Start()
    {

        time = 0;
        start = startInput;
        target = targetInput;
        // noise = new Vector2(Random.Range(minNoise.x, maxNoise.x), Random.Range(minNoise.y, maxNoise.y));
        // minNoise=new vec
        if (duration <= 0.01f)
        {
            duration = 0.02f;
            // duration = Mathf.Epsilon; // Thiết lập giá trị nhỏ nhất
        }
    }



 
    public virtual void DrawGizmos(Vector2 start, Vector2 target)
    {
        Vector2 previousPoint = start;
        float timeStep = 0.01f; // Adjust for smoother curves
        for (float t = 0; t <= duration; t += timeStep)
        {
            float linearT = t / duration;
            float heightT = curve.Evaluate(linearT);
            float height = heightY * heightT;

            Vector2 currentPoint = Vector2.Lerp(start, target, linearT) + new Vector2(0, height);

            Gizmos.color = Color.green; // Change color if needed
            Gizmos.DrawLine(previousPoint, currentPoint);

            previousPoint = currentPoint;
        }

    }



}
