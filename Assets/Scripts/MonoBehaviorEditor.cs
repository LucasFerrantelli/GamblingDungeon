using UnityEngine;
using UnityEditor;

/// <summary>
/// Dummy Editor, prevents errors with Scriptable Object drawer
/// </summary>
[CanEditMultipleObjects]
[CustomEditor(typeof(MonoBehaviour), true)]
public class MonoBehaviourEditor : Editor
{

}