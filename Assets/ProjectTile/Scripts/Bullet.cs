
namespace Trajectory.PJT
{
    using UnityEngine;
    using System.Collections;
    using Random = UnityEngine.Random;

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
    public abstract class Bullet : MonoBehaviour
    {
        [SerializeField] private BulletType type;
        private Color color;
        protected Vector3 start, target;
        [SerializeField] protected AnimationCurve curve;
        [SerializeField] public float duration, heightY;
        protected float time;

        [SerializeField] private TrailRenderer trail;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private Collider2D col;


        [SerializeField] private Vector3? targetVec3;
        public Vector3? TargetPosition { get => targetVec3; set => targetVec3 = value; }
        public BulletType Type { get => type; set => type = value; }
        public TrailRenderer Trail { get => trail; set => trail = value; }
        public SpriteRenderer Sprite { get => sprite; set => sprite = value; }
        public Collider2D Col { get => col; set => col = value; }



        public void Init(Vector3 start, Vector3 target)
        {
            this.start = start;
            this.target = target;
        }
        public void SetUp()
        {
            Trail = GetComponentInChildren<TrailRenderer>();
            Col = GetComponent<Collider2D>();
            Sprite = GetComponentInChildren<SpriteRenderer>();
        }

        protected virtual void Start()
        {

            SetUp();
            time = 0;
            RandomColorTrail();

        }


        protected virtual void Update()
        {

            if (duration <= 0)
            {
                this.transform.position = target;
            }
            if (!Col.enabled) return;

        }

        //làm mới viên đạn sau khi va cham muc tieu
        protected void ResetBullet()
        {
            time = 0;
            transform.position = start;
            Col.enabled = true;
            Sprite.enabled = true;
        }


        public virtual void DrawGizmos(Vector3 start, Vector3 target)
        {
            Vector3 previousPoint = start;
            float timeStep = 0.01f;
            for (float t = 0; t <= duration; t += timeStep)
            {
                float linearT = t / duration;
                float heightT = curve.Evaluate(linearT);
                float height = heightY * heightT;

                Vector3 currentPoint = Vector3.Lerp(start, target, linearT) + new Vector3(0, height, 0);

                Gizmos.color = Color.green;
                Gizmos.DrawLine(previousPoint, currentPoint);

                previousPoint = currentPoint;
            }

        }
        //mỗi màu bắn ra sẽ là 1 màu trailrenderer khác nhau
        protected void RandomColorTrail()
        {
            color = Random.ColorHSV();
            Trail.startColor = color;
            Trail.endColor = color;
        }

        protected void Destruct()
        {
            Debug.Log("BOOM, GET PLAYERIMPACT");
            StartCoroutine(IEDestruct());
        }
        

        private IEnumerator IEDestruct()
        {
            Sprite.enabled = false;
            Col.enabled = false;
            yield return new WaitForSeconds(2f);

            ResetBullet();
            this.gameObject.SetActive(false);
        }
    }

}