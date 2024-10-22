namespace MCPMovement.Runtime.MCPMove.LogicElementTrigger
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ElementTriggerTJT : MonoBehaviour,ILifeTime
    {
        [SerializeField] private Transform headHolder;
        [SerializeField] private Transform trailHolder;
        [SerializeField] private Transform triggerHolder;
        [SerializeField] private List<TrailRenderer> trails;
        [SerializeField] private List<SpriteRenderer> sprites;
#if !UNITY_SERVER

        [SerializeField] private List<ParticleSystem> trailParticals;
#endif
        private float duration;
        [SerializeField] private float timeDelayHit;
        [SerializeField] private bool canTrigger;
        private Vector3 startPos;
        private float totalTime;
        // public float TimeDelayHit { get => timeDelayHit; }
        public float Time { get => timeDelayHit; set => timeDelayHit=value; }


        public void SetUp()
        {
            if (headHolder)
            {

                // this.headHolder = headHolder;
                if (headHolder.childCount > 0 && headHolder != null && headHolder.gameObject.activeSelf)
                {
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
            }
            if (trailHolder)
            {

                if (trailHolder.childCount > 0 && trailHolder != null && trailHolder.gameObject.activeSelf)
                {
                    //////////////////Get trailrenderer in trailHolder///////////////////////
                    trails = new List<TrailRenderer>();
                    foreach (Transform child in trailHolder)
                    {
                        TrailRenderer trail = child.GetComponent<TrailRenderer>();
                        if (trail != null)
                        {
                            trails.Add(trail);
                        }
                    }

                    //////////////////Get Particle in trailHolder///////////////////////
#if !UNITY_SERVER

                    trailParticals = new List<ParticleSystem>();
                    foreach (Transform child in trailHolder)
                    {
                        var pat = child.GetComponent<ParticleSystem>();
                        if (pat != null)
                        {
                            trailParticals.Add(pat);
                        }
                    }
#endif
                }

            }
            // if (triggerHolder)
            // this.triggerHolder = triggerHolder;
            // _triggerHolder = this.triggerHolder;

        }

        public void Init(float duration,float totalTime, Vector3 startPos)
        {
            // this.headHolder = headHolder;
            // this.trailHolder = trailHolder;
            // this.triggerHolder = triggerHolder;
            this.duration = duration;
            this.startPos = startPos;
            this.totalTime = totalTime;
            SetActiveParent(triggerHolder, false);
            SetActiveParent(trailHolder, false);
            SetActiveParent(headHolder, false);

            ResetElement(startPos);

        }

        public void OnUpdate(float time)
        {
            // Cập nhật trạng thái của Element
            if (time > duration)
            {
                SetActiveParent(headHolder, false);
                CheckTrailPartical();
            }
            // Cập nhật trạng thái của Hit Trigger
            if (canTrigger)
            {
                // float totalHitTime = duration + totalTime;
                if (time > duration)
                {
                    SetActiveHit(true);
                }
                if (time >= totalTime)
                {
                    SetActiveHit(false);
                }
            }
        }

        public void CheckTrailPartical()
        {
            if (trailHolder != null)
            {
#if !UNITY_SERVER
                foreach (var child in trailParticals)
                {
                    if (child != null)
                    {
                        child.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                    }
                }
#endif
            }
        }
        public void ResetElement(Vector3 start)
        {
            SetActiveHit(false);
            SetActiveParent(trailHolder, false);
            transform.position = start;
            if (trails != null)
            {
                foreach (var trail in trails)
                {
                    // trail.enabled = false;
                    trail?.Clear();
                }
            }
            SetActiveParent(headHolder, true);
            SetActiveParent(trailHolder, true);

        }


        private void SetActiveParent(Transform parent, bool isActive)
        {
            if (parent != null)
            {
                parent.gameObject.SetActive(isActive);
            }
        }

        public void SetActiveHit(bool isActive)
        {
            if (triggerHolder != null && canTrigger)
                triggerHolder.gameObject.SetActive(isActive);
        }
        #region TEST

        // public void SetActiveHead(bool isActive)
        // {
        //     if (headHolder != null)
        //     {
        //         headHolder.gameObject.SetActive(isActive);
        //     }
        // }

        // public void SetActiveTrail(bool isActive)
        // {
        //     if (trailHolder != null)
        //     {
        //         trailHolder.gameObject.SetActive(isActive);
        //     }
        // }
        // public void SetActiceHit(bool isActive)
        // {
        //     if (triggerHolder != null)
        //     {
        //         hitTriggerTJT.SetActiveHit(isActive);
        //     }
        // }
        #endregion
    }
}