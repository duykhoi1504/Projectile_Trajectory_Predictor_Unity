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

    Color color;
    protected Vector2 start, target;
    protected Vector2 startInput, targetInput;
    [SerializeField] protected AnimationCurve curve;
    [SerializeField] protected LayerMask layer;

    [SerializeField] public float duration, heightY;
    protected float time;
    protected TrailRenderer trail;
    protected SpriteRenderer sprite;
    protected Collider2D col;

    // [SerializeField] Vector2 minNoise, maxNoise;
    // [SerializeField] protected SkillStat skillStat;

    public void init(Vector2 _start, Vector2 _target)
    {
        startInput = _start;
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
        col = GetComponent<Collider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        trail = GetComponentInChildren<TrailRenderer>();
        time = 0;
        start = startInput;
        target = targetInput;
        RandomColorTrail();
        // noise = new Vector2(Random.Range(minNoise.x, maxNoise.x), Random.Range(minNoise.y, maxNoise.y));
        // minNoise=new vec

    }

    protected virtual void Update()
    {
        if (duration <= 0)
        {
            this.transform.position = target;
        }
        if (!col.enabled) return;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 1.5f, layer);
        if (hit.collider != null)
        {
            hit.collider.gameObject.GetComponent<Enemy>().TakeDamage();
            Debug.Log("25555" + hit.collider.gameObject.name);
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

    protected void RandomColorTrail()
    {
        color = Random.ColorHSV();
        trail.startColor = color;
        trail.endColor = color;
    }
    protected void Destruct()
    {
        StartCoroutine(IEDestruct());
    }
    IEnumerator IEDestruct()
    {
        sprite.enabled = false;
        col.enabled = false;
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }
}
