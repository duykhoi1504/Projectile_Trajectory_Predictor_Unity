namespace MCPMovement.Runtime.MCPMove.LogicDestruction
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using MCPMovement.Runtime.MCPMove.LogicMove;
    using MCPMovement.Runtime.MCPMove.LogicTrigger;
    using UnityEngine;
    public class DestructionTJT : MonoBehaviour
    {
        private Transform headHolder;
        private Transform trailHolder;
        private List<SpriteRenderer> sprites;
        private List<TrailRenderer> trails;
        private event Action<EntityMove> onDestroy;
        private TriggerTJT triggerTJT;


        [Tooltip("nếu không dùng trail sẽ dùng mặc định là thời gian này")]
        [SerializeField] private float defaultTime = .1f;


        private void Start()
        {
            triggerTJT = GetComponent<TriggerTJT>();
        }
        public void Init(List<SpriteRenderer> sprites, List<TrailRenderer> trails, Transform headHolder, Transform trailHolder, Action<EntityMove> onDestroy)
        {
            this.sprites = sprites;
            this.trails = trails;
            this.onDestroy = onDestroy;
            this.headHolder = headHolder;
            this.trailHolder = trailHolder;
        }
        public void CheckForDestruct(float time, float duration)
        {
            if (time < duration) return;
            StartCoroutine(IEDestruct(GetComponent<EntityMove>()));

        }



        private IEnumerator IEDestruct(EntityMove bullet)
        {
            float time;
            if (trails != null && trails.Count > 0 && trails[0] != null)
            {
                TrailRenderer trail = trails[0];
                time = trail.time;
            }
            else
            {
                time = defaultTime; // Thay thế thời gian mặc định nếu không có trail
            }

            //kiểm tra xem có trigger không để cộng vào time tránh return sớm về pool
            if (triggerTJT != null && triggerTJT.CanTrigger)
            {
                time += triggerTJT.TimeDelayHit;
            }
            SetActiveParent(headHolder, false);
            yield return new WaitForSeconds(time);

            onDestroy?.Invoke(bullet);

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