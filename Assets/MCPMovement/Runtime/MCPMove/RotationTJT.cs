
namespace MCPMovement.Runtime.MCPMove.LogicRotation
{
    
    using UnityEngine;
    public class RotationTJT : MonoBehaviour
    {
    
        [SerializeField] private Vector3 dirRotate;
        
        public void Init(Vector3 dirRotate)
        {
        
            this.dirRotate = dirRotate;
        }
        private void Update()
        {
            this.transform.Rotate(dirRotate * Time.deltaTime);
        }



    }
  
}