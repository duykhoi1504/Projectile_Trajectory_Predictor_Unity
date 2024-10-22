using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailLifeTime : MonoBehaviour, ILifeTime
{
    [SerializeField] TrailRenderer trailRenderer;



  public float Time 
{ 
    get => trailRenderer.time; 
    set => trailRenderer.time = value; 
}
}
