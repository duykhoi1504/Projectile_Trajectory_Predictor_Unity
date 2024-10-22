using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLifeTime : MonoBehaviour, ILifeTime
{
#if !UNITY_SERVER
    [SerializeField] private ParticleSystem particle;
#endif
    private float time;



    public float Time
    {
        get
        {
#if !UNITY_SERVER
            return particle.main.startLifetime.constant;
#else
            return 0;
#endif
        }
        set => time = value;
    }

}
