using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(HelloWorld))]
public class HelloWorldEditor : Editor {

	private bool visible;

	public override void OnInspectorGUI(){

		var script = target as HelloWorld;
		EditorGUILayout.Space();


		EditorGUILayout.LabelField (GetType().ToString(),EditorUIUtil.guiTitleStyle);
		EditorGUILayout.Space();

		EditorGUILayout.LabelField("Put text here to explain how to use editor Put text here to explain how to use editor Put text here to explain how to use editor",
									EditorUIUtil.guiMessageStyle);

		EditorGUILayout.BeginVertical("box");
		EditorGUILayout.Space();
		script.speed = EditorGUILayout.Slider("Speed", script.speed, 0f, 10f);
		script.target = EditorGUILayout.ObjectField("Target", script.target, typeof(HelloWorld), true) as HelloWorld;
		EditorGUILayout.Space();
		EditorGUILayout.EndVertical();

		if(script.target == null){
			EditorGUILayout.HelpBox("There is an error.", MessageType.Error);
		}	

		EditorGUILayout.Space();

		EditorGUILayout.BeginVertical("box");
		EditorGUI.indentLevel++;
		visible = EditorGUILayout.Foldout(visible, "Options");

		if(visible){
			
			EditorGUI.indentLevel++;

			var props = new []{"startPos"};

			foreach(string s in props){
				var sProp = serializedObject.FindProperty(s);
				var guiContent = new GUIContent();
				guiContent.text = sProp.displayName;
				EditorGUILayout.PropertyField(sProp, guiContent);
				DisplayPropError(sProp);
			}

			EditorGUI.indentLevel--;
		}
		EditorGUI.indentLevel--;
		EditorGUILayout.EndVertical();

		EditorGUILayout.Space();
		EditorGUILayout.BeginVertical();
		EditorGUILayout.LabelField("A Button");
		if (GUILayout.Button("Open Test Window")){
			EditorWindow.GetWindow(typeof(TestWindow));
		}

		EditorGUILayout.EndVertical();

		serializedObject.ApplyModifiedProperties();		
	}

	private void DisplayPropError(SerializedProperty prop){

		var empty = false;

		switch(prop.type){
			case("string"):
				empty = prop.stringValue == "";
				break;
		}

		if(empty){
			DisplayErrors(prop.displayName);
		}
	}

	private void DisplayErrors(string name){
		
		var message = name + " field can not be empty";
		EditorGUILayout.HelpBox(message, MessageType.Error);
	}
}