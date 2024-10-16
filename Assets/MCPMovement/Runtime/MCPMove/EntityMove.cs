
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

        [SerializeField] private Transform headHolder;
        [SerializeField] private Transform trailHolder;
        [SerializeField] private Transform triggerHolder;

        //Element
        [SerializeField] private List<TrailRenderer> trails;
        private List<SpriteRenderer> sprites;

        #endregion

        #region Component
        [SerializeField] private MovementTJT movement;
        [SerializeField] private DestructionTJT destructTJT;

        //1
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

        private bool inited = false;

        public List<ILifeTime> lifeTimes;

        //Editor
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

            lifeTimes = GetComponentsInChildren<ILifeTime>(true).ToList();
        }

        public void Init(Vector3 start, Vector3 target, float duration, Action<EntityMove> onDestroy)
        {
            ResetBullet();

            this.start = start;
            this.target = target;
            this.duration = duration;
            this.onDestroy += onDestroy;

            List<float> maxTimeList = new();
            lifeTimes.ForEach(t => maxTimeList.Add(t.Time));
            totalTime = maxTimeList.Max() + duration;

            hitTriggerTJT.Init(triggerHolder, duration);
            destructTJT.Init(headHolder, trailHolder, onDestroy, totalTime);
            elementTJT.Init(headHolder, trailHolder, trails, sprites, duration);

            inited = true;
        }

        private void ResetBullet()
        {
            inited = false;
            time = 0;
            SetActiveParent(triggerHolder, false);
            elementTJT.ResetElement(start);
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
            hitTriggerTJT.OnUpdate(time);
            elementTJT.OnUpdate(time);
        }

        private void SetActiveParent(Transform parent, bool isActive)
        {
            if (parent != null)
            {
                parent.gameObject.SetActive(isActive);
            }
        }


        #region TEST

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
        #endregion
    }

}