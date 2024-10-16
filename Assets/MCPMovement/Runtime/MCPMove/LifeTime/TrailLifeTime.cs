using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailLifeTime : MonoBehaviour, ILifeTime
{
    [SerializeField] TrailRenderer trailRenderer;

    private float time;

    void Awake()
    {
        time = trailRenderer.time;
    }

    public float Time 
    {   get => time; 
        set => time = value; 
    }
}
