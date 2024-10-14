namespace MCP.Sample
{
using UnityEngine;
using MCP.Runtime;
public class TestAnimationMaterial : MonoBehaviour
{
    // [SerializeField] private float radius = 1f;
    [SerializeField] private bool isImpact = false;
     [SerializeField] private AnimMaterial enemyAnim;

    private void Start()
    {
        enemyAnim = GetComponent<AnimMaterial>();
    }
    private void Update()
    {
         if (isImpact)
            {
                isImpact = false;
                enemyAnim.ImpactAnim();
            }
    }


}
}