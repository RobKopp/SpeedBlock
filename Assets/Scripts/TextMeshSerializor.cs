using UnityEngine;
using System.Collections.Generic;

public class TextMeshSerializor : MonoBehaviour, ISerializable {

	public Dictionary<string,string> Serialize() {
		Dictionary<string,string> options = new Dictionary<string, string>();

		TextMesh text = gameObject.GetComponent<TextMesh>();

		options.Add("text",text.text);
		options.Add ("color",text.color.ToString());
		options.Add ("csize", text.characterSize.ToString());
		options.Add ("fsize", text.fontSize.ToString());

		return options;
	}
	
	public void DeSerialize(Dictionary<string,object> definition) {
		TextMesh text = gameObject.GetComponent<TextMesh>();

		text.text = (string)definition["text"];
		text.characterSize = float.Parse((string)definition["csize"]);
		text.fontSize = int.Parse((string)definition["fsize"]);
		string color = (string)definition["color"];
		
	}
	
}
