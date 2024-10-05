namespace Trajectory.Sample
{
    using System.Collections;
    using System.Collections.Generic;
    using Trajectory.Runtime;
    using Unity.VisualScripting;
    using UnityEngine;
    public class BulletManager : MonoBehaviour
    {
             [SerializeField] List<BulletSlot> bulletSlots;
        // [SerializeField] private BulletSO bulletSO;
        // [SerializeField] private bool canDraw = true;
        // [SerializeField] private BulletType currentBulletType;
  
     [Header("Pool")]
        [SerializeField] List<Bullet> bullets = new List<Bullet>();

        public Bullet GetBulletSlot(BulletType bulletType)
        {
            foreach (BulletSlot slot in bulletSlots)
            {
                if (slot.bulletType == bulletType)
                {
                    return slot.bullet;
                }
            }
            return null;
        }
        public void SpawmBullet(Vector3 start, Vector3 end, BulletType bulletType)
        {

            Bullet bullet = GetBulletInPool(bulletType);
   
            bullet.Init(start, end,(a)=>BackToPool(a));

        }
        public Bullet GetBulletInPool(BulletType bulletType)
        {
            foreach (Bullet obj in bullets)
            {
                if (!obj.gameObject.activeSelf && obj.Type == bulletType)
                {
                    obj.gameObject.SetActive(true);
                    
                    return obj;
                }
            }

            Bullet newObject = Instantiate(GetBulletSlot(bulletType), transform.position, Quaternion.identity);
            bullets.Add(newObject);
            return newObject;
        }
    private void BackToPool(Bullet bullet){
        bullet.gameObject.SetActive(false);
    }

    }
}