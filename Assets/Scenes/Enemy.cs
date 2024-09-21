using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public enum EnemyStat
    {
        Idle,
        Impact
    }
    [SerializeField] EnemyStat enemyStat;
    [SerializeField] Animator anim;
    bool isAnimationTrigger = false;
    private void Start()

    {
        anim = GetComponent<Animator>();
        enemyStat = EnemyStat.Idle;
    }
    private void Update()
    {

        switch (enemyStat)
        {
            case EnemyStat.Idle:
                anim.SetBool("Idle", true);
                anim.SetBool("Impact", false);

                break;
            case EnemyStat.Impact:
                anim.SetBool("Idle", false);
                anim.SetBool("Impact", true);
                if (isAnimationTrigger)
                {
                    isAnimationTrigger = false;
                    enemyStat = EnemyStat.Idle;
                }
                break;
            default:
                break;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            enemyStat = EnemyStat.Impact;
            Debug.Log("tim that dan");
        }
    }
    public void AnimationTrigger()
    {
        isAnimationTrigger = true;
    }
}
