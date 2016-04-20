using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Boomlagoon.JSON;
using SFDC;

//Just to be sure that Salesforce is on object
[RequireComponent(typeof(Salesforce))]
public class SalesforceClient : MonoBehaviour {
	public Salesforce sf;
	public string username;
	public string password;
	public string securityToken;
	
	public List <string> subObjectQueries;

	// pass in the reference to the prefab.
	public Transform Ring;
	public Transform Block;
	public Transform blockBox;

	// Use this for initialization
	IEnumerator Start () {

		Cursor.visible = false;

		//init object
		//It's better to attach this component in scene. 
//		sf = gameObject.AddComponent<Salesforce>();
		sf = GetComponent<Salesforce>();
		
		// login
		Debug.Log ("Password + security token = " + password + " " + securityToken);
		sf.login(username, password + securityToken);
		
		// wait for Auth Token
		while(sf.token == null){
			yield return new WaitForSeconds(0.2f);
		}


		string mainQuery = "";
		mainQuery += "SELECT Id, IsDeleted, AccountId, IsPrivate, Name, Description, StageName, Amount, Probability, ExpectedRevenue,";
		mainQuery += " TotalOpportunityQuantity, CloseDate, Type, NextStep, LeadSource, IsClosed, IsWon, ForecastCategory,";
		mainQuery += " ForecastCategoryName, CampaignId, HasOpportunityLineItem, Pricebook2Id, OwnerId, CreatedDate, CreatedById,";
		mainQuery += " LastModifiedDate, LastModifiedById, SystemModstamp, LastActivityDate, FiscalQuarter, FiscalYear, Fiscal,";
		mainQuery += " LastViewedDate, LastReferencedDate, Urgent__c,";
		//Account
		mainQuery += " Account.Id, Account.Name, Account.AccountNumber, Account.Description, Account.Type, Account.Industry, Account.CustomerPriority__c, Account.UpsellOpportunity__c, Account.Priority__c,";

		//OppProducts
		mainQuery += " (SELECT Id, OpportunityId, SortOrder, PricebookEntryId, Product2.Name, ProductCode, Name, Quantity, TotalPrice, UnitPrice, ListPrice, ServiceDate, Description, Priority__c FROM OpportunityLineItems ORDER BY Priority__c DESC),";

		// Campaigns
		mainQuery += " Campaign.Id, Campaign.Name, Campaign.AmountWonOpportunities, Campaign.AmountAllOpportunities, Campaign.NumberOfWonOpportunities, Campaign.NumberOfOpportunities, Campaign.NumberOfResponses, Campaign.NumberOfContacts, Campaign.NumberOfConvertedLeads, Campaign.NumberOfLeads, Campaign.Description, Campaign.IsActive, Campaign.NumberSent, Campaign.ExpectedResponse, Campaign.ActualCost, Campaign.BudgetedCost, Campaign.ExpectedRevenue, Campaign.EndDate, Campaign.StartDate, Campaign.Status, Campaign.Type, Campaign.ParentId, Campaign.OwnerId, Campaign.Priority__c, ";

		// Contracts
		mainQuery += " Contract.Id, Contract.Status, Contract.StartDate, Contract.EndDate, Contract.ContractTerm, Contract.ContractNumber, Contract.Description, Contract.SpecialTerms, Contract.Priority__c ";

		mainQuery += " FROM Opportunity";

		sf.query(mainQuery);

		// wait for query results
		while(sf.response == null){
			yield return new WaitForSeconds(0.1f);
		}

		Debug.Log ("Response from Salesforce: " + sf.response);
		
		// Extract the JSON encoded value for the Store the procedure ID (Case) in a field 
		// We are using the free add-in from the Unity3D Asset STore called BoomLagoon.
		
		// Using BoomLagoon, create a JSON Object from the Salesforce response.
		JSONObject json = JSONObject.Parse(sf.response);


		JSONArray records = json.GetArray ("records");

		Debug.Log ("records = " + records);

		OpportunityUtil.BuildOpportunityRings(records,transform,Ring,Block,blockBox);

		
	}


	// Update is called once per frame
	void Update () {

		if (Input.GetKey(KeyCode.Escape)) {
			Application.Quit ();
		}
		
	}

	public void getChatterFeed(string id, WindowHandler handler) {
		if (sf.token != null) {
			StartCoroutine(getChatter(id, handler));
		}
	}
	
	IEnumerator getChatter(string id, WindowHandler handler) {

		// Waiting two seconds to let the typing stop before calling out (causes hiccup in VR)
		yield return new WaitForSeconds(2f);

		sf.getChatterFeed (id);
		
		// wait for query results
		while(sf.response == null){
			yield return new WaitForSeconds(0.1f);
		}
		
		Debug.Log ("Chatter Response from Salesforce: " + sf.response);
		
		JSONObject json = JSONObject.Parse(sf.response);
		
		JSONArray records = json.GetArray ("elements");
		
		Debug.Log ("records = " + records);

		handler.setChatterArray(records);
		
	}

	public void getChatterPhoto(string url, ChatterItem item) {
		StartCoroutine(getPhoto(url, item));
	}
	
	IEnumerator getPhoto(string url, ChatterItem item) {
		
		sf.getChatterPhoto (url);
		
		// wait for query results
		while(sf.response == null){
			yield return new WaitForSeconds(0.1f);
		}

		item.photoImage.enabled = true;
		item.photoImage.texture = sf.sfTexture;
		item.photoRetrieved = true;
		
	}

	public void doApprovalProcess(string objectId, string processId, string action, WindowHandler handler) {

		string comment = (action == "Approve") ? "Approved via VRpportunity!" : "Rejected via VRpportunity!";
		JSONObject request = new JSONObject();
		request.Add ("actionType", action);
		request.Add ("contextId", processId);
		request.Add ("comments", comment);

		JSONArray requestArray = new JSONArray();
		requestArray.Add (new JSONValue(request));

		JSONObject jsonBody = new JSONObject();
		jsonBody.Add ("requests", new JSONValue(requestArray));

		string jsonProcess = jsonBody.ToString();

		JSONObject chatter = new JSONObject();
		chatter.Add ("feedElementType", "FeedItem");
		chatter.Add ("subjectId", objectId);

		JSONObject chatterBody = new JSONObject();
		chatterBody.Add ("type", "Text");
		chatterBody.Add ("text", "Approval Process " + comment);

		JSONArray segments = new JSONArray();
		segments.Add (new JSONValue(chatterBody));

		JSONObject chatterSegments = new JSONObject();
		chatterSegments.Add ("messageSegments", new JSONValue(segments));

		chatter.Add ("body", new JSONValue(chatterSegments));

		string jsonChatter = chatter.ToString ();

		StartCoroutine(handleApprovalProcess(jsonProcess, jsonChatter, handler));

	}

	IEnumerator handleApprovalProcess(string processBody, string chatterBody, WindowHandler handler) {

		sf.handleApprovalProcess (processBody);

		// wait for query results
		while(sf.response == null){
			yield return new WaitForSeconds(0.1f);
		}

		Debug.Log ("Process Response from Salesforce: " + sf.response);

		sf.postToChatter (chatterBody);

		// wait for query results
		while(sf.response == null){
			yield return new WaitForSeconds(0.1f);
		}
		
		Debug.Log ("Chatter Post Response from Salesforce: " + sf.response);

		handler.reloadChatter();

	}

}
