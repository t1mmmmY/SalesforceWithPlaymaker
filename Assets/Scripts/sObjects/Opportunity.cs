using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Boomlagoon.JSON;

public class Opportunity : ScriptableObject {

	public string Id{ get; set;}
	public string accountName{ get; set; }
	public string amount{ get; set; }
	public string closeDate{ get; set; }
	//public string contract{ get; set; }
	public string createdBy{ get; set; }
	public string description{ get; set; }
	public double expectedRevenue{ get; set; }
	public string forecastCategoryName{ get; set; }
	public string lastModifiedBy{ get; set; }
	public string leadSource{ get; set; }
	public string nextStep{ get; set; }
	public string oppName{ get; set; }
	public string owner{ get; set; }
	public string pricebook2{ get; set; }
	public bool isPrivate{ get; set; }
	public double probability{ get; set; }
	public double quantity{ get; set; }
	public string stageName{ get; set; }
	public string type{ get; set; }
	public float urgent {get; set;}
	public Account account {get; set;}
	public List <OpportunityProduct> oppProducts {get; set;}
	public Campaign campaign {get; set;}
	public Contract contract {get; set;}


	public void init(JSONObject json){
		if(json.GetValue("Id") != null ){this.Id = json.GetString("Id");}
		if(json.GetValue("Acount") != null ){this.accountName = json.GetString("Acount");}
		if(json.GetValue("Amount") != null ){this.amount = json.GetString("Amount");}
		if(json.GetValue("CloseDate") != null ) {this.closeDate = json.GetString("CloseDate");}
		//if(json.GetValue("Contract") != null ) {this.contract = json.GetString("Contract");}
		if(json.GetValue("CreatedBy") != null ) {this.createdBy = json.GetString("CreatedBy");}
		if(json.GetValue("Description") != null ) {this.description = json.GetString("Description");}
		if(json.GetValue("ExpectedRevenue") != null ) {this.expectedRevenue = json.GetNumber("ExpectedRevenue");}
		if(json.GetValue("ForecastCategoryName") != null ) {this.forecastCategoryName = json.GetString("ForecastCategoryName");}
		if(json.GetValue("LastModifiedBy") != null ) {this.lastModifiedBy = json.GetString("LastModifiedBy");}
		if(json.GetValue("LeadSource") != null ) {this.leadSource = json.GetString("LeadSource");}
		if(json.GetValue("NextStep") != null ) {this.nextStep = json.GetString("NextStep");}
		if(json.GetValue("Name") != null ) {this.oppName = json.GetString("Name");}
		if(json.GetValue("Owner") != null ) {this.owner = json.GetString("Owner");}
		if(json.GetValue("Pricebook2") != null ) {this.pricebook2 = json.GetString("Pricebook2");}
		if(json.GetValue("IsPrivate") != null ) {this.isPrivate = json.GetBoolean("IsPrivate");}
		if(json.GetValue("Probability") != null ) {this.probability = json.GetNumber("Probability");}
		if(json.GetValue("TotalOpportunityQuantity") != null ) {this.quantity = json.GetNumber("TotalOpportunityQuantity");}
		if(json.GetValue("StageName") != null ) {this.stageName = json.GetString("StageName");}
		if(json.GetValue("Type") != null ) {this.type = json.GetString("Type");}
		if(json.GetValue("Urgent__c") != null ) {this.urgent = (float)json.GetNumber("Urgent__c");}
		if(json.GetValue("Urgent__c") != null ) {this.urgent = (float)json.GetNumber("Urgent__c");}

		//create and add account.
		if(json.GetObject("Account") != null){
		
			Account account = Account.CreateInstance("Account") as Account;
			account.init(json.GetObject("Account"));

			this.account = account;

		}


		//create and add opportunitylineitems/oppProducts
		if(json.GetObject("OpportunityLineItems") != null){

			JSONArray rowRecords = json.GetObject("OpportunityLineItems").GetArray ("records");

			List <OpportunityProduct> oppProducts = new List<OpportunityProduct>();

			foreach (JSONValue row in rowRecords) {

				OpportunityProduct oppProduct = OpportunityProduct.CreateInstance("OpportunityProduct") as OpportunityProduct;
				Debug.Log("opp product" + row.ToString());
				JSONObject rec = JSONObject.Parse(row.ToString());
				oppProduct.init(rec);
				oppProducts.Add(oppProduct);

			}

			this.oppProducts = oppProducts;

		}

		//create and add campaign.
		if(json.GetObject("Campaign") != null){
			
			Campaign campaign = Campaign.CreateInstance("Campaign") as Campaign;
			campaign.init(json.GetObject("Campaign"));
			
			this.campaign = campaign;
			
		}

		//create and add account.
		if(json.GetObject("Contract") != null){
			
			Contract contract = Contract.CreateInstance("Contract") as Contract;
			contract.init(json.GetObject("Contract"));
			
			this.contract = contract;
			
		}






	}
}
