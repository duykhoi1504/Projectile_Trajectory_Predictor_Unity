
namespace Trajectory.Runtime
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class KaisaBullet : Bullet
    {

        private float noiseY;
        private System.Random random;
        [Header("Random Seed")]
        [SerializeField] private int seed;

        public int Seed { get => seed; set => seed = value; }
        public float NoiseY { get => noiseY; set => noiseY = value; }

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
                float height = heightT * heightY * NoiseY;

                transform.position = Vector3.Lerp(start, end, linearT) + new Vector3(0, height, 0);

                Vector3 direction = (transform.position - previousPoint).normalized;
                transform.up = direction;
            }
   
        }
        
        public void InitializeNoise()
        {
            random = new System.Random(seed);
            NoiseY = (float)random.Next(-10, 11);
        }


    }


}





