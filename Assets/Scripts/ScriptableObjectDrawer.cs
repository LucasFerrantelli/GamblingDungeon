using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ScriptableObject), true)]
public class ScriptableObjectDrawer : PropertyDrawer
{
    // Cached scriptable object editor
    private Editor editor = null;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, label, true);
        if (property.objectReferenceValue != null)
        {
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, GUIContent.none);
        }

        if (property.isExpanded)
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUI.indentLevel++;
            if (!editor)
                Editor.CreateCachedEditor(property.objectReferenceValue, null, ref editor);
            if (editor) editor.OnInspectorGUI();

            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
        }
    }
}