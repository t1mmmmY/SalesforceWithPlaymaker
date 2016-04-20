using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Boomlagoon.JSON;

public class ChatterItem : MonoBehaviour {

	public Text nameText;
	public Text bodyText;
	public RawImage photoImage;
	public Button approveButton;
	public Button rejectButton;

	string photoUrl;
	public bool photoRetrieved = false;

	WindowHandler handler;
	bool isApproval = false;
	string approvalProcessId;

	// Use this for initialization
	void Start () {

		photoImage.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void init(int num) {

		nameText.text = "Item #" + num;
		bodyText.text = "This is the text for Chatter Item #" + num;

	}

	public void initMain(JSONObject rec, WindowHandler inHandler) {

		JSONObject actor = rec.GetObject("actor");
		string name = actor.GetString ("displayName");
		nameText.text = name;

		JSONObject feedBody = rec.GetObject ("body");
		string body = feedBody.GetString ("text");
		if (body == null) {
			JSONObject header = rec.GetObject ("header");
			body = header.GetString ("text");
		}
		bodyText.text = body;

		JSONObject photo = actor.GetObject ("photo");
		photoUrl = photo.GetString ("largePhotoUrl");

		JSONObject capabilities = rec.GetObject ("capabilities");
		JSONValue approval = capabilities.GetValue ("approval");
		if (approval != null) {
			string id = approval.Obj.GetString ("id");
			if (id != null) {
				enableApproval(approval.Obj.GetString ("id"));
			}
		}

		if (!isApproval) {
			approveButton.enabled = false;
			approveButton.GetComponent<CanvasRenderer>().SetAlpha(0);
			rejectButton.enabled = false;
			rejectButton.GetComponent<CanvasRenderer>().SetAlpha(0);
		}

		handler = inHandler;
		photoRetrieved = false;

	}

	public void getPhoto() {

		if (photoUrl != null) {
			SalesforceClient client = GameObject.FindGameObjectWithTag("Salesforce").GetComponent<SalesforceClient>();
			client.getChatterPhoto (photoUrl, this);
		}

	}

	void enableApproval(string id) {

		approveButton.enabled = true;
		approveButton.GetComponent<CanvasRenderer>().SetAlpha(255);
		rejectButton.enabled = true;
		rejectButton.GetComponent<CanvasRenderer>().SetAlpha(255);

		approvalProcessId = id;
		isApproval = true;

		Debug.Log (approveButton.isActiveAndEnabled + " - " + rejectButton.isActiveAndEnabled);

	}

	public void handleProcess(string action) {

		SalesforceClient client = GameObject.FindGameObjectWithTag("Salesforce").GetComponent<SalesforceClient>();
		client.doApprovalProcess (handler.objectId, approvalProcessId, action, handler);

	}

}
