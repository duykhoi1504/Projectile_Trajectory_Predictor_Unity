namespace Trajectory.Runtime
{


    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class DrawLineGizmos : MonoBehaviour
    {
        [SerializeField] public float duration, heightY;
        [SerializeField] List<BulletSlot> bulletSlots;
        [SerializeField] private bool canDraw = true;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform targetPoint;
        [SerializeField] private BulletType currentBulletType;
        [SerializeField] private Vector3? targetPosition;

        public Vector3? TargetPosition { get => targetPosition; set => targetPosition = value; }

        private void Start()
        {
            CheckStart();

        }

        private void Update()
        {
            CheckStart();
        }

        private void CheckStart()
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

            switch (currentBulletType)
            {
                case BulletType.NormalBullet:
                    DrawGizmosNormal(startPoint.position, target);
                    break;
                case BulletType.KaisaBullet:
                    DrawGizmosKaisa(startPoint.position, target);
                    break;
            }

        }

        public void DrawGizmosNormal(Vector3 start, Vector3 target)
        {
            NormalBullet bullet = GetBulletSlot(BulletType.NormalBullet) as NormalBullet;
            Vector3 previousPoint = start;
            float timeStep = 0.01f;
            for (float t = 0; t <= duration; t += timeStep)
            {
                float linearT = t / duration;
                float heightT = bullet.Curve.Evaluate(linearT);
                float height = heightY * heightT;

                Vector3 currentPoint = Vector3.Lerp(start, target, linearT) + new Vector3(0, height, 0);

                Gizmos.color = Color.green;
                Gizmos.DrawLine(previousPoint, currentPoint);

                previousPoint = currentPoint;
            }

        }
        public void DrawGizmosKaisa(Vector3 start, Vector3 target)
        {
            KaisaBullet bullet = GetBulletSlot(BulletType.KaisaBullet) as KaisaBullet;

            bullet.InitializeNoise();

            Vector3 previousPoint = start;
            float timeStep = 0.01f;
            for (float t = 0; t <= duration; t += timeStep)
            {
                float linearT = t / duration;
                float heightT = bullet.Curve.Evaluate(linearT);
                float height = heightY * heightT * bullet.NoiseY;

                Vector3 currentPoint = Vector3.Lerp(start, target, linearT) + new Vector3(0, height, 0);

                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(previousPoint, currentPoint);

                previousPoint = currentPoint;
            }

        }
    }
}