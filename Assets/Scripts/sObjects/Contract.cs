using UnityEngine;
using System.Collections;
using Boomlagoon.JSON;

public class Contract : ScriptableObject {
	public string Id{ get; set;}
	public string Account{ get; set; }
	public string ActivatedBy{ get; set; }
	public string BillingAddress{ get; set; }
	public string CompanySigned{ get; set; }
	public string CompanySignedDate{ get; set; }
	public string EndDate{ get; set; }
	public string Name{ get; set; }
	public string ContractNumber{ get; set; }
	public string Owner{ get; set; }
	public string StartDate{ get; set; }
	public double ContractTerm{ get; set; }
	public string CreatedBy{ get; set; }
	public string CustomerSigned{ get; set; }
	public string CustomerSignedDate{ get; set; }
	public string CustomerSignedTitle{ get; set; }
	public string Description{ get; set; }
	public string LastModifiedBy{ get; set; }
	public string OwnerExpirationNotice{ get; set; }
	public string Pricebook2{ get; set; }
	public string ShippingAddress{ get; set; }
	public string SpecialTerms{ get; set; }
	public string Status{ get; set; }
	public float Priority{ get; set;}


	public void init(JSONObject json){
		if(json.GetValue("Id") != null ){this.Id = json.GetString("Id");}
		if(json.GetValue("Account") != null ){this.Account = json.GetString("Account");}
		if(json.GetValue("ActivatedBy") != null ){this.ActivatedBy = json.GetString("ActivatedBy");}
		if(json.GetValue("BillingAddress") != null ){this.BillingAddress = json.GetString("BillingAddress");}
		if(json.GetValue("CompanySigned") != null ){this.CompanySigned = json.GetString("CompanySigned");}
		if(json.GetValue("CompanySignedDate") != null ){this.CompanySignedDate = json.GetString("CompanySignedDate");}
		if(json.GetValue("StartDate") != null ){this.StartDate = json.GetString("StartDate");}
		if(json.GetValue("EndDate") != null ){this.EndDate = json.GetString("EndDate");}
		if(json.GetValue("Name") != null ){this.Name = json.GetString("Name");}
		if(json.GetValue("ContractTerm") != null ){this.ContractTerm = json.GetNumber("ContractTerm");}
		if(json.GetValue("ContractNumber") != null ){this.ContractNumber = json.GetString("ContractNumber");}
		if(json.GetValue("CreatedBy") != null ){this.CreatedBy = json.GetString("CreatedBy");}
		if(json.GetValue("CustomerSigned") != null ){this.CustomerSigned = json.GetString("CustomerSigned");}
		if(json.GetValue("CustomerSignedDate") != null ){this.CustomerSignedDate = json.GetString("CustomerSignedDate");}
		if(json.GetValue("CustoemrSignedTitle") != null ){this.CustomerSignedTitle = json.GetString("CustomerSignedTitle");}
		if(json.GetValue("Description") != null ){this.Description = json.GetString("Description");}
		if(json.GetValue("LastModifiedBy") != null ){this.LastModifiedBy = json.GetString("LastModifiedBy");}
		if(json.GetValue("OwnerExpirationNotice") != null ){this.OwnerExpirationNotice = json.GetString("OwnerExpirationNotice");}
		if(json.GetValue("Pricebook2") != null ){this.Pricebook2 = json.GetString("Pricebook2");}
		if(json.GetValue("ShippingAddress") != null ){this.ShippingAddress = json.GetString("ShippingAddress");}
		if(json.GetValue("SpecialTerms") != null ){this.SpecialTerms = json.GetString("SpecialTerms");}
		if(json.GetValue("Status") != null ){this.Status = json.GetString("Status");}
		if(json.GetValue("Priority__c") != null ){this.Priority = (float)json.GetNumber("Priority__c");}
		Debug.Log("This is priority on contract " + this.Priority + "" );
	}
}
