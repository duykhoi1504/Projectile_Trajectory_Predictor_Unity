using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Enemy : MonoBehaviour
{

    public enum EnemyStat
    {
        Idle,
        Impact
    }
    [SerializeField] EnemyStat enemyStat;



    [SerializeField] float radius = 1f;

    [SerializeField] SpriteRenderer renderer;
    [SerializeField] MaterialPropertyBlock materialPropertyBlock;
    [SerializeField] public bool isTakeDamage = false;
    private void Start()
    {

        enemyStat = EnemyStat.Idle;
        renderer.GetComponent<SpriteRenderer>();
        // materialPropertyBlock = new MaterialPropertyBlock();

    }

    public void ImpactAnim()
    {
        // materialPropertyBlock.SetFloat("_ZoomUvAmount", 1);
        // renderer.SetPropertyBlock(materialPropertyBlock);
        // mate.DOFloat(1,"_ZoomUvAmount",2);
        if (!isTakeDamage) return;
        isTakeDamage = false;
        // renderer.material.DOFloat(1.5f,"_ZoomUvAmount",1f).SetEase(Ease.InBounce).SetLoops(-1);
        renderer.material.DOFloat(-0.08f, "_OffsetUvY", .2f).SetEase(Ease.Unset).OnComplete(() =>
        {
            renderer.material.DOFloat(0, "_OffsetUvY", .2f).SetEase(Ease.Unset);
        });
        renderer.material.DOFloat(1, "_HitEffectBlend", .1f).OnComplete(() =>
        {
            renderer.material.DOFloat(0, "_HitEffectBlend", .1f);

        }).SetLoops(2);

    }
    private void Update()
    {
        ImpactAnim();
    }
    public void TakeDamage()
    {
        Debug.Log("isTakeDamage");
        isTakeDamage = true;
    }
    protected virtual void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);

    }
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("bullet"))
    //     {
    //         enemyStat = EnemyStat.Impact;

    //     }
    // }

}
