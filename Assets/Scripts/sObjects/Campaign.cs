using UnityEngine;
using System.Collections;
using Boomlagoon.JSON;

public class Campaign : ScriptableObject {

	public string Id{ get; set; }
	public string IsActive{ get; set; }
	public string ActualCost{ get; set; }
	public string BudgetedCost{ get; set; }
	public string CampaignMemberRecordType{ get; set; }
	public string Name{ get; set; }
	public string Owner{ get; set; }
	public double NumberOfConvertedLeads{ get; set; }
	public string CreatedBy{ get; set; }
	public string Description{ get; set; }
	public string EndDate{ get; set; }
	public string ExpectedResponse{ get; set; }
	public string ExpectedRevenue{ get; set; }
	public string LastModifiedBy{ get; set; }
	public double NumberSent{ get; set; }
	public double NumberOfOpportunities{ get; set; }
	public double NumberOfWonOpportunities{ get; set; }
	public string Parent{ get; set; }
	public string StartDate{ get; set; }
	public string Status{ get; set; }
	public string HierarchyActualCost{ get; set; }
	public string HierarchyBudgetedCost{ get; set; }
	public double NumberOfContacts{ get; set; }
	public double HierarchyNumberOfContacts{ get; set; }
	public double HierarchyNumberOfConvertedLeads{ get; set; }
	public string HierarchyExpectedRevenue{ get; set; }
	public double NumberOfLeads{ get; set; }
	public double HierarchyNumberOfLeads{ get; set; }
	public double HierarchyNumberSent{ get; set; }
	public double HierarchyNumberOfOpportunities{ get; set; }
	public double NumberOfResponses{ get; set; }
	public double HierarchyNumberOfResponses { get; set; }
	public string AmountAllOpportunities{ get; set; }
	public string HierarchyAmountAllOpportunities{ get; set; }
	public string AmountWonOpportunities{ get; set; }
	public string HierarchyNumberOfWonOpportunities{ get; set; }
	public string Type{ get; set; }
	public float Priority { get; set;}

	public void init(JSONObject json){
		if(json.GetValue("Id") != null ){this.Id = json.GetString("Id");}
		if(json.GetValue("IsActive") != null ){this.IsActive = json.GetString("IsActive");}
		if(json.GetValue("ActualCost") != null ){this.ActualCost = json.GetString("ActualCost");}
		if(json.GetValue("BudgetedCost") != null ){this.BudgetedCost = json.GetString("BudgetedCost");}
		if(json.GetValue("CampaignMemberRecordType") != null ){this.CampaignMemberRecordType = json.GetString("CampaignMemberRecordType");}
		if(json.GetValue("Name") != null ){this.Name = json.GetString("Name");}
		if(json.GetValue("Owner") != null ){this.Owner = json.GetString("Owner");}
		if(json.GetValue("NumberOfConvertedLeads") != null ){this.NumberOfConvertedLeads = json.GetNumber("NumberOfConvertedLeads");}
		if(json.GetValue("CreatedBy") != null ){this.CreatedBy = json.GetString("CreatedBy");}
		if(json.GetValue("Description") != null ){this.Description = json.GetString("Description");}
		if(json.GetValue("EndDate") != null ){this.EndDate = json.GetString("EndDate");}
		if(json.GetValue("ExpectedResponse") != null ){this.ExpectedResponse = json.GetString("ExpectedResponse");}
		if(json.GetValue("ExpectedRevenue") != null ){this.ExpectedRevenue = json.GetString("ExpectedRevenue");}
		if(json.GetValue("LastModifiedBy") != null ){this.LastModifiedBy = json.GetString("LastModifiedBy");}
		if(json.GetValue("NumberSent") != null ){this.NumberSent = json.GetNumber("NumberSent");}
		if(json.GetValue("NumberOfOpportunities") != null ){this.NumberOfOpportunities = json.GetNumber("NumberOfOpportunities");}
		if(json.GetValue("NumberOfWonOpportunities") != null ){this.NumberOfWonOpportunities = json.GetNumber("NumberOfWonOpportunities");}
		if(json.GetValue("Parent") != null ){this.Parent = json.GetString("Parent");}
		if(json.GetValue("StartDate") != null ){this.StartDate = json.GetString("StartDate");}
		if(json.GetValue("Status") != null ){this.Status = json.GetString("Status");}
		if(json.GetValue("HierarchyActualCost") != null ){this.HierarchyActualCost = json.GetString("HierarchyActualCost");}
		if(json.GetValue("HierarchyBudgetedCost") != null ){this.HierarchyBudgetedCost = json.GetString("HierarchyBudgetedCost");}
		if(json.GetValue("NumberOfContacts") != null ){this.NumberOfContacts = json.GetNumber("NumberOfContacts");}
		if(json.GetValue("HierarchyNumberOfContacts") != null ){this.HierarchyNumberOfContacts = json.GetNumber("HierarchyNumberOfContacts");}
		if(json.GetValue("HierarchyNumberOfConvertedLeads") != null ){this.HierarchyNumberOfConvertedLeads = json.GetNumber("HierarchyNumberOfConvertedLeads");}
		if(json.GetValue("HierarchyExpectedRevenue") != null ){this.HierarchyExpectedRevenue = json.GetString("HierarchyExpectedRevenue");}
		if(json.GetValue("NumberOfLeads") != null ){this.NumberOfLeads = json.GetNumber("NumberOfLeads");}
		if(json.GetValue("HierarchyNumberOfLeads") != null ){this.HierarchyNumberOfLeads = json.GetNumber("HierarchNumberOfLeads");}
		if(json.GetValue("HierarchyNumberSent") != null ){this.HierarchyNumberSent = json.GetNumber("HierarchyNumberSent");}
		if(json.GetValue("HierarchyNumberOfOpportunities") != null ){this.HierarchyNumberOfOpportunities = json.GetNumber("HierarchyNumberOfOpportunities");}
		if(json.GetValue("NumberOfResponses") != null ){this.NumberOfResponses = json.GetNumber("NumberOfResponses");}
		if(json.GetValue("HierarchyNumberOfResponses") != null ){this.HierarchyNumberOfResponses = json.GetNumber("HierArchyNumberOfResponses");}
		if(json.GetValue("AmountAllOpportunities") != null ){this.AmountAllOpportunities = json.GetString("AmountAllOpportunities");}
		if(json.GetValue("HierarchyAmountAllOpportunities") != null ){this.HierarchyAmountAllOpportunities = json.GetString("HierarchyAmountAllOpportunities");}
		if(json.GetValue("AmountWonOpportunities") != null ){this.AmountWonOpportunities = json.GetString("AmountWonOpportunities");}
		if(json.GetValue("HierarchyNumberOfWonOpportunities") != null ){this.HierarchyNumberOfWonOpportunities = json.GetString("HierarchyNumberOfWonOpportunities");}
		if(json.GetValue("Type") != null ){this.Type = json.GetString("Type");}
		if(json.GetValue("Priority__c") != null ){this.Priority = (float)json.GetNumber("Priority__c");}
	}
}
