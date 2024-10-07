
namespace Trajectory.Runtime
{
    using UnityEngine;
    using System.Collections;
    using Random = UnityEngine.Random;
    using System;

    [System.Serializable]
    public class BulletSlot
    {
        public BulletType bulletType;
        public Bullet bullet;
    }

    public enum BulletType
    {
        NormalBullet,
        KaisaBullet,
    }

    [RequireComponent(typeof(BulletMovement),typeof(BulletDestruction))]
    public abstract class Bullet : MonoBehaviour
    {
        [SerializeField] private BulletType type;
        private Color color;
        protected float time;
        protected Vector3 start, target;
        [SerializeField] protected AnimationCurve curve;
        [SerializeField] protected float heightY,duration;

        [SerializeField] private TrailRenderer[] trails;
        [SerializeField] private SpriteRenderer[] sprites;

        [SerializeField] private BulletMovement bulletMovement;
        [SerializeField] private BulletDestruction bulletDestruction;
        [SerializeField]
        protected event Action<Bullet> onDestroy;
        public BulletType Type { get => type; set => type = value; }
        public AnimationCurve Curve { get => curve; set => curve = value; }
        public float Duration { get => duration; set => duration = value; }
        public float HeightY { get => heightY; set => heightY = value; }

        public void Init(Vector3 start, Vector3 target, Action<Bullet> onDestroy)
        {
            this.start = start;
            this.target = target;
            this.onDestroy += onDestroy;
        }

        [ContextMenu("SetUp")]
        public void SetUp()
        {
            bulletMovement = GetComponent<BulletMovement>();
           bulletDestruction = GetComponent<BulletDestruction>();

            trails = GetComponentsInChildren<TrailRenderer>();
            sprites = GetComponentsInChildren<SpriteRenderer>();
            
            bulletDestruction.Init(sprites,trails,onDestroy);
        }
        private void OnEnable()
        {
            ResetBullet();
        }
        protected virtual void Start()
        {
            SetUp();
            time = 0;
            RandomColorTrail();

        }
        protected virtual void Update()
        {
            bulletMovement.CheckDuration(duration, target);
            bulletDestruction.CheckForDestruct(time,duration);
        }

        private void ResetBullet()
        {
            time = 0;
            foreach (var trail in trails)
            {
                transform.position = start;

                trail.Clear(); // Xóa toàn bộ trail
                trail.enabled = false;

            }
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