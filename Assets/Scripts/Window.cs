using UnityEngine;
using System.Collections;

public class Window : MonoBehaviour {

	public TextHelper windowName;
	public TextHelper[] labels;
	public TextHelper[] fields;
	public TextHelper freeformText;
	Animator windowAnim;
	bool windowValid = false;

	// Use this for initialization
	void Start () {

		windowAnim = GetComponent<Animator>();
		transform.localScale = new Vector3(0, 0, 0);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void doFieldWindow(string inWindowName, string[] inLabels, string[] inFields) {

		windowName.setNewText (inWindowName);

		int i;

		for (i = 0;i < labels.Length;i++) {
			labels[i].setMainText ("");
			fields[i].setMainText ("");
			fields[i].setNewText ("");
		}

		for (i = 0;i < inLabels.Length;i++) {
			labels[i].setMainText (inLabels[i] + ":");
		}
		for (i = 0;i < inFields.Length;i++) {
			fields[i].setNewText (inFields[i]);
		}

		transform.localScale = new Vector3(1, 1, 1);
		windowAnim.Play ("WindowUpAnim");

		windowValid = true;

	}

	public void doFreeformWindow(string inWindowName, string inFreeformText) {

		windowName.setMainText (inWindowName);
		freeformText.setMainText (inFreeformText);

		transform.localScale = new Vector3(1, 1, 1);
		windowAnim.Play ("WindowUpAnim");

		windowValid = true;

	}

	public void startTyping() {

		if (windowName.newText != "") {
			windowName.startTyping ();
		}

		if (fields.Length > 0) {
			for (int i = 0;i < fields.Length;i++) {
				fields[i].startTyping ();
			}
		}

	}

	public void stopTyping() {

		windowName.stopTyping ();

		if (fields.Length > 0) {
			for (int i = 0;i < fields.Length;i++) {
				fields[i].stopTyping ();
			}
		}

	}

	public void closeWindow() {

		windowAnim.Play ("WindowDownAnim");

	}

	public void reopenWindow() {

		if (windowValid) {
			transform.localScale = new Vector3(1, 1, 1);
			windowAnim.Play ("WindowUpAnim");
		}

	}

	public void killWindow() {

		transform.localScale = new Vector3(0, 0, 0);

	}

}
