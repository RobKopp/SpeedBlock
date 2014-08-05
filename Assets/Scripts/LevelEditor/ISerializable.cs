using UnityEngine;
using System.Collections.Generic;

public interface ISerializable  {
	
	Dictionary<string,string> Serialize();

	void DeSerialize(Dictionary<string,object> definition);
}
