using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextHelper : MonoBehaviour {

	public float typeTime = 0.04f;
	public string newText;
	Text thisText;
	AudioSource thisAudio;
	bool typing;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void initComponents() {

		thisText = GetComponent<Text>();
		thisAudio = GetComponent<AudioSource>();
		thisText.text = "";
		typing = false;

	}

	public void setMainText(string inText) {

		if (thisText == null) {
			initComponents();
		}
		thisText.text = inText;

	}

	public void setNewText(string inText) {

		if (thisText == null) {
			initComponents();
		}
		newText = inText;
		thisText.text = "";

	}

	public void startTyping() {

		if (thisText == null) {
			initComponents();
		}
		thisText.text = "";

		if (newText != null) {
			typing = true;
			StartCoroutine("typeNewText");
		}

	}

	public void stopTyping() {

		typing = false;
	}

	IEnumerator typeNewText() {

		for (int i = 0;i <= newText.Length;i++) {
			if (!typing) {
				break;
			}
			thisText.text = newText.Substring(0, i);
			if (thisText.text.Length != newText.Length) {
				thisText.text = thisText.text + "|";
			}

			if (thisAudio != null) {
				if (i == 0) {
					thisAudio.volume = 0;
				} else {
					thisAudio.volume = 1;
				}
				thisAudio.Play();
			}

			yield return new WaitForSeconds(typeTime);
		}

		typing = false;

	}

}
