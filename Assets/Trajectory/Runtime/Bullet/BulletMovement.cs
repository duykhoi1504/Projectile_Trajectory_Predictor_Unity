namespace Trajectory.Runtime
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class BulletMovement : MonoBehaviour
    {

        public void CheckDuration(float duration, Vector3 target)
        {

            if (duration <= 0)
            {
                this.transform.position = target;
            }
        }
    }
}