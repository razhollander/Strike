using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(JoystickUFO))]
public class JoystickUFOEditor : JoystickEditor
{
    private SerializedProperty _UFOVehicle;

    private SerializedProperty rotateBy;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        _UFOVehicle = serializedObject.FindProperty("_UFOVehicle");
        rotateBy = serializedObject.FindProperty("rotateBy");

    }

    protected override void DrawValues()
    {
        base.DrawValues();
        EditorGUILayout.PropertyField(_UFOVehicle, new GUIContent("UFO Vehicle", "The moving UFO"));
       // EditorGUILayout.PropertyField(rotateBy, new GUIContent("rotateBy", "The moving UFO"));

    }




}
