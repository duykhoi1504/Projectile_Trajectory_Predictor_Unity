
namespace Trajectory.PJT
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class KaisaBullet : Bullet
    {

        private float noiseY;

        private System.Random random;
        [SerializeField] private int seed;

        public int Seed { get => seed; set => seed = value; }

        protected override void Start()
        {

            base.Start();
            InitializeNoise();
        }

        protected override void Update()
        {
            base.Update();
            Apply(start, target);
        }
        public void Apply(Vector3 start, Vector3 end)
        {

            Vector3 previousPoint = transform.position;
            time += Time.deltaTime;
            if (time < duration)
            {

                float linearT = time / duration;
                float heightT = curve.Evaluate(linearT);
                float height = heightT * heightY * noiseY;

                transform.position = Vector3.Lerp(start, end, linearT) + new Vector3(0, height, 0);

                Vector3 direction = (transform.position - previousPoint).normalized;
                transform.up = direction;
            }
            else
            {
                Destruct();
            }
        }
        private void InitializeNoise()
        {
       
            random = new System.Random(seed);
            noiseY = (float)random.Next(-10, 11);
        }
        public override void DrawGizmos(Vector3 start, Vector3 target)
        {

            InitializeNoise();

            Vector3 previousPoint = start;
            float timeStep = 0.01f;
            for (float t = 0; t <= duration; t += timeStep)
            {
                float linearT = t / duration;
                float heightT = curve.Evaluate(linearT);
                float height = heightY * heightT * noiseY;

                Vector3 currentPoint = Vector3.Lerp(start, target, linearT) + new Vector3(0, height, 0);

                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(previousPoint, currentPoint);

                previousPoint = currentPoint;
            }

        }

    }


}





