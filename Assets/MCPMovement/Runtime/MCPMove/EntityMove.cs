
namespace MCPMovement.Runtime.MCPMove.LogicMove
{
    using UnityEngine;
    using System.Collections;
    using System;
    using MCPMovement.Runtime.MCPMove.LogicDestruction;
    using System.Collections.Generic;
    using MCPMovement.Runtime.MCPMove.LogicTrigger;
    using MCPMovement.Runtime.MCPMove.LogicElement;
    using System.Linq;

    [System.Serializable]
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

    [RequireComponent(typeof(MovementTJT), typeof(DestructionTJT), typeof(HitTriggerTJT)),]
    public abstract class EntityMove : MonoBehaviour
    {
        #region Element
        [SerializeField] private MoveType type;
        [SerializeField] protected AnimationCurve curve;
        protected float time;
        protected Vector3 start, target;
        [SerializeField] protected float heightY;
        protected float duration;
        float totalTime;

        [SerializeField] private List<TrailRenderer> trails;
        private List<SpriteRenderer> sprites;
        [SerializeField] private Transform headHolder;
        [SerializeField] private Transform trailHolder;
        [SerializeField] private Transform triggerHolder;


        #endregion

        #region Component
        [SerializeField] private MovementTJT movement;
        [SerializeField] private DestructionTJT destructTJT;
        [SerializeField] private HitTriggerTJT hitTriggerTJT;
        [SerializeField] private ElementTJT elementTJT;

        #endregion


        #region Event
        private event Action<EntityMove> onDestroy;

        #endregion


        #region Getter Setter
        public MoveType Type { get => type; }
        public AnimationCurve Curve { get => curve; }
        public float Duration { get => duration; }
        public float HeightY { get => heightY; }

        public void Init(Vector3 start, Vector3 target, float duration, Action<EntityMove> onDestroy)
        {
            this.start = start;
            this.target = target;
            this.duration = duration;
            this.onDestroy += onDestroy;

            ////////tính maxtime của element////////
            List<float> maxTimeList = new List<float>();
            if (trails != null)
            {
                foreach (TrailRenderer trail in trails)
                {
                    if (trail != null)
                    {

                        maxTimeList.Add(trail.time);
                    }

                }
            }
            maxTimeList.Add(hitTriggerTJT.TimeDelayHit);

            maxTimeList.Add(destructTJT.DefaultTime);



            float maxTimeElement = maxTimeList.Max();

            totalTime = maxTimeElement + duration;

            /////////////////////////////////////////////
            hitTriggerTJT.Init(duration);
            destructTJT.Init(totalTime);
            elementTJT.Init(duration);
        }
        #endregion

        [ContextMenu("SetUp")]
        public void SetUp()
        {
            if (headHolder.childCount > 0 && headHolder != null && headHolder.gameObject.activeSelf)
            {
                sprites = new List<SpriteRenderer>();
                foreach (Transform child in headHolder)
                {
                    SpriteRenderer sprite = child.GetComponent<SpriteRenderer>();
                    if (sprite != null)
                    {
                        sprites.Add(sprite);
                    }
                }


            }

            if (trailHolder.childCount > 0 && trailHolder != null && trailHolder.gameObject.activeSelf)
            {
                trails = new List<TrailRenderer>();
                foreach (Transform child in trailHolder)
                {
                    TrailRenderer trail = child.GetComponent<TrailRenderer>();
                    if (trail != null)
                    {
                        trails.Add(trail);
                    }
                }
            }

            // movement = GetComponent<MovementTJT>();
            // hitTriggerTJT=GetComponent<HitTriggerTJT>();
            hitTriggerTJT.SetUp(triggerHolder);
            destructTJT.SetUp(headHolder, trailHolder, onDestroy);
            elementTJT.SetUp(headHolder, trailHolder, trails, sprites);
        }


        // private void OnEnable()
        // {
        //     ResetBullet();
        // }
        protected virtual void Start()
        {
            SetUp();
            ResetBullet();

        }
        protected virtual void Update()
        {
            time += Time.deltaTime;
            movement.OnUpdate(duration, target);
            destructTJT.OnUpdate(time);
            hitTriggerTJT.OnUpdate(time);
            elementTJT.OnUpdate(time);
        }

        public void ResetBullet()
        {
            time = 0;
            SetActiveParent(triggerHolder, false);
            elementTJT.ResetElement(start);

        }
        private void SetActiveParent(Transform parent, bool isActive)
        {
            if (parent != null)
            {
                parent.gameObject.SetActive(isActive);
            }
        }

        public void SetActiveHead(bool isActive)
        {
            if (headHolder != null)
            {
                headHolder.gameObject.SetActive(isActive);
            }
        }

        public void SetActiveTrail(bool isActive)
        {
            if (trailHolder != null)
            {
                trailHolder.gameObject.SetActive(isActive);
            }
        }
        public void SetActiceHit(bool isActive)
        {
            if (triggerHolder != null)
            {
                hitTriggerTJT.SetActiveHit(isActive);
            }
        }
    }

}