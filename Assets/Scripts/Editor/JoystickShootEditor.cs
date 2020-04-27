using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(JoystickShoot))]
public class JoystickShootEditor : JoystickEditor
{
    private SerializedProperty shooterVacuum;
    private SerializedProperty selectedInventoryObject;
    private SerializedProperty slowTimeDuration;
    private SerializedProperty handlerRadiusToShoot;
    private SerializedProperty _shotObjectHandleImage;
    protected override void OnEnable()
    {
        base.OnEnable();
        shooterVacuum = serializedObject.FindProperty("shooterVacuum");
        selectedInventoryObject = serializedObject.FindProperty("selectedInventoryObject");
        slowTimeDuration = serializedObject.FindProperty("slowTimeDuration");
        handlerRadiusToShoot = serializedObject.FindProperty("handlerRadiusToShoot");
        _shotObjectHandleImage = serializedObject.FindProperty("_shotObjectHandleImage");
    }
    protected override void DrawValues()
    {
        base.DrawValues();
        EditorGUILayout.PropertyField(shooterVacuum, new GUIContent("shooterVacuum", "The Objects That Shoots"));
        EditorGUILayout.PropertyField(selectedInventoryObject, new GUIContent("InventoryObjectUI", "The ObjectType to be shot"));
        EditorGUILayout.PropertyField(_shotObjectHandleImage, new GUIContent("_shotObjectHandleImage", "The Image of the shot Object"));
        EditorGUILayout.PropertyField(slowTimeDuration, new GUIContent("slowTimeDuration", "The time it takes the slowMotion to get to max"));
        EditorGUILayout.PropertyField(handlerRadiusToShoot, new GUIContent("handlerRadiusToShoot", "The % (0-1) the handler need to be from the center to shoot"));
    }



}
