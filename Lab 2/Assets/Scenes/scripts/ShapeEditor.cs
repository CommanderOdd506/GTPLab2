using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

[CustomEditor(typeof(Shape)), CanEditMultipleObjects]
public class ShapeEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //find size variable on object and draw the property field 
        serializedObject.Update();
        var size = serializedObject.FindProperty("size");

        EditorGUILayout.PropertyField(size);

        //if the size is greater than 2 warn about cube sizing, if it is less than one warn about sphere radius
        if (size.floatValue < 1)
        {
            EditorGUILayout.HelpBox("The spheres' radius cannot be smaller than 1!", MessageType.Warning);
        }
        else if (size.floatValue > 2)
        {
            EditorGUILayout.HelpBox("The cubes' sizes cannot be bigger than 2!", MessageType.Warning);
        }
        serializedObject.ApplyModifiedProperties();

        //set up horizontal selection buttons 
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Select all cubes/spheres"))
        {
            var allShape = GameObject.FindObjectsByType<Shape>();
            var allShapeGameObjects = allShape.Select(enemy => enemy.gameObject).ToArray();
            Selection.objects = allShapeGameObjects;
        }
        if (GUILayout.Button("Clear selection"))
        {
            Selection.objects = new Object[] { (target as Shape).gameObject };
        }
        EditorGUILayout.EndHorizontal();

        // store current color so other UI doesn't render green later. Set background color based on current objects active status
        var cachedColor = GUI.backgroundColor;
        bool active = Selection.activeGameObject.activeSelf;
        if (active)
        {

            GUI.backgroundColor = Color.green;
        }
        else
        {

            GUI.backgroundColor = Color.red;
        }

        // setup for Disable all button which disables all shape objects
        if (GUILayout.Button("Disable/Enable all shape", GUILayout.Height(40)))
        {

            foreach (var shape in GameObject.FindObjectsByType<Shape>(FindObjectsInactive.Include))
            {
                shape.gameObject.SetActive(!shape.gameObject.activeSelf);
            }
        }

        GUI.backgroundColor = cachedColor;
    }
}
