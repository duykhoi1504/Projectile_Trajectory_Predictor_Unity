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
         [SerializeField] BezierMove _bezierMove;

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
//////////////////////////////////////////////////////////////////////
            if (Input.GetKeyDown(KeyCode.T))
            {
                NormalMove kaisaBullet=Instantiate(_kaisaBullet,transform.position,Quaternion.identity);
                kaisaBullet.Init(transform.position,target.position,2,(a)=>Destroy(a.gameObject));
        
            }
              if (Input.GetKeyDown(KeyCode.Y))
            {
                BezierMove bezierMove=Instantiate(_bezierMove,transform.position,Quaternion.identity);
                bezierMove.Init(transform.position,target.position,2,(a)=>Destroy(a.gameObject));
        
            }
            
        }


    }
}
