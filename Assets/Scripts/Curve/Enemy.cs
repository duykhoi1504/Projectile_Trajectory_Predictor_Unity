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
    [SerializeField] Animator anim;

    [SerializeField] LayerMask layer;
    [SerializeField] float radius = 1f;

    [SerializeField] Renderer renderer;
    [SerializeField] MaterialPropertyBlock materialPropertyBlock;
    private void Start()
    {
        anim = GetComponent<Animator>();
        enemyStat = EnemyStat.Idle;
        renderer.GetComponent<SpriteRenderer>();
        // materialPropertyBlock = new MaterialPropertyBlock();

    }

    void ImpactAnim()
    {
        // materialPropertyBlock.SetFloat("_ZoomUvAmount", 1);
        // renderer.SetPropertyBlock(materialPropertyBlock);
        // mate.DOFloat(1,"_ZoomUvAmount",2);
     
        // renderer.material.DOFloat(1.5f,"_ZoomUvAmount",1f).SetEase(Ease.InBounce).SetLoops(-1);
        renderer.material.DOFloat(-0.08f, "_OffsetUvY", .2f).SetEase(Ease.Unset).OnComplete(() =>
        {
            renderer.material.DOFloat(0, "_OffsetUvY", .2f).SetEase(Ease.Unset);
        });
        renderer.material.DOFloat(1, "_HitEffectBlend", .1f).OnComplete(() =>
        {
            renderer.material.DOFloat(0, "_HitEffectBlend", .1f);

        }).SetLoops(2);
        // transform.DOShakeScale(.5f, 1f, 10, 90f, true);

    }
    private void Update()
    {


        DetectLayer();
        switch (enemyStat)
        {
            case EnemyStat.Idle:
                break;
            case EnemyStat.Impact:
                ImpactAnim();
                enemyStat = EnemyStat.Idle;
                break;
            default:
                break;
        }

    }
    void DetectLayer()
    {
        // RaycastHit2D hit = Physics2D.CircleCast(transform.position, radius, Vector2.zero, 0f, layer);
        //cach 1 dung collider
        Collider2D[] collider2D = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D collider in collider2D)
        {
            if (collider.GetComponent<IDamageable>() != null)
            {

                collider.gameObject.SetActive(false);
                collider.GetComponent<IDamageable>().TakeDamage();
                TakeDamage();
            }
        }


        //cach 2 khong dung collider
        bullet[] bullets=FindObjectsOfType<bullet>();
        foreach(bullet bullet in bullets){
            float distance=Vector2.Distance(bullet.transform.position, transform.position);
            if(distance<=radius){
                 Destroy(bullet.gameObject);
        //         collider.GetComponent<IDamageable>().TakeDamage();
                TakeDamage();
            }
        }
    }
    public void TakeDamage()
    {
        enemyStat = EnemyStat.Impact;
    }
    protected virtual void OnDrawGizmos()
    {
        // Vẽ một đường tròn với bán kính 2 tại vị trí của game object
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
