using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoalController : MonoBehaviour, ISerializable {

	public float SpeedLimit;

	public GameObject DestroyedGoal;

	// Use this for initialization
	void OnTriggerEnter(Collider collider) {
		BlockMovementController blm = collider.gameObject.GetComponent<BlockMovementController>();
		if(blm.Speed >= SpeedLimit) {
			Vector3 direction = Vector3.zero;
			float num = 0;
			Destroy(GetComponent<BoxCollider>());
			Destroy(transform.GetChild(0).gameObject);
			if(transform.localScale.z > 1) {
				direction = Vector3.forward;
				num = transform.localScale.z;
			} else if(transform.localScale.x > 1) {
				direction = Vector3.right;
				num = transform.localScale.x;
			}
			Vector3 startingLoc = direction * -num/2.0f;
			for(int i=0;i<num;++i) {
				GameObject obj = Instantiate(DestroyedGoal,transform.position + startingLoc,Quaternion.identity) as GameObject;
				obj.GetComponent<Rigidbody>().AddExplosionForce(50,collider.gameObject.transform.position, 10,5.0f);
				startingLoc += direction;
			}

		} else {
			collider.gameObject.SendMessage("Failed");
		}
	}

	public Dictionary<string,string> Serialize() {
		Dictionary<string,string> options = new Dictionary<string, string>();
		options.Add ("SpeedLimit", SpeedLimit.ToString());
		return options;
	}

	public void DeSerialize(Dictionary<string,object> options) {
		SpeedLimit = float.Parse((string)options["SpeedLimit"]);
	}
}
