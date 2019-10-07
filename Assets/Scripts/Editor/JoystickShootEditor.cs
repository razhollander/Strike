using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(JoystickShoot))]
public class JoystickShootEditor : JoystickEditor
{
    private SerializedProperty shooterObject;
    private SerializedProperty selectedInventoryObject;

    protected override void OnEnable()
    {
        base.OnEnable();
        shooterObject = serializedObject.FindProperty("shooterObject");
        selectedInventoryObject = serializedObject.FindProperty("selectedInventoryObject");
        //axisOptions = serializedObject.FindProperty("axisOptions");
        //snapX = serializedObject.FindProperty("snapX");
        //background = serializedObject.FindProperty("background");
        //handle = serializedObject.FindProperty("handle");
    }
    protected override void DrawValues()
    {
        base.DrawValues();
        EditorGUILayout.PropertyField(shooterObject, new GUIContent("Shooter", "The Objects That Shoots"));
        EditorGUILayout.PropertyField(selectedInventoryObject, new GUIContent("InventoryObjectUI", "The ObjectType to be shot"));
    }
    //public override void OnInspectorGUI()
    //{
    //    base.OnInspectorGUI();


    //}
}
