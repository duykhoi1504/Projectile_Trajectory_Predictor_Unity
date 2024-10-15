namespace MCPMovement.Sample
{
using UnityEngine;
using MCPMovement.Runtime;
public class TestAnimationMaterial : MonoBehaviour
{
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