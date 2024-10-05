namespace Trajectory.PJT
{
    using System.Collections;
    using System.Collections.Generic;
    using Trajectory.PJT;
    using UnityEngine;

    public class DrawLineBullet : MonoBehaviour
    {
        [SerializeField] List<BulletSlot> bulletSlots;
        [SerializeField] private bool canDraw = true;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform targetPoint;
        [SerializeField] private BulletType currentBulletType;
        [SerializeField] private Vector3? targetPosition;
        public Vector3? TargetPosition { get => targetPosition; set => targetPosition = value; }



        private void Start()
        {

            if (startPoint == null)
            {
                startPoint = this.transform;
            }
        }

        IEnumerator DelayLine(float duration)
        {
            canDraw = false;
            yield return new WaitForSeconds(duration);
            canDraw = true;
        }


        
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


        private void OnDrawGizmos()
        {
            Vector3 target;
            if (!canDraw) return;


            if (targetPoint != null)
            {
                target = targetPoint.position;
            }
            else if (TargetPosition.HasValue)
            {
                target = TargetPosition.Value;
            }
            else
            {
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target.z = 0;
            }

            if (startPoint == null) return;

            GetBulletSlot(currentBulletType).DrawGizmos(startPoint.position, target);
        }
    }


}
