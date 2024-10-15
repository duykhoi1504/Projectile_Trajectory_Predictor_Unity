
namespace MCP.Runtime.MCPMove.LogicMove
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class BezierMove : EntityMove
    {

        private float noiseY;
        private System.Random random;
        [Header("Random Seed")]
        [SerializeField] private int seed;

        // public int Seed { get => seed; set => seed = value; }
        public float NoiseY { get => noiseY; }

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
        private void Apply(Vector3 start, Vector3 end)
        {

  
            time += Time.deltaTime;
            if (time < duration)
            {

                float linearT = time / duration;
                float heightT = curve.Evaluate(linearT);
                float height = heightT * heightY * NoiseY;

                transform.position = Vector3.Lerp(start, end, linearT) + new Vector3(0, height, 0);

            }

        }

        public void InitializeNoise()
        {
            random = new System.Random(seed);
            noiseY = (float)random.Next(-10, 11);
        }


    }


}





