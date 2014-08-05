using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class GameObjectDefinition : MonoBehaviour {

	public virtual Dictionary<string,object> GetSerialization() {

		Dictionary<string,object> objectProperties = new Dictionary<string,object>();

		string objName = gameObject.name;
		Object prefab = PrefabUtility.GetPrefabParent(gameObject);
		if(prefab != null) {
			objName = prefab.name;
		}
		objectProperties.Add("name", objName);
		objectProperties.Add ("friendlyName", gameObject.name);
		objectProperties.Add ("pos", transform.position);
		objectProperties.Add ("scale", transform.localScale);
		objectProperties.Add ("rot", transform.localEulerAngles);


		Dictionary<string, object> compDefs = new Dictionary<string, object>();
		Component[] comps = GetComponents(typeof(Component));
		foreach(Component comp in comps) {
			ISerializable serComp = comp as ISerializable;
			if(serComp != null) {
				compDefs.Add (comp.GetType ().Name, serComp.Serialize());
			}
		}
		if(compDefs.Count > 0) {
			objectProperties.Add ("components", compDefs);
		}


		if(transform.childCount > 0) {
			ArrayList children = new ArrayList();
			foreach(Transform child in transform) {
				GameObjectDefinition def = child.GetComponent<GameObjectDefinition>();
				if(def != null) {
					children.Add(def.GetSerialization());
				}
			}
			if(children.Count > 0) {
				objectProperties.Add ("children", children);
			}
		}

		return objectProperties;
	}
}
