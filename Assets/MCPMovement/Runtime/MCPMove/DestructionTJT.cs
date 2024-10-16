namespace MCPMovement.Runtime.MCPMove.LogicDestruction
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using MCPMovement.Runtime.MCPMove.LogicMove;
    using MCPMovement.Runtime.MCPMove.LogicTrigger;
    using UnityEngine;

    public class DestructionTJT : MonoBehaviour
    {
        private Transform headHolder;
        private Transform trailHolder;

        private event Action<EntityMove> onDestroy;


        [Tooltip("nếu không dùng trail sẽ dùng mặc định là thời gian này")]
        [SerializeField] private float defaultTime = .1f;

        private float duration;
        public float DefaultTime { get => defaultTime; }



        public void SetUp(Transform headHolder, Transform trailHolder, Action<EntityMove> onDestroy)
        {
            this.onDestroy = onDestroy;
            this.headHolder = headHolder;
            this.trailHolder = trailHolder;
        }
        public void Init(float totalTime)
        {
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
