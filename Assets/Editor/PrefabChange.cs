using UnityEngine;
using UnityEditor;
using Trajectory.PJT;

[CustomEditor(typeof(Bullet),true)]
public class PrefabChange : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Setup"))
        {
            SetupComponents();
        }
    }

    private void SetupComponents()
    {
       Bullet bullet = (Bullet)target;        
       bullet.SetUp();

    }
}