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


    [SerializeField] Transform tartgetPoint, startPoint;
    [SerializeField] bullet Bullet1, Bullet2;


    [SerializeField] SkillStat currentSkillStat;
    // Vector3 mouse;
    Rigidbody2D rig;
    [SerializeField] bool canDraw = true;
    // [SerializeField] int numPJT;
    [SerializeField] int[] seedPJTArray;
    private void Start()
    {
        // seedArray = new int[numPJT];
        // for (int i = 0; i < seedArray.Length; i++)
        // {
        //     seedArray[i] = Random.Range(0, 100);
        // }
        rig = GetComponent<Rigidbody2D>();



    }

    private void Update()
    {
        //get pos of camera for target pos
        // mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // mouse.z = 0;

        //movement
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rig.velocity = movement * moveSpeed;


        //skill
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentSkillStat = SkillStat.NormalParabol;
            StartCoroutine(DelayLine(Bullet1.duration));
            NormalBullet pre = Instantiate(Bullet1, transform.position, transform.rotation) as NormalBullet;
            pre.init(startPoint.position, tartgetPoint.position);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentSkillStat = SkillStat.KaisaParabol;
            StartCoroutine(DelayLine(Bullet2.duration));
            for (int i = 0; i < seedPJTArray.Length; i++)
            {
                KaisaBullet pre = Instantiate(Bullet2, transform.position, transform.rotation) as KaisaBullet;
                pre.init(startPoint.position, tartgetPoint.position);
                pre.seed = seedPJTArray[i];

            }

        }

    }
    IEnumerator DelayLine(float duration)
    {
        canDraw = false;
        yield return new WaitForSeconds(duration);
        canDraw = true;
    }

    private void OnDrawGizmos()
    {
        if (!canDraw) return;
        if (currentSkillStat == SkillStat.NormalParabol)
        {
            Bullet1.DrawGizmos(this.transform.position, tartgetPoint.position);
        }
        if (currentSkillStat == SkillStat.KaisaParabol)
        {
            for (int i = 0; i < seedPJTArray.Length; i++)
            {
                Bullet2.GetComponent<KaisaBullet>().DrawGizmos(this.transform.position, tartgetPoint.position);
                Bullet2.GetComponent<KaisaBullet>().seed = seedPJTArray[i];
            }

        }
    }


}
