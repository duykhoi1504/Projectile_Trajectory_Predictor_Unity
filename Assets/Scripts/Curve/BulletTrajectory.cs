using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BulletTrajectory : MonoBehaviour
{
    
    public Transform target;
    public float jumpPower = 5f;
    public int numJumps = 1;
    public float duration = 2f;
     public Ease easeType = Ease.InOutQuad; // Chọn kiểu ease

    void Start()
    {
        if (target != null)
        {
            MoveInParabola();
        }
    }

    void MoveInParabola()
    {
        transform.DOJump(target.position, jumpPower, numJumps, duration)
                 .SetEase(easeType);
    }
}

