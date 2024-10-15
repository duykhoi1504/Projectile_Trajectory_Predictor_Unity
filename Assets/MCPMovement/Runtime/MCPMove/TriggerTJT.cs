
namespace MCPMovement.Runtime.MCPMove.LogicTrigger
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class TriggerTJT : MonoBehaviour
    {
        private Transform triggerHolder;
        [SerializeField] private float timeDelayHit;
        [SerializeField] private bool canTrigger;
        public float TimeDelayHit { get => timeDelayHit;  }
        public bool CanTrigger { get => canTrigger; }

        public void Init(Transform triggerHolder)
        {
            this.triggerHolder = triggerHolder;

        }
        private void OnEnable()
        {
            SetActiveHit(false);
        }
        private void Start()
        {
            SetActiveHit(false);
        }


        public void CheckForHit(float time, float duration)
        {
            if (time < duration) return;
            SetActiveHit(true);

        }


        //bật tắt hit
        public void SetActiveHit(bool isActive)
        {
            if (triggerHolder != null && canTrigger)
                triggerHolder.gameObject.SetActive(isActive);
        }
    }
}