using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SubObjectUtil : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void createAccountBlock(Account account, int i, Transform parentTransform, Transform Block, SubRingDiskController subRingDiskCtrl){

		Vector3 setRotation = parentTransform.rotation.eulerAngles;
		setRotation.x = -90.0f;

		float posX = (i < 1) ? 1.29f : 1.29f + (i * (1.29f * 0.14f));
		float posY = 0f;
		
		Transform currentBlock = (Transform)Instantiate(Block, new Vector3(0, 0, 0), Quaternion.identity);
		currentBlock.SetParent(parentTransform);
		currentBlock.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		currentBlock.rotation = Quaternion.Euler(setRotation);
		currentBlock.localPosition = new Vector3(posX, posY, 0f);
	
		
		//get an instance of the component.
		Transform blockInstance = currentBlock.GetComponent<Transform>();
		
		BlockController blockController = blockInstance.GetComponent<BlockController>();

		blockController.speed = account.Priority;
		blockController.order = i;

		blockController.objectId = account.Id;
		blockController.objectName = account.Name;
		blockController.objectType = "Account";
		blockController.labels = new string[]{"Account Number", "Type", "Industry", "Customer Priority", "Upsell Opportunity"};
		blockController.fields = new string[]{account.AccountNumber, account.Type, account.Industry, account.CustomerPriority, account.UpsellOpportunity};
		blockController.description = account.Description;
		blockController.parentSubRingDiskController = subRingDiskCtrl;
		blockController.setText ("Account");
	
	}

	public static void createCampaignBlock(Campaign campaign, int i, Transform parentTransform, Transform Block, SubRingDiskController subRingDiskCtrl){
		
		Vector3 setRotation = parentTransform.rotation.eulerAngles;
		setRotation.x = -90.0f;
	
		float posX = (i < 1) ? 1.29f : 1.29f + (i * (1.29f * 0.14f));
		float posY = 0f;
		
		Transform currentBlock = (Transform)Instantiate(Block, new Vector3(0, 0, 0), Quaternion.identity);
		currentBlock.SetParent(parentTransform);
		currentBlock.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		currentBlock.rotation = Quaternion.Euler(setRotation);
		currentBlock.localPosition = new Vector3(posX, posY, 0f);


		//get an instance of the component.
		Transform blockInstance = currentBlock.GetComponent<Transform>();
		
		BlockController blockController = blockInstance.GetComponent<BlockController>();

		blockController.speed = campaign.Priority;
		blockController.order = i;

		blockController.objectId = campaign.Id;
		blockController.objectName = campaign.Name;
		blockController.objectType = "Campaign";
		blockController.labels = new string[]{"Type", "Status", "Start Date", "End Date", "Total Leads"};
		blockController.fields = new string[]{campaign.Type, campaign.Status, campaign.StartDate, campaign.EndDate, "" + campaign.NumberOfLeads + ""};
		blockController.description = campaign.Description;
		blockController.parentSubRingDiskController = subRingDiskCtrl;
		blockController.setText ("Campaign");
		
	}


	public static void createOppProductBlock(OpportunityProduct oppProduct, int i, Transform parentTransform, Transform Block, SubRingDiskController subRingDiskCtrl){
		
		Vector3 setRotation = parentTransform.rotation.eulerAngles;
		setRotation.x = -90.0f;

		float posX = (i < 1) ? 1.29f : 1.29f + (i * (1.29f * 0.14f));;
		float posY = 0f;
		
		Transform currentBlock = (Transform)Instantiate(Block, new Vector3(0, 0, 0), Quaternion.identity);
		currentBlock.SetParent(parentTransform);
		currentBlock.localScale = new Vector3(0.5f, 0.5f, 0.5f);;
		currentBlock.rotation = Quaternion.Euler(setRotation);
		currentBlock.localPosition = new Vector3(posX, posY, 0f);


		
		//get an instance of the component.
		Transform blockInstance = currentBlock.GetComponent<Transform>();
		
		BlockController blockController = blockInstance.GetComponent<BlockController>();

		blockController.speed = oppProduct.Priority;
		blockController.order = i;

		blockController.objectId = oppProduct.Id;
		blockController.objectName = oppProduct.Name;
		blockController.objectType = "OpportunityProduct";
		blockController.labels = new string[]{"Product", "List Price", "Sales Price", "Quantity", "Total Price"};
		blockController.fields = new string[]{oppProduct.Product, oppProduct.ListPrice.ToString ("$0,0.00"), oppProduct.UnitPrice.ToString ("$0,0.00"), "" + oppProduct.Quantity + "", oppProduct.TotalPrice.ToString ("$0,0.00")};
		blockController.description = oppProduct.Description;
		blockController.parentSubRingDiskController = subRingDiskCtrl;
		blockController.setText ("Opportunity Product");
		
	}

	public static void createContractBlock(Contract contract, int i, Transform parentTransform, Transform Block, SubRingDiskController subRingDiskCtrl){
		
		Vector3 setRotation = parentTransform.rotation.eulerAngles;
		setRotation.x = -90.0f;
		
		float posX = (i < 1) ? 1.29f : 1.29f + (i * (1.29f * 0.14f));
		float posY = 0f;
		
		Transform currentBlock = (Transform)Instantiate(Block, new Vector3(0, 0, 0), Quaternion.identity);
		currentBlock.SetParent(parentTransform);
		currentBlock.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		currentBlock.rotation = Quaternion.Euler(setRotation);
		currentBlock.localPosition = new Vector3(posX, posY, 0f);
		
		
		//get an instance of the component.
		Transform blockInstance = currentBlock.GetComponent<Transform>();
		
		BlockController blockController = blockInstance.GetComponent<BlockController>();
		
		blockController.speed = contract.Priority;
		blockController.order = i;
		
		blockController.objectId = contract.Id;
		blockController.objectName = contract.ContractNumber;
		blockController.objectType = "Contract";
		blockController.labels = new string[]{"Status", "Start Date", "End Date", "Contract Term"};
		blockController.fields = new string[]{contract.Status, contract.StartDate, contract.EndDate, contract.ContractTerm + " months"};
		blockController.description = contract.Description;
		blockController.parentSubRingDiskController = subRingDiskCtrl;
		blockController.setText ("Contract");

		if (contract.SpecialTerms != null) {
			blockController.leftTitle = "Special Terms";
			blockController.leftText = contract.SpecialTerms;
		}

		
	}


		//	public static void createOrderBlock(int i, Transform parentTransform, Transform Block, SubRingDiskController subRingDiskCtrl){
		//		
		//		Vector3 setRotation = parentTransform.rotation.eulerAngles;
		//		setRotation.x = -90.0f;
		//		//setRotation.z = 135f;
		//
		//		float posX = (i < 1) ? -0.9f : -0.9f + (i * (-0.9f * 0.14f));
		//		//float posY = (i < 1) ? 0.9f : 0.9f + (i * (0.9f * 0.14f));
		//
		//		float posX = i + 1;
		//		float posY = 0f;
		//		
		//		Transform currentBlock = (Transform)Instantiate(Block, new Vector3(0, 0, 0), Quaternion.identity);
		//		currentBlock.SetParent(parentTransform);
		//		currentBlock.localScale = new Vector3(1f, 1f, 5f);
		//		currentBlock.rotation = Quaternion.Euler(setRotation);
		//		currentBlock.localPosition = new Vector3(posX, posY, 0f);
		//
		//		//get an instance of the component.
		//		Transform blockInstance = currentBlock.GetComponent<Transform>();
		//		
		//		BlockController blockController = blockInstance.GetComponent<BlockController>();
		//
		//		
		//	}


//	public static void createEventBlock(int i, Transform parentTransform, Transform Block){
//		
//		Vector3 setRotation = parentTransform.rotation.eulerAngles;
//		setRotation.x = -90.0f;
//		//setRotation.z = -180.0f;
//		
//		
//		float posX = (i < 1) ? 1.29f : 1.29f + (i * (1.299f * 0.14f));
//		float posY = 0;
//		
//		Transform currentBlock = (Transform)Instantiate(Block, new Vector3(0, 0, 0), Quaternion.identity);
//		currentBlock.SetParent(parentTransform);
//		currentBlock.localScale = new Vector3(5f, 5f, 5f);
//		currentBlock.rotation = Quaternion.Euler(setRotation);
//		currentBlock.localPosition = new Vector3(posX, posY, 0f);
//
//		//get an instance of the component.
//		Transform blockInstance = currentBlock.GetComponent<Transform>();
//		
//		BlockController blockController = blockInstance.GetComponent<BlockController>();
//	
//		
//	}
//
//	
//
//	public static void createPriceBookBlock( int i, Transform parentTransform, Transform Block, SubRingDiskController subRingDiskCtrl){
//		
//		Vector3 setRotation = parentTransform.rotation.eulerAngles;
//		setRotation.x = -90.0f;
//		setRotation.z = 90f;
//		
//
//		float posX = 0;
//		float posY = (i < 1) ? 1.29f : 1.29f + (i * (1.29f * 0.14f));
//		
//		Transform currentBlock = (Transform)Instantiate(Block, new Vector3(0, 0, 0), Quaternion.identity);
//		currentBlock.SetParent(parentTransform);
//		currentBlock.localScale = new Vector3(0.5f, 0.5f, 0.5f);
//		currentBlock.rotation = Quaternion.Euler(setRotation);
//		currentBlock.localPosition = new Vector3(posX, posY, 0f);
//
//		//get an instance of the component.
//		Transform blockInstance = currentBlock.GetComponent<Transform>();
//		
//		BlockController blockController = blockInstance.GetComponent<BlockController>();
//	
//		
//	}
//
//	public static void createTaskBlock(int i, Transform parentTransform, Transform Block, SubRingDiskController subRingDiskCtrl){
//		
//		Vector3 setRotation = parentTransform.rotation.eulerAngles;
//		setRotation.x = -90.0f;
//		setRotation.z = 90f;
//		
//		
//		float posX = 0;
//		float posY = (i < 1) ? -1.29f : -1.29f + (i * (-1.29f * 0.14f));
//		
//		Transform currentBlock = (Transform)Instantiate(Block, new Vector3(0, 0, 0), Quaternion.identity);
//		currentBlock.SetParent(parentTransform);
//		currentBlock.localScale = new Vector3(0.5f, 0.5f, 0.5f);
//		currentBlock.rotation = Quaternion.Euler(setRotation);
//		currentBlock.localPosition = new Vector3(posX, posY, 0f);
//
//		//get an instance of the component.
//		Transform blockInstance = currentBlock.GetComponent<Transform>();
//		
//		BlockController blockController = blockInstance.GetComponent<BlockController>();
//
//		
//	}




}
