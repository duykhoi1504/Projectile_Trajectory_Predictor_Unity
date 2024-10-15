namespace MCPMovement.Runtime.MCPMove.LogicDest
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using MCPMovement.Runtime.MCPMove.LogicMove;
    using UnityEngine;
    public class DestructionTJT : MonoBehaviour
    {
        private SpriteRenderer[] sprites;
        private TrailRenderer[] trails;
        private event Action<EntityMove> onDestroy;

        public void Init(SpriteRenderer[] sprites, TrailRenderer[] trails, Action<EntityMove> onDestroy)
        {
            this.sprites = sprites;
            this.trails = trails;
            this.onDestroy = onDestroy;
        }
        public void CheckForDestruct(float time, float duration)
        {
            if (time < duration) return;
            Destruct(GetComponent<EntityMove>());
        }
        private void Destruct(EntityMove bullet)
        {

            StartCoroutine(IEDestruct(bullet));
        }

        private IEnumerator IEDestruct(EntityMove bullet)
        {
            float time;
            if (trails != null && trails.Length > 0 && trails[0] != null)
            {
                TrailRenderer trail = trails[0];
                time = trail.time;
            }
            else
            {
                time = .01f; // Thay thế thời gian mặc định nếu không có trail
            }

            foreach (var sprite in sprites)
            {
                if (sprite != null) // Kiểm tra null trước khi gọi phương thức
                {
                    sprite.enabled = false;
                }
            }
            yield return new WaitForSeconds(time);

            onDestroy?.Invoke(bullet);
            // onDestroy=null;

        }
    }
}