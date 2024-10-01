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

    private Color color;
    protected Vector3 start, target;
    // protected Vector2 startInput, targetInput;
    [SerializeField] protected AnimationCurve curve;
    // [SerializeField] protected LayerMask layer;
    [SerializeField] public float duration, heightY;
    protected float time;
    protected TrailRenderer trail;
    protected SpriteRenderer sprite;
    protected Collider2D col;

    // [SerializeField] Vector2 minNoise, maxNoise;
    // [SerializeField] protected SkillStat skillStat;


    public void init(Vector3 _start, Vector3 _target)
    {
        start = _start;
        target = _target;
    }


    protected virtual void Start()
    {
   
        col = GetComponent<Collider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        trail = GetComponentInChildren<TrailRenderer>();
        time = 0;
        RandomColorTrail();


    }

    protected virtual void Update()
    {
        if (duration <= 0)
        {
            this.transform.position = target;
        }
        if (!col.enabled) return;

        
        // RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 1.5f, layer);
        // if (hit.collider != null)
        // {

        //     hit.collider.gameObject.GetComponent<Enemy>().TakeDamage();
        //     Debug.Log("25555" + hit.collider.gameObject.name);
        // }
    }

    public virtual void DrawGizmos(Vector3 start, Vector3 target)
    {
        Vector3 previousPoint = start;
        float timeStep = 0.01f; // Adjust for smoother curves
        for (float t = 0; t <= duration; t += timeStep)
        {
            float linearT = t / duration;
            float heightT = curve.Evaluate(linearT);
            float height = heightY * heightT;

            Vector3 currentPoint = Vector3.Lerp(start, target, linearT) + new Vector3(0, height,0);

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
        Debug.Log("BOOM, GET PLAYERIMPACT");
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
