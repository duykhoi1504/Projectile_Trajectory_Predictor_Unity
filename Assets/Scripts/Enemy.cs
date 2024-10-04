using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Trajectory.PJT;
public class Enemy : MonoBehaviour
{
    // [SerializeField] private float radius = 1f;
    [SerializeField] private bool isTakeDamage = false;
    private AnimMaterial enemyAnim;

    private void Start()
    {
        enemyAnim = GetComponentInChildren<AnimMaterial>();
    }
    private void Update()
    {
        if (!isTakeDamage) return;
        isTakeDamage = false;
        if (enemyAnim == null) return;
        enemyAnim.ImpactAnim();
    }

    public void TakeDamage()
    {
        Debug.Log("isTakeDamage");
        isTakeDamage = true;
    }
    // protected virtual void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(transform.position, radius);
    // }

}
