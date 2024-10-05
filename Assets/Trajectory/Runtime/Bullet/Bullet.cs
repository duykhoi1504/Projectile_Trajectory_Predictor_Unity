
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

    [RequireComponent(typeof(BulletMovement))]
    public abstract class Bullet : MonoBehaviour
    {
        [SerializeField] private BulletType type;
        private Color color;
        protected Vector3 start, target;
        [SerializeField] private AnimationCurve curve;
        [SerializeField] public float duration, heightY;
        protected float time;

        [SerializeField] private TrailRenderer trail;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private BulletMovement bulletMovement;


        [SerializeField] private Vector3? targetVec3;
        public Vector3? TargetPosition { get => targetVec3; set => targetVec3 = value; }
        public BulletType Type { get => type; set => type = value; }
        public AnimationCurve Curve { get => curve; set => curve = value; }

        protected event Action<Bullet> onDestroy;
        public void Init(Vector3 start, Vector3 target, Action<Bullet> onDestroy)
        {
            this.start = start;
            this.target = target;
            this.onDestroy += onDestroy;
        }
        public void SetUp()
        {
            bulletMovement = GetComponent<BulletMovement>();
            trail = GetComponentInChildren<TrailRenderer>();

            sprite = GetComponentInChildren<SpriteRenderer>();
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
        }

        //làm mới viên đạn sau khi va cham muc tieu
        protected void ResetBullet()
        {
            time = 0;
            transform.position = start;
            // Col.enabled = true;
            sprite.enabled = true;
        }


        //mỗi màu bắn ra sẽ là 1 màu trailrenderer khác nhau
        protected void RandomColorTrail()
        {
            color = Random.ColorHSV();
            trail.startColor = color;
            trail.endColor = color;
        }

        protected void Destruct()
        {
            Debug.Log("BOOM, GET PLAYERIMPACT");
            StartCoroutine(IEDestruct());
        }


        private IEnumerator IEDestruct()
        {
            sprite.enabled = false;
            // Col.enabled = false;
            yield return new WaitForSeconds(2f);

            ResetBullet();

            onDestroy?.Invoke(this);
        }
    }

}