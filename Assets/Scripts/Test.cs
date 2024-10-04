using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trajectory.PJT;
public class Test : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] BulletManager bulletManager;
    private void Start() {
        bulletManager=GetComponent<BulletManager>();
    }
    private void Update() {

           if(Input.GetKeyDown(KeyCode.E)) {
            bulletManager.SpawmBullet(transform.position,target.position,BulletType.NormalBullet);
        }    if(Input.GetKeyDown(KeyCode.R)) {
            bulletManager.SpawmBullet(transform.position,target.position,BulletType.KaisaBullet);
        }
    }
}
