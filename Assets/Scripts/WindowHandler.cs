using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Boomlagoon.JSON;

public class WindowHandler : MonoBehaviour {

	public float maxDistance = 25f;
	public float chatterWaitTime = 0.15f;
	public Window frontWindow;
	public Window rightWindow;
	public Window leftWindow;
	public Button chatterButton;
	public GameObject closeChatterButton;
	public SubRingDiskController currentSubRingController;
	public CamRaycast raycaster;

	public string objectId;
	public string objectType;
	public int order;

	bool windowsOpen = false;

	public GameObject[] panels;
	bool chatterOpen = false;
	bool chatterFetched = false;

	public JSONArray chatterArray;

	// Use this for initialization
	void Start () {

		closeChatterButton.transform.localScale = new Vector3(0, 0, 0);
	
	}
	
	// Update is called once per frame
	void Update () {

		if (windowsOpen || chatterOpen) {
			checkDistanceFromPlayer();
		}

		if (Input.GetButtonUp ("Fire3")) {
			if (chatterOpen) {
				closeChatter();
			} else {
				//StartCoroutine(fakeFeed());
			}
		}

		if (Input.GetButtonUp ("Fix")) {
			transform.position = new Vector3(0f, -25f, 0f);
		}
	
	}

	public void showWindows(string windowName, string[] labels, string[] fields, string description, string leftTitle, string leftText) {

		windowsOpen = true;
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		transform.position = player.transform.position;
		transform.rotation = player.transform.rotation;
		disableChatter();
		frontWindow.doFieldWindow (windowName, labels, fields);

		if (description != null) {
			rightWindow.doFreeformWindow ("Description", description);
		}

		if ((leftTitle != null) && (leftTitle != "")) {
			leftWindow.doFreeformWindow (leftTitle, leftText);
		}

	}

	public void closeWindows() {

		windowsOpen = false;
		frontWindow.closeWindow ();
		rightWindow.closeWindow ();
		leftWindow.closeWindow ();

	}

	public void resetPriority(){

		if (objectType == "Account") {

			currentSubRingController.accounts[0].Priority = 2f;
			currentSubRingController.change = true;

		} else if (objectType == "Campaign") {
		
			currentSubRingController.campaigns[0].Priority = 2f;
			currentSubRingController.change = true;

		} else if (objectType == "OpportunityProduct") {
		
			currentSubRingController.opportuniutyProducts[order].Priority = 2f;
			currentSubRingController.change = true;

		}
 
	}

	void checkDistanceFromPlayer() {

		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		if (Vector3.Distance (player.transform.position, transform.position) > maxDistance) {
			if (windowsOpen) {
				closeWindows();
			}
			if (chatterOpen) {
				closeChatter();
			}
		}

	}

	public void getChatter(string id) {

		SalesforceClient client = GameObject.FindGameObjectWithTag("Salesforce").GetComponent<SalesforceClient>();
		client.getChatterFeed (id, this);
		objectId = id;

	}

	public void setChatterArray(JSONArray records) {

		chatterArray = records;
		chatterFetched = true;

		if (records.Length > 0) {
			enableChatter();
		}

	}

	public void showChatter() {

		closeWindows();

		closeChatterButton.transform.localScale = new Vector3(1, 1, 1);
		
		StartCoroutine(popinFeed());

	}

	IEnumerator fakeFeed() {

		chatterOpen = true;
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		transform.position = player.transform.position;
		transform.rotation = player.transform.rotation;
		
		for (int i = 0;i < 15;i++) {
			
			int panelNum = i / 3;
			int itemNum = i % 3;
			float offset = Random.Range (-0.03f, 0.03f);
			
			yield return new WaitForSeconds(chatterWaitTime);
			
			GameObject obj = (GameObject)Instantiate(Resources.Load ("Chatter Item"));
			obj.transform.SetParent (panels[panelNum].transform);
			ChatterItem newItem = obj.GetComponent<ChatterItem>();
			newItem.init (i);
			
			obj.transform.localPosition = new Vector3(offset, 0.35f - (itemNum * 0.29f) + (offset / 10f), offset);
			obj.transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
			obj.transform.localRotation = Quaternion.identity;
			
			newItem.GetComponent<Animator>().Play ("Chatter Item Open");
			newItem.GetComponent<AudioSource>().Play ();
			
		}
		
	}
	
	IEnumerator popinFeed() {

		yield return new WaitForSeconds(0.5f);

		int num = 0;
		chatterOpen = true;
		raycaster.chatterOpen = true;
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		transform.position = player.transform.position;
		transform.rotation = player.transform.rotation;
		
		foreach(JSONValue row in chatterArray) {
			
			int panelNum = num / 3;
			int itemNum = num % 3;
			float offset = Random.Range (-0.03f, 0.03f);
			
			yield return new WaitForSeconds(chatterWaitTime);
			
			JSONObject rec = JSONObject.Parse (row.ToString ());
			GameObject obj = (GameObject)Instantiate(Resources.Load ("Chatter Item"));
			obj.transform.SetParent (panels[panelNum].transform);
			ChatterItem newItem = obj.GetComponent<ChatterItem>();
			newItem.initMain(rec, this);
			
			obj.transform.localPosition = new Vector3(offset, 0.35f - (itemNum * 0.29f) + (offset / 10f), offset);
			obj.transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
			obj.transform.localRotation = Quaternion.identity;
			
			newItem.GetComponent<Animator>().Play ("Chatter Item Open");
			newItem.GetComponent<AudioSource>().Play ();
			
			num++;
			
		}
		
		GameObject[] items = GameObject.FindGameObjectsWithTag ("Chatter Item");
		foreach(GameObject item in items) {

			ChatterItem chatterItem = item.GetComponent<ChatterItem>();
			chatterItem.getPhoto ();

			while (!chatterItem.photoRetrieved) {
				yield return new WaitForSeconds(0.1f);
			}
			
		}

	}

	void closeChatter() {

		GameObject[] items = GameObject.FindGameObjectsWithTag ("Chatter Item");
		foreach(GameObject item in items) {
			Destroy(item);
		}

		closeChatterButton.transform.localScale = new Vector3(0, 0, 0);
		chatterOpen = false;
		raycaster.chatterOpen = false;

	}

	public void chatterToWindow() {

		StartCoroutine(repoenWindows());

	}

	IEnumerator repoenWindows() {

		yield return new WaitForSeconds(0.2f);

		closeChatter();

		frontWindow.reopenWindow ();
		rightWindow.reopenWindow ();
		leftWindow.reopenWindow ();

		windowsOpen = true;

	}

	void disableChatter() {

		chatterButton.enabled = false;
		chatterButton.GetComponent<CanvasRenderer>().SetAlpha(0);

	}

	void enableChatter() {

		chatterButton.enabled = true;
		chatterButton.GetComponent<CanvasRenderer>().SetAlpha(255);

	}

	public void reloadChatter() {
		StartCoroutine(reopenChatter());
	}

	IEnumerator reopenChatter() {

		closeChatter();
		disableChatter();
		chatterFetched = false;
		getChatter(objectId);

		while (!chatterFetched) {
			yield return new WaitForSeconds(0.2f);
		}

		showChatter();

	}

}
