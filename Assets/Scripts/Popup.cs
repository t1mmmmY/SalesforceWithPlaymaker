using UnityEngine;
using System.Collections;

public class Popup : MonoBehaviour {

	public TextHelper popupText;
	Animator popupAnim;
	bool lockPopup = false;

	// Use this for initialization
	void Start () {

		popupAnim = GetComponent<Animator>();
		transform.localScale = new Vector3(0, 0, 0);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void doPopup(string inText) {

		transform.localScale = new Vector3(1, 1, 1);
		popupText.setNewText (inText);
		lockPopup = true;
		popupAnim.Play ("PopupAnim");

	}

	public void doPopdown() {

		if (!lockPopup) {
			popupText.stopTyping ();
			popupAnim.Play ("PopdownAnim");
		}

	}

	public void startTyping() {

		lockPopup = false;
		popupText.startTyping ();

	}

	public void killPopup() {

		transform.localScale = new Vector3(0, 0, 0);

	}

}
