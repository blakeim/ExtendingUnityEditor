using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(HelloWorld))]
public class HelloWorldEditor : Editor {

	public override void OnInspectorGUI(){

		var script = target as HelloWorld;

		script.speed = EditorGUILayout.Slider("Speed", script.speed, 0f, 10f);
		
		script.target = EditorGUILayout.ObjectField("Target", script.target, typeof(HelloWorld), true) as HelloWorld;
		
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("A Button");
		if (GUILayout.Button("Click me")){
			Debug.Log(script.message);
		}

		EditorGUILayout.EndHorizontal();
		
		//DrawDefaultInspector();

		//EditorGUILayout.LabelField("Custom Message", script.message);
	}
}