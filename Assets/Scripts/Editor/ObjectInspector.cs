using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(Object))]
public class ObjectInspector : Editor
{
    private string SceneName;
    private int LastSceneNumber;

    // Þetta gerir custom inspector fyrir Object scriptuna
    public override void OnInspectorGUI()
    {
        // Þetta loadar venjulega inspectorinum
        base.OnInspectorGUI();

        // Sækja Object scriptuna til þess að geta unnið með hana
        Object pickupScript = (Object)target;

        // Þetta stillir scene númerið automatically ef scenið sem game objectinn er í er ekki það sama og hann er skráður á
        if (pickupScript.gameObject.scene.buildIndex != pickupScript.SceneNumber)
        {
            pickupScript.SceneNumber = pickupScript.gameObject.scene.buildIndex;// Upfæra SceneNumber á Object scriptunni
            SceneName = SceneManager.GetSceneByBuildIndex(pickupScript.SceneNumber).name;// Uppfæra SceneName
        }

        // Passa að það sé eitthvað í Scene Name barinum
        if (SceneName == null)
            SceneName = SceneManager.GetSceneByBuildIndex(pickupScript.SceneNumber).name;// Uppfæra SceneName

        // Eitt línubil
        GUILayout.Space(EditorGUIUtility.singleLineHeight);
        // Gera Scene Name barinn
        EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Scene Name", GUILayout.Width(EditorGUIUtility.labelWidth));
            EditorGUILayout.SelectableLabel(SceneName, EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));
        EditorGUILayout.EndHorizontal();

        // Þetta stillir Object tagið automatically þegar Object scriptunni er bætt við eitthvern hlut
        if (pickupScript.Initialized == false)
        {
            pickupScript.gameObject.tag = "Object";
            pickupScript.Initialized = true;
            Debug.Log("Object initialized");
        }
    }
}
