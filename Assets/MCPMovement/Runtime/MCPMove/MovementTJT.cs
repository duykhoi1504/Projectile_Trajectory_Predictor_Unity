namespace MCPMovement.Runtime.MCPMove.LogicMove
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MovementTJT : MonoBehaviour
    {
        public void OnUpdate(float duration, Vector3 target)
        {

            if (duration <= 0)
            {
                this.transform.position = target;
            }
        }
    }
}