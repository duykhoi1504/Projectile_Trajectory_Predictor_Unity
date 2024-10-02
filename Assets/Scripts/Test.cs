using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] BulletManager bulletManager;
    private void Start() {
        bulletManager=GetComponent<BulletManager>();
    }
    private void Update() {

           if(Input.GetKeyDown(KeyCode.E)) {
            bulletManager.SpawmBullet(transform.position,target.position,BulletType.NormalParabol);
        }    if(Input.GetKeyDown(KeyCode.R)) {
            bulletManager.SpawmBullet(transform.position,target.position,BulletType.KaisaParabol);
        }
    }
}
