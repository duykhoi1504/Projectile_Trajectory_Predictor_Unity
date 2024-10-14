namespace MCP.Sample
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using MCP.Runtime.MCPMove.LogicMove;

    public class TestTrajectory : MonoBehaviour
    {
        [SerializeField] public Transform target;
        [SerializeField] BulletManager bulletManager;
         [SerializeField] BezierMove _kaisaBullet;
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
                BezierMove kaisaBullet=Instantiate(_kaisaBullet,transform.position,Quaternion.identity);
                kaisaBullet.Init(transform.position,target.position,2,2,(a)=>Destroy(a.gameObject));
            }
        }

    }
}
