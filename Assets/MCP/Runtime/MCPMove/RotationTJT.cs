
namespace MCP.Runtime.MCPMove.LogicRota
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class RotationTJT : MonoBehaviour
    {
        //    private bool canRotate=true;
        [SerializeField] private Vector3 dirRotate;
        public void Init(Vector3 dirRotate)
        {
            // this.currentCanRotate = canRotate;
            this.dirRotate = dirRotate;
        }
        private void Update()
        {
            this.transform.Rotate(dirRotate * Time.deltaTime);
        }
        // public void CheckCanRotate()
        // {
        //     if (canRotate == false) return;
        //     this.transform.Rotate(dirRotate * Time.deltaTime);
        // }
    }
}