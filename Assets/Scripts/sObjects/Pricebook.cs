using UnityEngine;
using System.Collections;
using Boomlagoon.JSON;

public class Pricebook : ScriptableObject {

	public string Id{ get; set; }
	public string IsActive{ get; set; }
	public string CreatedBy{ get; set; }
	public string Description{ get; set; }
	public string isStandard{ get; set; }
	public string LastModifiedBy{ get; set; }
	public string Name{ get; set; }

	public void init(JSONObject json){
		if(json.GetValue("Id") != null ){this.Id = json.GetString("Id");}
		if(json.GetValue("IsActive") != null ){this.CreatedBy = json.GetString("IsActive");}
		if(json.GetValue("CreatedBy") != null ){this.CreatedBy = json.GetString("CreatedBy");}
		if(json.GetValue("Description") != null ){this.Description = json.GetString("Description");}
		if(json.GetValue("IsStandard") != null ){this.CreatedBy = json.GetString("IsStandard");}
		if(json.GetValue("LastModifiedBy") != null ){this.CreatedBy = json.GetString("LastModifiedBy");}
		if(json.GetValue("Name") != null ){this.CreatedBy = json.GetString("Name");}
	}
}
