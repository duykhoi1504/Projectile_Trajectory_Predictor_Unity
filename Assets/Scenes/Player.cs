using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    [Header(" PLayer config")]
    [SerializeField] float moveSpeed = 5f;

    
    [SerializeField] Transform TartgetPoint, startPoint;
    [SerializeField] bullet Bullet;
    // [SerializeField] GameObject bullet0;

    // [SerializeField] int angle = 0;
    // [SerializeField] float angle_rad = 0;
    // [SerializeField] int trajectory = 100;
    // [SerializeField] float Vo;
    // [SerializeField] float config;
    /// <summary>
    /// //////////////////////////
    /// 

    [Header(" curveParapol")]
    [SerializeField] AnimationCurve curveParabol;

    /// </summary>
    [Header("Kaisa Q by curve")]
    [SerializeField] AnimationCurve curve;

    [SerializeField] float duration;
    [SerializeField] float heightY;
    [SerializeField] float time;
    [SerializeField] float numPJT;

    Rigidbody2D rig;

    private void Start()
    {

        rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rig.velocity = movement * moveSpeed;
        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     GameObject pre = Instantiate(bullet0, transform.position, Quaternion.identity);
        //     Vector2 Force = new Vector2(
        //         Vo * 50 * Mathf.Cos(angle_rad),
        //         Vo * 50 * Mathf.Sin(angle_rad)
        //     );

        //     pre.GetComponent<Rigidbody2D>().AddForce(Force);
        // }
        if (Input.GetKeyDown(KeyCode.E))
        {
            bullet pre = Instantiate(Bullet, transform.position, Quaternion.identity);
            // StartCoroutine(pre.SpawnParabol(startPoint.transform.position, TartgetPoint.transform.position, pre.gameObject));
            pre.init(startPoint, TartgetPoint, curveParabol, SkillStat.NormalParabol, duration, heightY);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(SpawnBullets());

        }
    }
    //q kaisa
    private IEnumerator SpawnBullets()
    {
        for (int i = 0; i < numPJT; i++)
        {
            bullet pre = Instantiate(Bullet, transform.position, Quaternion.identity);
            pre.init(startPoint, TartgetPoint, curve, SkillStat.KaisaForm, duration, heightY);

            yield return new WaitForSeconds(0.1f); // Chờ 0.1 giây trước khi sinh viên đạn tiếp theo
        }
    }

    // ///DÙng Vật lý
    // void CalV()
    // {
    //     float y = TartgetPoint.transform.position.y - startPoint.transform.position.y;
    //     float x = TartgetPoint.transform.position.x - startPoint.transform.position.x;
    //     float v2 = ((10 * x * x) / (-y + Mathf.Tan(angle_rad) * x)) / (2 * Mathf.Pow(Mathf.Cos(angle_rad), 2));
    //     v2 = Mathf.Abs(v2);
    //     Vo = Mathf.Sqrt(v2);
    // }
    // private void OnDrawGizmosSelected()
    // {

    //     angle_rad = angle * Mathf.Deg2Rad;
    //     CalV();
    //     Gizmos.color = Color.green;
    //     for (int i = 0; i < trajectory; i++)
    //     {
    //         float time = i * config;
    //         float x = Vo * Mathf.Cos(angle_rad) * time;
    //         float y = Vo * Mathf.Sin(angle_rad) * time - 0.5f * (10 * time * time);
    //         Vector3 post1 = startPoint.position + new Vector3(x, y, 0);
    //         time = (i + 1) * config;
    //         x = Vo * Mathf.Cos(angle_rad) * time;
    //         y = Vo * Mathf.Sin(angle_rad) * time - 0.5f * (10 * time * time);
    //         Vector3 post2 = startPoint.position + new Vector3(x, y, 0);

    //         Gizmos.DrawLine(post1, post2);

    //     }

    // }

}
