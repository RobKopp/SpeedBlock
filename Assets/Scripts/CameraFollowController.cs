using UnityEngine;
using System.Collections;

public class CameraFollowController : MonoBehaviour {

    public GameObject ObjectToFollow;

    public float FollowThreshold;

	public bool MaintainZ;

    Vector3 followVelocity = Vector3.zero;

    void Update()
    {

        float distanceFrom = (transform.position - ObjectToFollow.transform.position).sqrMagnitude;
        if (distanceFrom > FollowThreshold)
        {
			Vector3 targetPos = ObjectToFollow.transform.position;
			if(MaintainZ) {
				targetPos.z = transform.position.z;
			}
            transform.position = Vector3.SmoothDamp(transform.position, targetPos,ref followVelocity, 0.5f);
        }
        else
        {
            followVelocity = Vector3.zero;
        }
    }
}
