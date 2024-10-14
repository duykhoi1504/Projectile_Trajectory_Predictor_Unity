
namespace MCP.Runtime.MCPMove.LogicMove
{
    using UnityEngine;
    using System.Collections;
    using Random = UnityEngine.Random;
    using System;
    using MCP.Runtime.MCPMove.LogicDest;
    using MCP.Runtime.MCPMove.LogicRota;

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

        [SerializeField] private MovementTJT movement;
        [SerializeField] private DestructionTJT destruction;
     


        [SerializeField]
        protected event Action<EntityMove> onDestroy;



        public MoveType Type { get => type; }
        public AnimationCurve Curve { get => curve; }
        public float Duration { get => duration; }
        public float HeightY { get => heightY; }

        public void Init(Vector3 start, Vector3 target, float duration, float height, Action<EntityMove> onDestroy)
        {
            this.start = start;
            this.target = target;
            this.onDestroy += onDestroy;
            this.duration = duration;
            this.heightY = height;
        }

        [ContextMenu("SetUp")]
        public void SetUp()
        {
            movement = GetComponent<MovementTJT>();
            destruction = GetComponent<DestructionTJT>();
    


            trails = GetComponentsInChildren<TrailRenderer>();
            sprites = GetComponentsInChildren<SpriteRenderer>();
            destruction.Init(sprites, trails, onDestroy);
 

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

                trail.enabled = false;
                trail.Clear(); // Xóa toàn bộ trail

            }
            transform.position = start;
            foreach (var sprite in sprites)
            {
                sprite.enabled = false;
            }

            // Đợi 1 frame trước khi bật lại
            StartCoroutine(EnableTrailAndSprite());
        }

        private IEnumerator EnableTrailAndSprite()
        {
            yield return null; // Đợi 1 frame
  
            foreach (var trail in trails)
            {
                trail.enabled = true; // Bật lại trail
            }
            foreach (var sprite in sprites)
            {
                sprite.enabled = true;
            }
        }
        //mỗi màu bắn ra sẽ là 1 màu trailrenderer khác nhau
        protected void RandomColorTrail()
        {
            color = Random.ColorHSV();
            foreach (var trail in trails)
            {
                trail.startColor = color;
                trail.endColor = color;
            }
        }
    }

}