namespace MCPMovement.Runtime.MCPMove.LogicMove
{
    using UnityEngine;
    public class NormalMove : EntityMove
    {
        protected override void OnUpdate()
        {
            base.OnUpdate();
            Apply(start, target);
        }
        private void Apply(Vector3 start, Vector3 end)
        {
            if (time < duration)
            {
                float linearT = time / duration;
                float heightT = curve.Evaluate(linearT);
                float height = Mathf.Lerp(0f, heightY, heightT);

                transform.position = Vector3.Lerp(start, end, linearT) + new Vector3(0, height, 0);
            }
            
        }
    }
}