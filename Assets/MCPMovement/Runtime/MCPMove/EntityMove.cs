
namespace MCPMovement.Runtime.MCPMove.LogicMove
{
    using UnityEngine;
    using System.Collections;
    using System;
    using MCPMovement.Runtime.MCPMove.LogicDestruction;
    using System.Collections.Generic;
    using MCPMovement.Runtime.MCPMove.LogicTrigger;

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

    [RequireComponent(typeof(MovementTJT), typeof(DestructionTJT),typeof(TriggerTJT)),]
    public abstract class EntityMove : MonoBehaviour
    {
        #region Element
        [SerializeField] private MoveType type;
        [SerializeField] protected AnimationCurve curve;
        protected float time;
        protected Vector3 start, target;
        [SerializeField] protected float heightY;
        protected float duration;


        private List<TrailRenderer> trails;
        private List<SpriteRenderer> sprites;
        [SerializeField] private Transform headHolder;
        [SerializeField] private Transform trailHolder;
        [SerializeField] private Transform triggerHolder;


        #endregion

        #region Component
        private MovementTJT movement;
        private DestructionTJT destruction;
        private TriggerTJT triggerTJT;
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
            this.onDestroy += onDestroy;
            this.duration = duration;
        }
        #endregion

        [ContextMenu("SetUp")]
        public void SetUp()
        {
            if (headHolder != null && headHolder.gameObject.activeSelf)
            {

                if (headHolder.childCount > 0)
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


            if (trailHolder != null && trailHolder.gameObject.activeSelf)
            {
                if (trailHolder.childCount > 0)
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
            triggerTJT = GetComponent<TriggerTJT>();
            movement = GetComponent<MovementTJT>();
            destruction = GetComponent<DestructionTJT>();

            triggerTJT.Init(triggerHolder);
            destruction.Init(sprites, trails, headHolder, trailHolder, onDestroy);
        }


        private void OnEnable()
        {
            ResetBullet();
        }
        protected virtual void Start()
        {
            SetUp();

            ResetBullet();

        }
        protected virtual void Update()
        {
            movement.CheckDuration(duration, target);
            destruction.CheckForDestruct(time, duration);
            triggerTJT.CheckForHit(time, duration);

        }

        private void ResetBullet()
        {
            time = 0;
            SetActiveParent(triggerHolder, false);
            if (trails != null)
            {
                foreach (var trail in trails)
                {

                    if (trail != null) // Kiểm tra null trước khi gọi phương thức
                    {
                        trail.enabled = false;
                        trail.Clear(); // Xóa toàn bộ trail
                    }
                }
            }
            transform.position = start;
            SetActiveParent(headHolder, false);


            // Đợi 1 frame trước khi bật lại
            StartCoroutine(EnableTrailAndSprite());
        }
        private void SetActiveParent(Transform parent, bool isActive)
        {
            if (parent != null)
            {
                parent.gameObject.SetActive(isActive);
            }
        }
        private IEnumerator EnableTrailAndSprite()
        {
            yield return null; // Đợi 1 frame

            if (trails != null)
            {
                foreach (var trail in trails)
                {
                    if (trail != null)
                    {
                        trail.enabled = true; // Bật lại trail
                    }
                }
            }
            SetActiveParent(headHolder, true);
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
                triggerTJT.SetActiveHit(isActive);
            }
        }
    }

}