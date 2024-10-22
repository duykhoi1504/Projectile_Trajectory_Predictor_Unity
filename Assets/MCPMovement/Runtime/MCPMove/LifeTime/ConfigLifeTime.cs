using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigLifeTime : MonoBehaviour,ILifeTime
{
    [SerializeField] private float config;
  private float time;

    public float Time 
    {   get => config; 
        set => config = value; 
    }
}
