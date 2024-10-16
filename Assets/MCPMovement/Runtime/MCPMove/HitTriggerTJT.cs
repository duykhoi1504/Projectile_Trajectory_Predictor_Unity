
namespace MCPMovement.Runtime.MCPMove.LogicTrigger
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class HitTriggerTJT : MonoBehaviour
    {
        private Transform triggerHolder;
        [SerializeField] private float timeDelayHit;
        [SerializeField] private bool canTrigger;

        private float duration;
        public float TimeDelayHit { get => timeDelayHit; }
        public bool CanTrigger { get => canTrigger; }

        public void Init(Transform triggerHolder, float duration)
        {
            this.triggerHolder = triggerHolder;
            this.duration = duration;
        }
 
        private void OnEnable()
        {
            ResetHit();
        }
        public void OnUpdate(float time)
        {
            if (canTrigger==false) 
                return;
            if(time>duration){
                SetActiveHit(true);
            }
            float totalHitTime=duration+timeDelayHit;
            if (time >= totalHitTime)
            {
                SetActiveHit(false);
            }
        }
        public void ResetHit()
        {
            SetActiveHit(false);
        }
        public void SetActiveHit(bool isActive)
        {
            if (triggerHolder != null && canTrigger)
                triggerHolder.gameObject.SetActive(isActive);
        }
    }
}


