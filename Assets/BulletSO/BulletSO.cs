
namespace Trajectory.PJT
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    [CreateAssetMenu(fileName = "bullet Data", menuName = "Scriptable Objects/bullet")]
    public class BulletSO : ScriptableObject
    {
        // Start is called before the first frame update
        [SerializeField] private List<BulletSlot> bulletSlots;

        public List<BulletSlot> BulletSlots { get => bulletSlots; set => bulletSlots = value; }
    }


}