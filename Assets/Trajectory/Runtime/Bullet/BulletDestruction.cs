namespace Trajectory.Runtime
{
using System;
using System.Collections;
using System.Collections.Generic;
using Trajectory.Runtime;
using UnityEngine;
public class BulletDestruction : MonoBehaviour
{
        private SpriteRenderer[] sprites;
        private TrailRenderer[] trails;
        private event Action<Bullet> onDestroy;

        public void Init(SpriteRenderer[] sprites, TrailRenderer[] trails, Action<Bullet> onDestroy)
        {
            this.sprites = sprites;
            this.trails = trails;
            this.onDestroy = onDestroy;
        }
        public void CheckForDestruct(float time,float duration){
            if(time<duration)return;
            Destruct(GetComponent<Bullet>());
        }
        private void Destruct(Bullet bullet)
        {
        
            StartCoroutine(IEDestruct(bullet));
        }

        private IEnumerator IEDestruct(Bullet bullet)
        {
            foreach (var sprite in sprites)
            {
                sprite.enabled = false;
            }
            yield return new WaitForSeconds(2f);
            onDestroy?.Invoke(bullet);
            onDestroy=null;

        }
}
}