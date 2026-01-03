using UnityEngine;
using UnityEditor;
using System.Reflection;

[InitializeOnLoad]
public class VectorHandleTool
{
    static VectorHandleTool()
    {
        // Nos suscribimos al evento de dibujado de la escena
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private static void OnSceneGUI(SceneView sceneView)
    {
        GameObject targetObj = Selection.activeGameObject;
        if (targetObj == null) return;

        MonoBehaviour[] scripts = targetObj.GetComponents<MonoBehaviour>();

        foreach (var script in scripts)
        {
            if (script == null) continue;

            FieldInfo[] fields = script.GetType().GetFields(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var field in fields)
            {
                HandleAttribute attr = field.GetCustomAttribute<HandleAttribute>();

                if (attr != null && (field.FieldType == typeof(Vector3) || field.FieldType == typeof(Vector2)))
                {
                    DrawHandle(script, field, attr);
                }
            }
        }
    }

    private static void DrawHandle(MonoBehaviour script, FieldInfo field, HandleAttribute attr)
    {
        Vector3 localPos = (Vector3)field.GetValue(script);

        Vector3 worldPos = script.transform.TransformPoint(localPos);

        Handles.color = attr.Color;

        EditorGUI.BeginChangeCheck();
        Vector3 newWorldPos = Handles.PositionHandle(worldPos, Quaternion.identity);

        if (attr.drawLabel)
            Handles.Label(newWorldPos, field.Name);

        if (attr.drawConnectionLine)
            Handles.DrawDottedLine(script.transform.position, newWorldPos, 5f);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(script, "Move Vector Handle");

            Vector3 newLocalPos = script.transform.InverseTransformPoint(newWorldPos);
            field.SetValue(script, newLocalPos);

            EditorUtility.SetDirty(script);
        }
    }
}