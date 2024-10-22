namespace MCPMovement.Editor
{
using MCPMovement.Runtime.MCPMove.LogicMove;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(EntityMove),true)]
public class SetUpMoveLogic : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Vẽ các thuộc tính mặc định

        EntityMove script = (EntityMove)target;

        if (GUILayout.Button("Set Up"))
        {
            script.SetUp(); // Gọi phương thức SetUp khi nhấn nút
        }
    }
}
}