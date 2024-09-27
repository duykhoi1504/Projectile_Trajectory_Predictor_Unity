using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Mathematics;
using Unity.VisualScripting;
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
    // [SerializeField] float time;
    [SerializeField] float numPJT;
Vector3 mouse;
    Rigidbody2D rig;

    private void Start()
    {

        rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {




         mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rig.velocity = movement * moveSpeed;

        if (Input.GetKeyDown(KeyCode.E))
        {
            bullet pre = Instantiate(Bullet, transform.position, Quaternion.identity);
            // StartCoroutine(pre.SpawnParabol(startPoint.transform.position, TartgetPoint.transform.position, pre.gameObject));
            pre.init(startPoint.position, mouse, curveParabol, SkillStat.NormalParabol, duration, heightY);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(SpawnBullets(mouse));

        }
    }
    //q kaisa
    private IEnumerator SpawnBullets(Vector3 mouse)
    {
        for (int i = 0; i < numPJT; i++)
        {
            bullet pre = Instantiate(Bullet, transform.position, Quaternion.identity);
            pre.init(startPoint.position, mouse, curve, SkillStat.KaisaParabol, duration, heightY);

            yield return new WaitForSeconds(0.1f); // Chờ 0.1 giây trước khi sinh viên đạn tiếp theo
        }
    }
    // [SerializeField] float config;
    private void OnDrawGizmos()
    {

        Vector2 previousPoint = transform.position;
        float timeStep = 0.01f; // Adjust for smoother curves
        for (float t = 0; t <= duration; t += timeStep)
        {
            // float t = i * config;
            float linearT = t / duration;
            float heightT = curveParabol.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            Vector2 currentPoint = Vector2.Lerp(transform.position, mouse, linearT) + new Vector2(0, height);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(previousPoint, currentPoint);

            previousPoint = currentPoint;
        }
    }




    //     Vector2 start = transform.position; // Vị trí bắt đầu
    //     Vector2 end = start + new Vector2(5, 0); // Ví dụ, có thể thay đổi tùy theo yêu cầu


    //     float trajectory = 100;
    //     Gizmos.color = Color.green;
    //     for (int i = 0; i < trajectory - 1; i++)
    // {
    //     float linearT1 = (float)i / (trajectory - 1);
    //     float heightT1 = curveParabol.Evaluate(linearT1);
    //     // float height1 = Mathf.Lerp(0f, heightY, heightT1);
    //      float height1 = heightT1 * heightY;
    //     Vector2 position1 = Vector2.Lerp(start, end, linearT1) + new Vector2(0, height1);

    //     float linearT2 = (float)(i + 1) / (trajectory - 1);
    //     float heightT2 = curveParabol.Evaluate(linearT2);
    //     // float height2 = Mathf.Lerp(0f, heightY, heightT2);
    //      float height2 = heightT2 * heightY;

    //     Vector2 position2 = Vector2.Lerp(start, end, linearT2) + new Vector2(0, height2);

    //     Gizmos.DrawLine(position1, position2); // Vẽ đường nối giữa các điểm
    // }
    // }

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
