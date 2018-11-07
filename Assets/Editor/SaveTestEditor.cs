using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(SaveTest))]
public class SaveTestEditor : Editor {

	private ReorderableList list;
	private SaveTest saveTestScript;

	private void OnEnable(){
		
		saveTestScript = Selection.activeGameObject.GetComponent<SaveTest>();
		list = new ReorderableList(serializedObject, serializedObject.FindProperty("items"), true, true, true, true);
		list.onRemoveCallback += RemoveCallback;
		list.drawElementCallback += OnDrawCallback;
	}

	private void OnDisable(){
		
		if(list != null){
			list.onRemoveCallback -= RemoveCallback;
			list.onRemoveCallback -= RemoveCallback;
		}
	}
	private void RemoveCallback(ReorderableList list){
		
		if(EditorUtility.DisplayDialog("Warning", "Are you sure?", "Yes", "No")){
			ReorderableList.defaultBehaviours.DoRemoveButton(list);
		}
	}

	private void OnDrawCallback(Rect rect, int index, bool isActive, bool isFocused){

		var item = list.serializedProperty.GetArrayElementAtIndex(index);
		EditorGUI.PropertyField(
			new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight),
			item.FindPropertyRelative("name"),
			GUIContent.none
		);

		EditorGUI.PropertyField(
			new Rect(rect.x + 80, rect.y, 150, EditorGUIUtility.singleLineHeight),
			item.FindPropertyRelative("pos"),
			GUIContent.none
		);
	}

	public override void OnInspectorGUI(){

		//DrawDefaultInspector();

		EditorGUILayout.Space();


		EditorGUILayout.LabelField (GetType().ToString(),EditorUIUtil.guiTitleStyle);
		EditorGUILayout.Space();

		EditorGUILayout.LabelField("Put text here to explain how to use editor Put text here to explain how to use editor Put text here to explain how to use editor",
									EditorUIUtil.guiMessageStyle);

		list.DoLayoutList();

		var saveTestScript = Selection.activeGameObject.GetComponent<SaveTest>();

		EditorGUILayout.BeginVertical();
		if(GUILayout.Button("Save")){
			var text = saveTestScript.Save();

			WriteDate(text);
		}
		if(GUILayout.Button("Load")){
			saveTestScript.Load(ReadDataFromFile());
		}
		EditorGUILayout.EndVertical();

		serializedObject.ApplyModifiedProperties();
	}

	private void WriteDate(string data){

		var path = EditorUtility.SaveFilePanel("Save data", "", "data.txt", "txt");

		using(FileStream fs = new FileStream(path, FileMode.Create)){

			using(StreamWriter writer = new StreamWriter(fs)){
				writer.Write(data);
				AssetDatabase.Refresh();
			}
		}
	}

	private string ReadDataFromFile(){

		var path = EditorUtility.OpenFilePanel("Load Data", "", "txt");

		var reader = new WWW("File:///" + path);

		while(!reader.isDone){

		}

		return reader.text;
	}
}
