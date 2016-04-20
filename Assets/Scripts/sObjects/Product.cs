using UnityEngine;
using System.Collections;
using Boomlagoon.JSON;

public class Product : ScriptableObject {

	public string Id{ get; set; }
	public string IsActive{ get; set; }
	public string CreatedBy{ get; set; }
	public string LastModifiedBy{ get; set; }
	public string ProductCode{ get; set; }
	public string Description{ get; set; }
	public string Family{ get; set; }
	public string Name{ get; set; }

	public void init(JSONObject json){
		if(json.GetValue("Id") != null ){this.Id = json.GetString("Id");}
		if(json.GetValue("IsActive") != null ){this.IsActive = json.GetString("IsActive");}
		if(json.GetValue("CreatedBy") != null ){this.CreatedBy = json.GetString("CreatedBy");}
		if(json.GetValue("LastModifiedBy") != null ){this.LastModifiedBy = json.GetString("LastModifiedBy");}
		if(json.GetValue("ProductCode") != null ){this.ProductCode = json.GetString("ProductCode");}
		if(json.GetValue("Description") != null ){this.Description = json.GetString("Description");}
		if(json.GetValue("Family") != null ){this.Family = json.GetString("Family");}
		if(json.GetValue("Name") != null ){this.Name = json.GetString("Name");}
	}
}
