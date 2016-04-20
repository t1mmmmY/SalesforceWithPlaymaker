using UnityEngine;
using System.Collections;
using Boomlagoon.JSON;

public class OpportunityProduct : ScriptableObject {

	public string Id{ get; set; }
	public string Name{ get; set; }
	public string CreatedBy{ get; set; }
	public string ServiceDate{ get; set; }
	public double Discount{ get; set; }
	public string LastModifiedBy{ get; set; }
	public string Description{ get; set; }
	public double ListPrice{ get; set; }
	public string Opportunitity{ get; set; }
	public string Product{ get; set; }
	public string ProductCode{ get; set; }
	public double Quantity{ get; set; }
	public double UnitPrice{ get; set; }
	public string Subtotal{ get; set; }
	public double TotalPrice{ get; set; }
	public float Priority { get; set; }


	public void init(JSONObject json){
		if(json.GetValue("Id") != null ){this.Id = json.GetString("Id");}
		if(json.GetValue("Name") != null ){this.Name = json.GetString("Name");}
		if(json.GetValue("CreatedBy") != null ){this.CreatedBy = json.GetString("CreatedBy");}
		if(json.GetValue("ServiceDate") != null ){this.ServiceDate = json.GetString("ServiceDate");}
		if(json.GetValue("Discount") != null ){this.Discount = json.GetNumber("Discount");}
		if(json.GetValue("LastModifiedBy") != null ){this.LastModifiedBy = json.GetString("LastModifiedBy");}
		if(json.GetValue("Description") != null ){this.Description = json.GetString("Description");}
		if(json.GetValue("ListPrice") != null ){this.ListPrice = json.GetNumber("ListPrice");}
		if(json.GetValue("Opportunity") != null ){this.Opportunitity = json.GetString("Opportunity");}
		if(json.GetValue("ProductCode") != null ){this.ProductCode = json.GetString("ProductCode");}
		if(json.GetValue("Quantity") != null ){this.Quantity = json.GetNumber("Quantity");}
		if(json.GetValue("UnitPrice") != null ){this.UnitPrice = json.GetNumber("UnitPrice");}
		if(json.GetValue("Subtotal") != null ){this.Subtotal = json.GetString("Subtotal");}
		if(json.GetValue("TotalPrice") != null ){this.TotalPrice = json.GetNumber("TotalPrice");}
		if(json.GetValue("Priority__c") != null ){this.Priority = (float)json.GetNumber("Priority__c");}

		if (json.GetValue ("Product2") != null) {
			JSONObject prod = json.GetObject ("Product2");
			this.Product = prod.GetString ("Name");
		}
		
	}
}
