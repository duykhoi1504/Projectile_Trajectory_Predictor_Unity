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
public class bullet : MonoBehaviour
{
    // public float speed = 10f;
    // public float lifetime = 2f;
    // public float height = 2f; // Height of the curve
    // private float timeAlive = 0f;
    Rigidbody2D rig;
    [SerializeField] private Vector2 start, target;
    [SerializeField] private Vector2 startInput, targetInput;
    // private Vector2 targetPosition;
    AnimationCurve curve;
    // [SerializeField] AnimationCurve curveSpeed;

    private float duration, time, heightY;
    [SerializeField] Vector2 minNoise, maxNoise;
    private Vector2 noise;
    [SerializeField] SkillStat skillStat;


    private void Awake()
    {

    }
    private void Start()
    {

        time = 0;
        start = startInput;
        target = targetInput;
        rig = GetComponent<Rigidbody2D>();
        noise = new Vector2(Random.Range(minNoise.x, maxNoise.x), Random.Range(minNoise.y, maxNoise.y));
        // Invoke("destruct", duration);


        if (duration <= 0.01f)
        {
            duration = 0.01f;
            // duration = Mathf.Epsilon; // Thiết lập giá trị nhỏ nhất
        }


    }

    void destruct()
    {
        Destroy(this.gameObject);
    }
    private void Update()
    {


        if (skillStat == SkillStat.NormalParabol)
        {

            // transform.DOLocalPath(path,duration,pathType).SetEase(easeType);


            NormalParabol(start, target);
            // StartCoroutine(NormalParabol(startPosition.position, target.position));
            // transform.DOJump(target.position, heightY, 1, duration)
            //       .SetEase(easeType);

        }
        else if (skillStat == SkillStat.KaisaParabol)
        {
            KaisaParabol(start, target);
        }

    }
    public void init(Vector2 start, Vector2 _target, AnimationCurve _curve, SkillStat _skillStat, float _duration, float _height)
    {
        startInput = start;
        targetInput = _target;
        curve = _curve;
        skillStat = _skillStat;
        duration = _duration;
        heightY = _height;
        //fix lỗi ko đúng dir khi mới sinh ra
        transform.up = (targetInput - startInput).normalized;
    }



    public void KaisaParabol(Vector2 start, Vector2 end)
    {

        Vector2 previousPoint = transform.position;

        if (time < duration)
        {
            time += Time.deltaTime;

            // yield return null;


            //v=s/t

            float linearT = time / duration;// 0 to 1
            float heightT = curve.Evaluate(linearT);
            // float height = Mathf.Lerp(0, heightY, heightT);

            //chiều cao theo HeightY
            float height = heightT * heightY;

            //chiều cao noise để giống với kaisa
            float heightNoise = height * noise.y;

            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0, heightNoise);


            //get dir

            // float nextLinearT = (time + 0.1f) / duration;
            // float nextHeightT = curve.Evaluate(nextLinearT);
            // // float nextHeight = Mathf.Lerp(0, heightY, nextHeightT);
            // float nextHeight = nextHeightT * heightY;
            // float nextHeightNoise = nextHeight * noise.y;

            // Vector2 nextPosition = Vector2.Lerp(start, end, nextLinearT) + new Vector2(0, nextHeightNoise);
            // Vector2 direction = (nextPosition - (Vector2)transform.position).normalized;
            Vector2 direction = ((Vector2)transform.position - previousPoint).normalized;

            transform.up = Vector2.Lerp(transform.up, direction, 0.5f);

        }
    }

    public void NormalParabol(Vector2 start, Vector2 end)
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

            //   transform.DOJump(target.position, heightY, 1, duration)
            //       .SetEase(easeType);




            //get dir

            // float nextLinearT = (time + 0.1f) / duration;
            // float nextHeightT = curve.Evaluate(nextLinearT);
            // float nextHeight = Mathf.Lerp(0f, heightY, nextHeightT);

            // Vector2 nextPosition = Vector2.Lerp(start, end, nextLinearT) + new Vector2(0, nextHeight);


            Vector2 direction = ((Vector2)transform.position - previousPoint).normalized;
            transform.up = Vector2.Lerp(transform.up, direction, 0.1f);
            // yield return null;

        }


        // gameObject.transform.position = end + new Vector2(0, heightY);
    }
}
