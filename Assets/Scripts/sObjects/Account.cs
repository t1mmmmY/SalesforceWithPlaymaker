using UnityEngine;
using System.Collections;
using Boomlagoon.JSON;

public class Account : ScriptableObject {

	public string Id{ get; set;}
	public string Name{ get; set; }
	public string AccountNumber{ get; set; }
	public string Owner{ get; set; }
	public string Site{ get; set; }
	public string AccountSource{ get; set; }
	public string AnnualRevenue{ get; set; }
	public string BillingAddress{ get; set; }
	public string CreatedBy{ get; set; }
	public string DandbCompany{ get; set; }
	public double NumberOfEmployees{ get; set; }
	public string Fax{ get; set; }
	public string Industry{ get; set; }
	public string LastModifiedBy{ get; set; }
	public string NaicsCode{ get; set; }
	public string NaicsDesc{ get; set; }
	public string Ownership{ get; set; }
	public string Parent{ get; set; }
	public string Phone{ get; set; }
	public string Rating{ get; set; }
	public string ShippingAddress{ get; set; }
	public string Sic{ get; set; }
	public string SicDesc{ get; set; }
	public string TickerSymbol{ get; set; }
	public string Tradestyle{ get; set; }
	public string Type{ get; set; }
	public string Website{ get; set; }
	public string YearStarted{ get; set; }
	public string Description{ get; set; }
	public string CustomerPriority{ get; set; }
	public string UpsellOpportunity{ get; set; }
	public float Priority { get; set;}

	public void init(JSONObject json){
		if(json.GetValue("Id") != null ){this.Id = json.GetString("Id");}
		if(json.GetValue("Name") != null ){this.Name = json.GetString("Name");}
		if(json.GetValue("AccountNumber") != null ){this.AccountNumber = json.GetString("AccountNumber");}
		if(json.GetValue("Owner") != null ){this.Owner = json.GetString("Owner");}
		if(json.GetValue("Site") != null ){this.Site = json.GetString("Site");}
		if(json.GetValue("AcountSource") != null ){this.AccountSource = json.GetString("AcountSource");}
		if(json.GetValue("AnnualRevenue") != null ){this.AnnualRevenue = json.GetString("AnnualRevenue");}
		if(json.GetValue("BillingAddress") != null ){this.BillingAddress = json.GetString("BillingAddress");}
		if(json.GetValue("CreatedBy") != null ){this.CreatedBy = json.GetString("CreatedBy");}
		if(json.GetValue("DandbCompany") != null ){this.DandbCompany = json.GetString("DandbCompany");}
		if(json.GetValue("NumberOfEmployees") != null ){this.NumberOfEmployees = json.GetNumber("NumberOfEmployees");}
		if(json.GetValue("Fax") != null ){this.Fax = json.GetString("Fax");}
		if(json.GetValue("Industry") != null ){this.Industry = json.GetString("Industry");}
		if(json.GetValue("LastModifiedBy") != null ){this.LastModifiedBy = json.GetString("LastModifiedBy");}
		if(json.GetValue("NaicsCode") != null ){this.NaicsCode = json.GetString("NaicsCode");}
		if(json.GetValue("NaicsDesc") != null ){this.NaicsDesc = json.GetString("NacisDesc");}
		if(json.GetValue("Ownership") != null ){this.Ownership = json.GetString("Ownership");}
		if(json.GetValue("Parent") != null ){this.Parent = json.GetString("Parent");}
		if(json.GetValue("Phone") != null ){this.Phone = json.GetString("Phone");}
		if(json.GetValue("Rating") != null ){this.Rating = json.GetString("Rating");}
		if(json.GetValue("ShippingAddress") != null ){this.ShippingAddress = json.GetString("ShippingAddress");}
		if(json.GetValue("Sic") != null ){this.Sic = json.GetString("Sic");}
		if(json.GetValue("SicDesc") != null ){this.SicDesc = json.GetString("SicDesc");}
		if(json.GetValue("TickerSymbol") != null ){this.TickerSymbol = json.GetString("TickerSymbol");}
		if(json.GetValue("Tradestyle") != null ){this.Tradestyle = json.GetString("Tradestyle");}
		if(json.GetValue("Type") != null ){this.Type = json.GetString("Type");}
		if(json.GetValue("Website") != null ){this.Website = json.GetString("Website");}
		if(json.GetValue("YearStarted") != null ){this.YearStarted = json.GetString("YearStarted");}
		if(json.GetValue("Description") != null ){this.Description = json.GetString("Description");}
		if(json.GetValue("CustomerPriority__c") != null ){this.CustomerPriority = json.GetString("CustomerPriority__c");}
		if(json.GetValue("UpsellOpportunity__c") != null ){this.UpsellOpportunity = json.GetString("UpsellOpportunity__c");}
		if(json.GetValue("Priority__c") != null ){this.Priority = (float)json.GetNumber("Priority__c");}
	}
}
