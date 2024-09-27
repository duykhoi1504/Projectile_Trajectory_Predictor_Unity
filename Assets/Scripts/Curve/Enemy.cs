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
    bool isAnimationTrigger = false;
    [SerializeField] LayerMask layer;
    [SerializeField] float radius = 1f;

    [SerializeField] Renderer renderer;
    [SerializeField] MaterialPropertyBlock materialPropertyBlock;
    private void Start()
    {
        anim = GetComponent<Animator>();
        enemyStat = EnemyStat.Idle;
        renderer.GetComponent<Renderer>();
        materialPropertyBlock = new MaterialPropertyBlock();

    }

    void ImpactAnim(){
                // Đặt giá trị ban đầu
        materialPropertyBlock.SetFloat("_ZoomUvAmount", 1);
        renderer.SetPropertyBlock(materialPropertyBlock);
        // mate.DOFloat(1,"_ZoomUvAmount",2);
    }
    private void Update()
    {


        DetectLayer(layer);
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
    void DetectLayer(LayerMask layer)
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, radius, Vector2.zero, 0f, layer);

        if (hit.collider != null)
        {
            // Destroy(hit.collider.gameObject);
            enemyStat = EnemyStat.Impact;

        }
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
    public void AnimationTrigger()
    {
        isAnimationTrigger = true;
    }
}
