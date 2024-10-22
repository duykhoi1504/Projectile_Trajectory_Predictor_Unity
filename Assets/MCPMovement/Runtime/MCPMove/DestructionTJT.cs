namespace MCPMovement.Runtime.MCPMove.LogicDestruction
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using MCPMovement.Runtime.MCPMove.LogicMove;
    using UnityEngine;

    public class DestructionTJT : MonoBehaviour
    {
        private event Action<EntityMove> onDestroy;
        private float duration;

        // public void SetUp(Transform headHolder, Transform trailHolder)
        // {
        //     if (headHolder)
        //         this.headHolder = headHolder;
        //     if (trailHolder)
        //         this.trailHolder = trailHolder;
        // }
        public void Init( Action<EntityMove> onDestroy, float totalTime)
        {
            this.onDestroy = onDestroy;
            // this.headHolder = headHolder;
            // this.trailHolder = trailHolder;
            this.duration = totalTime;
        }

        public void OnUpdate(float time)
        {
            if (time > duration)
            {
                // Bắt đầu quá trình hủy
                onDestroy?.Invoke(GetComponent<EntityMove>());
            }
        }
    }


}
