using UnityEngine;
using UnityEngine.UI;
using Boomlagoon.JSON;
using System.Collections;
using System.Collections.Generic;

public class OpportunityUtil : MonoBehaviour {
	
	private bool focus = false;

	
	void Awake () {}

	void Start(){}

	// Update is called once per frame
	void Update () {

		// this will update the value of the main hud text.
		if (focus == true) {};

		focus = false; // must reset the focus to false

	}

	void HitByMainView () {

		//Debug.Log ("util hit by main view");
		focus = true;
		
	}

	//------------------------------------------------------------
	//---------------- Util Methods For Building -----------------
	//------------------------------------------------------------

	public static void BuildOpportunityRings(JSONArray opportunityRecords, Transform parentTransform, Transform Ring, Transform Block, Transform blockBox){

		List<Opportunity> oppList = new List<Opportunity>();
		
		foreach (JSONValue row in opportunityRecords) {
			JSONObject rec = JSONObject.Parse(row.ToString());
			
			Opportunity opp = Opportunity.CreateInstance("Opportunity") as Opportunity;
			opp.init(rec);
			oppList.Add(opp);
			
		}
		
		int i = 0;
		foreach(Opportunity opp in oppList){
			createRing(opp, i, parentTransform, Ring, Block, blockBox);
			i++;
		}

	}

	// creates a ring as needed for an opportunity
	private static void createRing(Opportunity opp, int i, Transform parentTransform, Transform Ring, Transform Block, Transform blockBox){
		
		//Vector3 resetRotation = parentTransform.rotation.eulerAngles;
		//resetRotation.x = 90.0f;
		
		Transform currentRing = (Transform)Instantiate(Ring, new Vector3(0, 0, 0), Quaternion.identity);
		currentRing.SetParent(GameObject.Find("Rings").transform);
		currentRing.localScale = new Vector3(5, 5, 5);
		//currentRing.rotation = Quaternion.Euler(resetRotation);
		currentRing.localPosition = new Vector3(0, 0, i);
		
		//get an instance of the component.
		Transform ringInstance = currentRing.GetComponent<Transform>();
		
		RingController ringController = ringInstance.GetComponent<RingController>();
		ringController.speed = opp.urgent;
		ringController.vertOrder = (float)i + 1f;
		ringController.transform.position = currentRing.transform.position;
		ringController.originPosition = new Vector3(0, 0, i);
		ringController.shiftedPosition = new Vector3(0, 0, i);
		ringController.zone = i;

		// set the title of the current opportunity
		// used by the opportuntiy popup.
		ringController.titleTextValue = opp.oppName;

		// get reference to the subRingDisk controller
		// as the subObjects are built and rendered to the enviroment -
		// construct the subRingDisk object 
		// This is used for rotation management based on priorit

		Transform subRingDisk = ringInstance.FindChild("subRingDisk");
		subRingDisk.SetParent(ringInstance);

		SubRingDiskController subRingDiskCtrl = subRingDisk.GetComponent<SubRingDiskController>();

		//subRingDisk.RotateAround(subRingDisk.position,Vector3.up, 180f);


		// create Account object and block
		if(opp.account != null){


			Vector3 setRotation = parentTransform.rotation.eulerAngles;
			//setRotation.x = -90.0f;
			setRotation.y = 0.0f;

			Transform currentBlockBox = (Transform)Instantiate(blockBox, new Vector3(0, 0, 0), Quaternion.identity);
			currentBlockBox.SetParent(subRingDisk);
			currentBlockBox.localScale = new Vector3(1f, 1f, 1f);
			currentBlockBox.rotation = Quaternion.Euler(setRotation);
			currentBlockBox.localPosition = new Vector3(0f,0f,0f);

			SubObjectUtil.createAccountBlock(opp.account,0,currentBlockBox,Block,subRingDiskCtrl);
			subRingDiskCtrl.accounts.Add(opp.account);

		}

		// create Opportunity Products objects and blocks
		if(opp.oppProducts != null){
			int o = 0;

			Vector3 setRotation = parentTransform.rotation.eulerAngles;
			//setRotation.x = -90.0f;
			setRotation.y = 45.0f;

			Transform currentBlockBox = (Transform)Instantiate(blockBox, new Vector3(0, 0, 0), Quaternion.identity);
			currentBlockBox.SetParent(subRingDisk);
			currentBlockBox.localScale = new Vector3(1f, 1f, 1f);
			currentBlockBox.rotation = Quaternion.Euler(setRotation);
			currentBlockBox.localPosition = new Vector3(0f,0f,0f);

			foreach(OpportunityProduct oppProduct in opp.oppProducts){
				SubObjectUtil.createOppProductBlock(oppProduct, o, currentBlockBox, Block, subRingDiskCtrl);
				subRingDiskCtrl.opportuniutyProducts.Add(oppProduct);
				o++;
			}
		}

		// create campaign object and block
		if(opp.campaign != null){

			Vector3 setRotation = parentTransform.rotation.eulerAngles;
			//setRotation.x = -90.0f;
			setRotation.y = 180f;

			Transform currentBlockBox = (Transform)Instantiate(blockBox, new Vector3(0, 0, 0), Quaternion.identity);
			currentBlockBox.SetParent(subRingDisk);
			currentBlockBox.localScale = new Vector3(1f, 1f, 1f);
			currentBlockBox.rotation = Quaternion.Euler(setRotation);
			currentBlockBox.localPosition = new Vector3(0f,0f,0f);

			SubObjectUtil.createCampaignBlock(opp.campaign,0,currentBlockBox,Block, subRingDiskCtrl);
			subRingDiskCtrl.campaigns.Add(opp.campaign);
		}

		if(opp.contract != null){

			Vector3 setRotation = parentTransform.rotation.eulerAngles;
			//setRotation.x = -90.0f;
			setRotation.y = 270f;
			
			Transform currentBlockBox = (Transform)Instantiate(blockBox, new Vector3(0, 0, 0), Quaternion.identity);
			currentBlockBox.SetParent(subRingDisk);
			currentBlockBox.localScale = new Vector3(1f, 1f, 1f);
			currentBlockBox.rotation = Quaternion.Euler(setRotation);
			currentBlockBox.localPosition = new Vector3(0f,0f,0f);

			SubObjectUtil.createContractBlock(opp.contract,0,currentBlockBox,Block, subRingDiskCtrl);
			subRingDiskCtrl.contracts.Add (opp.contract);

		}


		//TODO: figure out order query issue.
//		SubObjectUtil.createOrderBlock(4,currentRing,Block);

		//TODO: Write a query for this Events
//		SubObjectUtil.createEventBlock(4,currentRing,Block);


	

		//TODO: Write a query for Tasks
//		SubObjectUtil.createTaskBlock(7,currentRing,Block);

		//TODO: Write a query for Pricebooks
//		SubObjectUtil.createPriceBookBlock(1,currentRing,Block);

			
	}

}
