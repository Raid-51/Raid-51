using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Object))]
public class ObjectInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Set As Object"))
        {
            Object pickupScript = (Object)target;

            pickupScript.gameObject.tag = "Object";
            pickupScript.SceneNumber = pickupScript.gameObject.scene.buildIndex;
        }
    }
}
