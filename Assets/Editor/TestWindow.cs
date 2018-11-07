using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestWindow : EditorWindow {

	private GameObject currentSelection;

	[MenuItem("Window/Test Window")]
	static void Init(){
		var window = GetWindow(typeof(TestWindow)) as TestWindow;
	
		var content = new GUIContent();
		content.text = "Test Window";

		var icon = new Texture2D(16, 16);
		content.image = icon;

		window.titleContent = content;
	}

	private void OnFocus(){

		currentSelection = Selection.activeGameObject;
	}

	private void OnLostFocus(){

		currentSelection = null;
	}

	private void OnGUI(){

		if(currentSelection != null){
			
			EditorGUILayout.BeginVertical();
			EditorGUILayout.LabelField("Currently Selected Object");
			EditorGUILayout.LabelField(currentSelection.name);
			currentSelection.transform.position = EditorGUILayout.Vector3Field("At position : ", currentSelection.transform.position);
			EditorGUILayout.EndVertical();

		}	
		else{
			EditorGUILayout.LabelField("First select a GameObject, then click here");
		}

		DropAreaGUI();
	}

	private void DropAreaGUI(){
		var e = Event.current.type;

		if(e == EventType.DragUpdated){
			DragAndDrop.visualMode = DragAndDropVisualMode.Copy;	
		}
		else if (e == EventType.DragPerform){
			DragAndDrop.AcceptDrag();

			foreach(Object draggedObject in DragAndDrop.objectReferences){
				if(draggedObject is GameObject){
					Debug.Log(draggedObject.name);
				}
			}
			
		}
	}
}
