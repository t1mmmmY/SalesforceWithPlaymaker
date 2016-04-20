using UnityEngine;
using System.Collections;

public class CamController : MonoBehaviour {

	public float speed = 10.0F;
	public float rotationSpeed = 100.0F;
	public float verticalSpeed = 2.0F;
	public float v = 0;

	void Update(){
		//v = v + verticalSpeed * Input.GetAxis("Mouse ScrollWheel");
		//transform.position = new Vector3(0,v,0);
		//Debug.Log ("test cam name" + transform.GetComponents<Camera>());
	}

} 