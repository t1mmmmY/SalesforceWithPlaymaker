using UnityEngine;
using System.Collections;
using Boomlagoon.JSON;

public class Order : ScriptableObject {

	public string Id{ get; set; }
	public string Account{ get; set; }
	public string AccountNumber{ get; set; }
	public string Contract{ get; set; }
	public string Description{ get; set; }
	public string Opportunitity{ get; set; }
	public string TotalAmount{ get; set; }
	public string EndDate{ get; set; }
	public string Name{ get; set; }
	public string OrderNumber{ get; set; }
	public string Owner{ get; set; }
	public string Type{ get; set; }
	public string Status{ get; set; }

	public void init(JSONObject json){
		if(json.GetValue("Id") != null ){this.Id = json.GetString("Id");}
		if(json.GetValue("Account") != null ){this.Account = json.GetString("Account");}
		if(json.GetValue("AccountNumber") != null ){this.AccountNumber = json.GetString("AccountNumber");}
		if(json.GetValue("Contract") != null ){this.Contract = json.GetString("Contract");}
		if(json.GetValue("Description") != null ){this.Description = json.GetString("Description");}
		if(json.GetValue("Opportunity") != null ){this.Opportunitity = json.GetString("Opportunity");}
		if(json.GetValue("TotalAmount") != null ){this.TotalAmount = json.GetString("TotalAmount");}
		if(json.GetValue("EndDate") != null ){this.EndDate = json.GetString("EndDate");}
		if(json.GetValue("Name") != null ){this.Name = json.GetString("Name");}
		if(json.GetValue("OrderNumber") != null ){this.OrderNumber = json.GetString("OrderNumber");}
		if(json.GetValue("Owner") != null ){this.Owner = json.GetString("Owner");}
		if(json.GetValue("Type") != null ){this.Type = json.GetString("Type");}
		if(json.GetValue("Status") != null ){this.Status = json.GetString("Status");}
	}
}
