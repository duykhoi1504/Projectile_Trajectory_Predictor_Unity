namespace MCPMovement.Sample
{

    using UnityEngine;
    using MCPMovement.Runtime.MCPMove.LogicMove;
    using System.Collections;

    public class TestTrajectory : MonoBehaviour
    {
        [SerializeField] public Transform target;
        [SerializeField] BulletManager bulletManager;
         [SerializeField] NormalMove _kaisaBullet;
        private void Start()
        {
            bulletManager = GetComponent<BulletManager>();
        }
        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                bulletManager.SpawmBullet(transform.position, target.position, MoveType.NormalMove);
                
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                bulletManager.SpawmBullet(transform.position, target.position, MoveType.BezierMove);
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                NormalMove kaisaBullet=Instantiate(_kaisaBullet,transform.position,Quaternion.identity);
                kaisaBullet.Init(transform.position,target.position,4,(a)=>Destroy(a.gameObject));
                // StartCoroutine(wait(kaisaBullet));
            }
            
        }

        // IEnumerator wait(NormalMove kaisaBullet){
        //     yield return new WaitForSeconds(2f);
        //     kaisaBullet.SetActiceHit(true);
        //     yield return new WaitForSeconds(1f);
        //     kaisaBullet.SetActiceHit(false);
        // }
    }
}
