namespace MCPMovement.Sample
{
    using System.Collections;
    using System.Collections.Generic;
    using MCPMovement.Runtime.MCPMove.LogicMove;
    using UnityEngine;
    public class BulletManager : MonoBehaviour
    {
        [SerializeField] List<MoveTypeSlot> bulletSlots;
        [SerializeField] private float duration;

        [Header("Pool")]
        [SerializeField] List<EntityMove> bullets = new List<EntityMove>();


        public EntityMove GetBulletSlot(MoveType bulletType)
        {
            foreach (MoveTypeSlot slot in bulletSlots)
            {
                if (slot.bulletType == bulletType)
                {
                    return slot.bullet;
                }
            }
            return null;
        }
        public void SpawmBullet(Vector3 start, Vector3 end, MoveType bulletType)
        {

            EntityMove bullet = GetBulletInPool(bulletType);
            bullet.Init(start, end,duration, (a) => BackToPool(a));
            
        }
        public EntityMove GetBulletInPool(MoveType bulletType)
        {
            foreach (EntityMove obj in bullets)
            {
                if (!obj.gameObject.activeSelf && obj.Type == bulletType)
                {
                    obj.gameObject.SetActive(true);

                    return obj;
                }
            }

            EntityMove newObject = Instantiate(GetBulletSlot(bulletType), transform.position, Quaternion.identity);
            bullets.Add(newObject);
            return newObject;
        }
        private void BackToPool(EntityMove bullet)
        {
            bullet.gameObject.SetActive(false);
        }

    }
}