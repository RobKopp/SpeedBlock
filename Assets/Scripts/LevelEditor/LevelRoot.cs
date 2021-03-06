﻿using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MiniJSON;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class LevelRoot : MonoBehaviour {
	

	static List<GameObject> mapping = new List<GameObject>();

	public string FileName;

	public static string FILE_PATH = "Assets/Levels/";


	static LevelRoot() {
#if UNITY_EDITOR
		GameObject[] objs = Resources.LoadAll<GameObject>("LevelAssets");
		mapping.AddRange(objs);
#endif
	}

	public void Awake() {
#if !UNITY_EDITOR
		GameObject[] objs = Resources.LoadAll<GameObject>("LevelAssets");
		mapping.AddRange(objs);
#endif
	}

	public void Serialize () {

		StreamWriter sw = new StreamWriter(FILE_PATH+FileName);
		Debug.Log ("Exporting Level...");
		string serializedLevel = "";
		ArrayList level = new ArrayList();
		GameObject levelRoot = GameObject.Find("LevelRoot");
		foreach(Transform child in levelRoot.transform) {
			GameObjectDefinition def = child.GetComponent<GameObjectDefinition>();

			if(def != null) {
				level.Add (def.GetSerialization());
			}
		}
		if(level.Count <=0){
			Debug.Log ("Level has no definition!");
		} else {
			serializedLevel += Json.Serialize(level);
			sw.Write(serializedLevel);
			sw.Flush();
			sw.Close();
			Debug.Log ("Level Saved");
		}

	}

	GameObject GetGameObjectByName(string name) {
		foreach(GameObject obj in mapping) {
			if(obj.name == name) {
				return obj;
			}
		}

		return null;
	}

	GameObject createChild(Dictionary<string,object> itemDef) {
		if(itemDef.ContainsKey("name")) {
			string name = (string)itemDef["name"];
			GameObject prefab = GetGameObjectByName(name);
			GameObject itemObj;
			if(prefab != null) {
#if UNITY_EDITOR
				itemObj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
#endif
#if !UNITY_EDITOR
				itemObj = Instantiate(prefab) as GameObject;
#endif
			} else {
				itemObj = new GameObject();
				itemObj.AddComponent<GameObjectDefinition>();
			}
			if(itemObj != null) {
				itemObj.name = (string)itemDef["friendlyName"];
				itemObj.transform.position = Json.ParseVector3((string)itemDef["pos"]);
				itemObj.transform.rotation = Quaternion.Euler(Json.ParseVector3((string)itemDef["rot"]));
				itemObj.transform.localScale = Json.ParseVector3((string)itemDef["scale"]);
				if(itemDef.ContainsKey("components")) {
					Dictionary<string,object> comps = itemDef["components"] as Dictionary<string, object>;
					foreach(string key in comps.Keys) {
						Dictionary<string,object> options = (Dictionary<string,object>)comps[key];
						((ISerializable)itemObj.GetComponent(key)).DeSerialize(options);
					}
				}
				if(itemDef.ContainsKey("children")) {
					List<object> children = itemDef["children"] as List<object>;
					foreach(object child in children) {
							Dictionary<string, object> childDef = child as Dictionary<string,object>;
							if(childDef != null) {
								GameObject myChild = createChild(childDef);
								if(myChild != null) {
									myChild.transform.parent = itemObj.transform;
								}
							}
					}
				}
				return itemObj;
			} else {
				Debug.LogWarning("Name not found in mapping: "+itemDef["name"]);
			}
		} else {
			Debug.LogWarning ("Name not found in definition.");
		}

		return null;
	}

	public void DestroyChildren(Transform parent) {
		List<GameObject> children = new List<GameObject>();
		foreach(Transform child in parent) {
			if(child.transform.childCount > 0) { 
				DestroyChildren(child);
			}
			children.Add (child.gameObject);
		}
		children.ForEach(child => DestroyImmediate(child));
	}
	

	public void LoadLevel(string levelFileName) {

		string fileToLoad = FileName;
		//IF we don't include a filename lets use the one defined in the editor
		if(levelFileName != "") {
			fileToLoad = levelFileName;
		}

		StreamReader sr = new StreamReader(FILE_PATH+fileToLoad);
		string levelDefinition = sr.ReadToEnd();
		sr.Close();

		GameObject root = GameObject.Find ("LevelRoot");

		if(root== null) {
			root = new GameObject();
		} else {
			if(root.transform.childCount > 0) {
				DestroyChildren(root.transform);
			}
		}
		
		root.name = "LevelRoot";

		List<object> level = (List<object>)MiniJSON.Json.Deserialize(levelDefinition);

		foreach(object item in level) {
			Dictionary<string,object> itemDef = item as Dictionary<string,object>;
			GameObject child = createChild(itemDef);
			child.transform.parent = root.transform;
		}
	}
}
