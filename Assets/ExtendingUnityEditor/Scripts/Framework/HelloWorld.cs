using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomDataStructure{
	public string name;
	public GameObject target;
	public Vector3 position;
}


public class HelloWorld : MonoBehaviour {

	[Range(0f, 10f)]
	public float speed; 
	public Vector3 startPos;

	[Tooltip("What the game object should follow")]
	public HelloWorld target;
	public string message = "Hello world";
	public CustomDataStructure[] customTarget;

	[Header("Array tests")]
	public string[] strings;
	public int[] ints;
	public Vector3[] vectors;
	public GameObject[] gameObjects;
	[HideInInspector]
	public List<string> stringList;
	public Dictionary<string, Vector3> childrenPosition;

	public int life{get; set;}
}
