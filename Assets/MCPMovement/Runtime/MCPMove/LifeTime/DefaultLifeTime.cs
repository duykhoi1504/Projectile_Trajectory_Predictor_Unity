using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultLifeTime : MonoBehaviour,ILifeTime
{
    private float defaultTime = 1f;  // Giá trị mặc định

    private float time;
    private void Awake()
    {
        time = defaultTime;
    }
    public float Time
    {
        get => defaultTime;
        set => time = value;
    }
}
