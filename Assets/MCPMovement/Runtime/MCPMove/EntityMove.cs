
namespace MCPMovement.Runtime.MCPMove.LogicMove
{
    using UnityEngine;
    using System.Collections;
    using Random = UnityEngine.Random;
    using System;
    using MCPMovement.Runtime.MCPMove.LogicDest;
    using MCPMovement.Runtime.MCPMove.LogicRota;
    using Unity.VisualScripting;

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

    [RequireComponent(typeof(MovementTJT), typeof(DestructionTJT)),]
    public abstract class EntityMove : MonoBehaviour
    {
        [SerializeField] private MoveType type;
        private Color color;
        protected float time;
        protected Vector3 start, target;
        [SerializeField] protected AnimationCurve curve;
        [SerializeField] protected float heightY, duration;

        [SerializeField] private TrailRenderer[] trails;
        [SerializeField] private SpriteRenderer[] sprites;
        [SerializeField] private Transform headHolder;
        [SerializeField] private Transform trailHolder;

        [SerializeField] private MovementTJT movement;
        [SerializeField] private DestructionTJT destruction;
        private event Action<EntityMove> onDestroy;



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

        [ContextMenu("SetUp")]
        public void SetUp()
        {
            if (headHolder.gameObject.activeSelf)
            {
               sprites = headHolder.GetComponentsInChildren<SpriteRenderer>();
            }
            if (trailHolder.gameObject.activeSelf)
            {
                
                 trails = trailHolder.GetComponentsInChildren<TrailRenderer>();
            }


            movement = GetComponent<MovementTJT>();
            destruction = GetComponent<DestructionTJT>();
            destruction.Init(sprites, trails, onDestroy);


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
        private void OnEnable()
        {
            ResetBullet();
        }
        protected virtual void Start()
        {
            SetUp();

            // time = 0;
            ResetBullet();
            RandomColorTrail();

        }
        protected virtual void Update()
        {
            movement.CheckDuration(duration, target);
            destruction.CheckForDestruct(time, duration);

        }

        private void ResetBullet()
        {
            time = 0;

            foreach (var trail in trails)
            {

                if (trail != null) // Kiểm tra null trước khi gọi phương thức
                {
                    trail.enabled = false;
                    trail.Clear(); // Xóa toàn bộ trail
                }
            }
            transform.position = start;
            foreach (var sprite in sprites)
            {
                if (sprite != null) // Kiểm tra null trước khi gọi phương thức
                {
                    sprite.enabled = false;
                }
            }

            // Đợi 1 frame trước khi bật lại
            StartCoroutine(EnableTrailAndSprite());
        }

        private IEnumerator EnableTrailAndSprite()
        {
            yield return null; // Đợi 1 frame

            foreach (var trail in trails)
            {
                if (trail != null)
                {
                    trail.enabled = true; // Bật lại trail
                }
            }
            foreach (var sprite in sprites)
            {
                if (sprite != null) // Kiểm tra null trước khi gọi phương thức
                {
                    sprite.enabled = true;
                }
            }
        }
        //mỗi màu bắn ra sẽ là 1 màu trailrenderer khác nhau
        protected void RandomColorTrail()
        {
            color = Random.ColorHSV();
            foreach (var trail in trails)
            {

                if (trail != null) // Kiểm tra null trước khi gọi phương thức
                {
                    trail.startColor = color;
                    trail.endColor = color;
                }
            }
        }
    }

}