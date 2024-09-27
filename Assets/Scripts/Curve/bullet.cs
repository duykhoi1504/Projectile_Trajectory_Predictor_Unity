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
public abstract class bullet : MonoBehaviour, IDamageable
{

    protected Rigidbody2D rig;
    protected Vector2 start, target;
    protected Vector2 startInput, targetInput;
    // private Vector2 targetPosition;
    public AnimationCurve curve;
    // [SerializeField] AnimationCurve curveSpeed;

    [SerializeField] public float duration, heightY;
    protected float time;

    // [SerializeField] Vector2 minNoise, maxNoise;
    [SerializeField] protected SkillStat skillStat;

    public void init(Vector2 start, Vector2 _target, AnimationCurve _curve, SkillStat _skillStat)
    {
        startInput = start;
        targetInput = _target;
        curve = _curve;
        skillStat = _skillStat;
        // duration = _duration;
        // heightY = _height;
        //fix lỗi ko đúng dir khi mới sinh ra
        transform.up = (targetInput - startInput).normalized;
    }

    protected virtual void Start()
    {

        time = 0;
        start = startInput;
        target = targetInput;
        rig = GetComponent<Rigidbody2D>();

        // noise = new Vector2(Random.Range(minNoise.x, maxNoise.x), Random.Range(minNoise.y, maxNoise.y));
        // minNoise=new vec


        if (duration <= 0.01f)
        {
            duration = 0.01f;
            // duration = Mathf.Epsilon; // Thiết lập giá trị nhỏ nhất
        }



    }
    //  public void OnDrawGizmos() {
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


    // private void Update()
    // {


    //     if (skillStat == SkillStat.NormalParabol)
    //     {

    //         // transform.DOLocalPath(path,duration,pathType).SetEase(easeType);


    //         NormalParabol(start, target);
    //         // StartCoroutine(NormalParabol(startPosition.position, target.position));
    //         // transform.DOJump(target.position, heightY, 1, duration)
    //         //       .SetEase(easeType);

    //     }
    //     else if (skillStat == SkillStat.KaisaParabol)
    //     {
    //         KaisaParabol(start, target);
    //     }


    // }



    // public void KaisaParabol(Vector2 start, Vector2 end)
    // {

    //     Vector2 previousPoint = transform.position;

    //     if (time < duration)
    //     {
    //         time += Time.deltaTime;



    //         float linearT = time / duration;// 0 to 1
    //         float heightT = curve.Evaluate(linearT);
    //         // float height = Mathf.Lerp(0, heightY, heightT);

    //         //chiều cao theo HeightY
    //         float height = heightT * heightY;

    //         //chiều cao noise để giống với kaisa
    //         float heightNoise = height * noiseY;

    //         transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0, heightNoise);


    //         //get dir
    //         Vector2 direction = ((Vector2)transform.position - previousPoint).normalized;

    //         transform.up = Vector2.Lerp(transform.up, direction, 0.5f);

    //     }else{
    //             Destroy(this.gameObject);
    //     }
    // }

    // public void NormalParabol(Vector2 start, Vector2 end)
    // {

    //     Vector2 previousPoint = transform.position;
    //     if (time < duration)
    //     {
    //         time += Time.deltaTime;
    //         float linearT = time / duration;// 0 to 1
    //         float heightT = curve.Evaluate(linearT);//value from curve
    //         float height = Mathf.Lerp(0f, heightY, heightT);
    //         // float v=Vector2.Distance(start, end)/duration;

    //         transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0, height);
    //         //get dir

    //         // float nextLinearT = (time + 0.1f) / duration;
    //         // float nextHeightT = curve.Evaluate(nextLinearT);
    //         // float nextHeight = Mathf.Lerp(0f, heightY, nextHeightT);

    //         // Vector2 nextPosition = Vector2.Lerp(start, end, nextLinearT) + new Vector2(0, nextHeight);


    //         Vector2 direction = ((Vector2)transform.position - previousPoint).normalized;
    //         transform.up = Vector2.Lerp(transform.up, direction, 0.1f);
    //         // yield return null;

    //     }else{
    //             Destroy(this.gameObject);
    //     }


    //     // gameObject.transform.position = end + new Vector2(0, heightY);
    // }

    public void TakeDamage()
    {
        Debug.Log("damage dan");
    }


}
