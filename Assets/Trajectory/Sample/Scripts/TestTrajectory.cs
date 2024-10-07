namespace Trajectory.Sample
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Trajectory.Runtime;
    public class TestTrajectory : MonoBehaviour
    {
        [SerializeField] public Transform target;
        [SerializeField] BulletManager bulletManager;
         [SerializeField] KaisaBullet _kaisaBullet;
        private void Start()
        {
            bulletManager = GetComponent<BulletManager>();
        }
        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                bulletManager.SpawmBullet(transform.position, target.position, BulletType.NormalBullet);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                bulletManager.SpawmBullet(transform.position, target.position, BulletType.KaisaBullet);
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                KaisaBullet kaisaBullet=Instantiate(_kaisaBullet,transform.position,Quaternion.identity);
                kaisaBullet.Init(transform.position,target.position,(a)=>Destroy(a.gameObject));
            }
        }

    }
}
