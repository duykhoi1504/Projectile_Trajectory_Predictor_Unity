namespace MCP.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using MCP.Runtime.MCPMove.LogicMove;
    using UnityEngine;
    // [System.Serializable]
    // public class BulletSlot
    // {
    //     public BulletType bulletType;
    //     public EntityMove bullet;
    // }

    // public enum BulletType
    // {
    //     NormalBullet,
    //     KaisaBullet,
    // }
    public class DrawLineGizmos : MonoBehaviour
    {
        [SerializeField] private float duration, heightY;
        [SerializeField] private List<MoveTypeSlot> moveTypeSlot;
        [SerializeField] private bool canDraw = true;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform targetPoint;
        [SerializeField] private MoveType currentMoveType;
        [SerializeField] private Vector3? targetPosition;
        private float previousHeightY;
        public Vector3? TargetPosition { get => targetPosition; set => targetPosition = value; }

        [ContextMenu("ReSetUp")]
        private void ReSetUp()
        {
            EntityMove bullet = GetBulletSlot(currentMoveType);
            duration = bullet.Duration;
            heightY = bullet.HeightY;
            previousHeightY = heightY; 
        }
        private void CheckChangeValue()
        {
            EntityMove bullet = GetBulletSlot(currentMoveType);
            if (bullet != null && bullet.HeightY != previousHeightY)
            {
                heightY = bullet.HeightY;
                previousHeightY = heightY;
            }
        }
        private void Start()
        {
            ReSetUp();

        }

        private void Update()
        {
            CheckStart();
            CheckChangeValue();

        }
        private void CheckStart()
        {
            if (startPoint == null)
            {
                startPoint = this.transform;
            }
        }
        // IEnumerator DelayLine(float duration)
        // {
        //     canDraw = false;
        //     yield return new WaitForSeconds(duration);
        //     canDraw = true;
        // }



        private EntityMove GetBulletSlot(MoveType bulletType)
        {

            foreach (MoveTypeSlot slot in moveTypeSlot)
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
            CheckChangeValue();
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

            switch (currentMoveType)
            {
                case MoveType.NormalMove:
                    DrawGizmosNormal(startPoint.position, target);
                    break;
                case MoveType.BezierMove:
                    DrawGizmosKaisa(startPoint.position, target);
                    break;
            }

        }

        private void DrawGizmosNormal(Vector3 start, Vector3 target)
        {
            NormalMove bullet = GetBulletSlot(MoveType.NormalMove) as NormalMove;
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
        private void DrawGizmosKaisa(Vector3 start, Vector3 target)
        {
            BezierMove bullet = GetBulletSlot(MoveType.BezierMove) as BezierMove;

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