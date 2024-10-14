namespace MCP.Sample
{
    using System.Collections;
    using System.Collections.Generic;
    using MCP.Runtime.MCPMove.LogicMove;
    using Unity.VisualScripting;
    using UnityEngine;
    public class BulletManager : MonoBehaviour
    {
        [SerializeField] List<MoveTypeSlot> bulletSlots;
        // [SerializeField] private BulletSO bulletSO;
        // [SerializeField] private bool canDraw = true;
        // [SerializeField] private BulletType currentBulletType;

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

            bullet.Init(start, end,1f,10, (a) => BackToPool(a));

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