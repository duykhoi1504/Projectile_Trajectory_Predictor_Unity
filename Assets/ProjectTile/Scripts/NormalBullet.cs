namespace Trajectory.PJT
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class NormalBullet : Bullet
    {

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
                float linearT = time / duration;// 0 to 1
                float heightT = curve.Evaluate(linearT);//value from curve
                float height = Mathf.Lerp(0f, heightY, heightT);

                transform.position = Vector3.Lerp(start, end, linearT) + new Vector3(0, height, 0);

                Vector3 direction = (transform.position - previousPoint).normalized;
                transform.up = direction;


            }
            else
            {
                Destruct();
            }
        }



    }
}