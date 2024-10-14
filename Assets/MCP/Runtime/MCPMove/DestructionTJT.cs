namespace MCP.Runtime.MCPMove.LogicDest
{
using System;
using System.Collections;
using System.Collections.Generic;
    using MCP.Runtime.MCPMove.LogicMove;
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
        public void CheckForDestruct(float time,float duration){
            if(time<duration)return;
            Destruct(GetComponent<EntityMove>());
        }
        private void Destruct(EntityMove bullet)
        {
        
            StartCoroutine(IEDestruct(bullet));
        }

        private IEnumerator IEDestruct(EntityMove bullet)
        {
            TrailRenderer trail=trails[0];
            float time=trail.time;
            
            foreach (var sprite in sprites)
            {
                sprite.enabled = false;
            }
            yield return new WaitForSeconds(time);
      
            onDestroy?.Invoke(bullet);
            // onDestroy=null;

        }
}
}