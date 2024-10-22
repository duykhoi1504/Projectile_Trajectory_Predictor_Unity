
namespace MCPMovement.Runtime.MCPMove.LogicMove
{
    using UnityEngine;
    using System.Collections;
    using System;
    using MCPMovement.Runtime.MCPMove.LogicDestruction;
    using MCPMovement.Runtime.MCPMove.LogicElementTrigger;
    using System.Collections.Generic;
    using System.Linq;

    [Serializable]
    public class MoveTypeSlot
    {
        public MoveType bulletType;
        public EntityMove bullet;
    }

    public enum MoveType
    {
        NormalMove,
        BezierMove,
    }

    
    public abstract class EntityMove : MonoBehaviour
    {
        #region Element
        [SerializeField] private MoveType type;
        [SerializeField] protected AnimationCurve curve;
        protected float time;
        protected Vector3 start, target;
        [SerializeField] protected float heightY;
        protected float duration;
        protected float totalTime = 0;


        private bool inited = false;

        public List<ILifeTime> lifeTimes;
        [SerializeField] private List<float> maxTimeList;


        #endregion

        #region Component
        [SerializeField] private MovementTJT movement;
        [SerializeField] private DestructionTJT destructTJT;
        [SerializeField] private ElementTriggerTJT elementTriggerTJT;
        #endregion

        #region Event
        private event Action<EntityMove> onDestroy;

        #endregion

        #region Getter Setter
        public MoveType Type { get => type; }
        public AnimationCurve Curve { get => curve; }
        public float Duration { get => duration; }
        public float HeightY { get => heightY; }




        //Editor
        [ContextMenu("SetUp")]
        public void SetUp()
        {
            movement=GetComponent<MovementTJT>();
            destructTJT=GetComponent<DestructionTJT>();
            elementTriggerTJT=GetComponent<ElementTriggerTJT>();
            lifeTimes = GetComponentsInChildren<ILifeTime>().ToList();
            elementTriggerTJT.SetUp();

            maxTimeList.Clear();
            lifeTimes.ForEach(t => maxTimeList.Add(t.Time));

        }


        public void Init(Vector3 start, Vector3 target, float duration, Action<EntityMove> onDestroy)
        {

            ResetMove();
            this.start = start;
            this.target = target;
            this.duration = duration;
            this.onDestroy += onDestroy;

            totalTime = maxTimeList.Max() + duration;

            destructTJT.Init(onDestroy, totalTime);
            elementTriggerTJT.Init(duration,totalTime, start);

            inited = true;
        }

        private void ResetMove()
        {
            inited = false;
            time = 0;

        }

        #endregion

        private void Update()
        {
            if (inited == false)
                return;

            OnUpdate();
        }

        protected virtual void OnUpdate()
        {
            time += Time.deltaTime;
            movement.OnUpdate(duration, target);
            destructTJT.OnUpdate(time);
            elementTriggerTJT.OnUpdate(time);
        }

        private void SetActiveParent(Transform parent, bool isActive)
        {
            if (parent != null)
            {
                parent.gameObject.SetActive(isActive);
            }
        }



    }



    
}