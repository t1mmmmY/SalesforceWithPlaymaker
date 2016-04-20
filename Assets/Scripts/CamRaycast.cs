using UnityEngine;
using System.Collections;

public class CamRaycast : MonoBehaviour {

	public bool chatterOpen = false;

	// Update is called once per frame
	void Update () {

		RaycastHit hit;

		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		if ((Physics.Raycast(transform.position, fwd, out hit)) && ((hit.transform.tag == "Ring") || (hit.transform.tag == "Block")) && (!chatterOpen)) {

			Vector3[] tempStorage = new Vector3[2];
			tempStorage[0] = transform.parent.parent.position;
			tempStorage[1] = hit.point;
			hit.transform.SendMessage ("HitByMainView", tempStorage);

		} else if (Physics.Raycast(transform.position, fwd, out hit)) {
			if (hit.transform.name != "Raycast Stopper") {
				//Debug.Log ("Raycast hit " + hit.transform.name);
			}
		}

	}
}
