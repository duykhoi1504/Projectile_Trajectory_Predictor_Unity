using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Mathematics;

using Random = UnityEngine.Random;
using UnityEngine.Events;
public enum SkillStat
{
    NormalParabol,
    KaisaForm,
}
public class bullet : MonoBehaviour
{
    // public float speed = 10f;
    // public float lifetime = 2f;
    // public float height = 2f; // Height of the curve
    // private float timeAlive = 0f;
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform target;
    // private Vector2 targetPosition;
    Rigidbody2D rig;
    [SerializeField] AnimationCurve curve;
    [SerializeField] float duration, time, heightY;
    [SerializeField] Vector2 minNoise, maxNoise;
    Vector2 noise;
    [SerializeField] SkillStat skillStat;
    private void Start()
    {
        time = 0;
        rig = GetComponent<Rigidbody2D>();
        noise = new Vector2(Random.Range(minNoise.x, maxNoise.x), Random.Range(minNoise.y, maxNoise.y));
        // StartCoroutine(Spawn(startPosition.position, target.position));
       
        if (skillStat == SkillStat.NormalParabol)
        {
            StartCoroutine(NormalParabol(startPosition.position, target.position));
        }
        else if (skillStat == SkillStat.KaisaForm)
        {
            StartCoroutine(KaisaForm(startPosition.position, target.position));

        }
    }
    public void init(Transform start, Transform _target, AnimationCurve _curve, SkillStat _skillStat,float _duration,float _height)
    {
        startPosition = start;
        target = _target;
        curve = _curve;
        skillStat = _skillStat;
        duration=_duration;
        heightY = _height;
    }



    public IEnumerator KaisaForm(Vector2 startPosition, Vector2 target)
    {

        while (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;// 0 to 1
            float heightT = curve.Evaluate(linearT);
            // float height = Mathf.Lerp(0, heightY, heightT);
             float height=heightT*heightY;
            float heightNoise = height * noise.y;
            transform.position = Vector2.Lerp(startPosition, target, linearT) + new Vector2(0, heightNoise);

            //get dir

            float nextLinearT = (time + 0.1f) / duration;
            float nextHeightT = curve.Evaluate(nextLinearT);
            // float nextHeight = Mathf.Lerp(0, heightY, nextHeightT);
            float nextHeight=nextHeightT*heightY;
            float nextHeightNoise = nextHeight * noise.y;

            Vector2 nextPosition = Vector2.Lerp(startPosition, target, nextLinearT) + new Vector2(0, nextHeightNoise);
            Vector2 direction = (nextPosition - (Vector2)transform.position).normalized;
            transform.up = direction;
            yield return null;

        }
    }
    public IEnumerator NormalParabol(Vector2 start, Vector2 end)
    {

        while (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;// 0 to 1
            float heightT = curve.Evaluate(linearT);//value from curve
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0, height);

            float nextLinearT = (time + 0.1f) / duration;
            float nextHeightT = curve.Evaluate(nextLinearT);
            float nextHeight = Mathf.Lerp(0f, heightY, nextHeightT);


            Vector2 nextPosition = Vector2.Lerp(start, end, nextLinearT) + new Vector2(0, nextHeight);
            Vector2 direction = (nextPosition - (Vector2)transform.position).normalized;
            transform.up = direction;
            yield return null;

        }


        // gameObject.transform.position = end + new Vector2(0, heightY);
    }

    void FlyWithRotate()
    {
        // float time = 0;
        transform.position = Vector2.Lerp(startPosition.position, target.position, duration);
        // Vector2 newDir = new Vector2(Random.Range(0, 1), Random.Range(0, 1)).normalized;
        // transform.up = newDir;
        float inDuration = duration / 3;
        float timeDuration = inDuration;

        // while (time < duration)
        // {
        //     time += Time.deltaTime;
        //     if (time > timeDuration)
        //     {

        //         Quaternion newAngle = new Quaternion();
        //         newAngle.eulerAngles = new Vector3(0, 0, Random.Range(0, 30));
        //         this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, newAngle, timeDuration);
        //         timeDuration += inDuration;

        //     }
        // }
    }

}
// private void OnTriggerEnter2D(Collider2D other)
// {
//     if (other.gameObject.CompareTag("Enemy"))
//     {
//         Debug.Log(other.name);
//         // Handle enemy hit
//         Destroy(gameObject); // Destroy the projectile
//     }
//     else
//     {
//         Debug.Log("khong tim thay");
//     }
// }
// }