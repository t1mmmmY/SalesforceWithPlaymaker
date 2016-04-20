using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SubRingDiskController : MonoBehaviour {

	public bool change = false;

	private string priorityZone;
	private float smoothing = 25f;
	private float offSetState = 0f;

	private float existingRotationOffset = 0f;
	private float lastRequestedOffset = 0f;
	private float calcOffset = 0f;
	
	public List <Account> accounts { get; set;}
	public List <Campaign> campaigns { get; set;}
	public List <OpportunityProduct> opportuniutyProducts { get; set;}
	public List <Contract> contracts { get; set;}
	public List <Product> products { get; set;}
	public List <Pricebook> pricebooks { get; set;}
	public List <Event> events { get; set;}
	public List <Task> tasks { get; set;}
	public List <Order> orders { get; set;}
	
	public static Dictionary <string,float> nameToDegreeDict = new Dictionary<string, float>{
		{"Account" , -135f},
		{"OpportunityProduct" , -180f},
		{"Order",-224f},
		{"Price_Book",-270f},
		{"Campaign",-315f},
		{"Event",0f},
		{"Contract",45f},
		{"Task",90f}
	};

	void Awake(){
	
		accounts = new List <Account>();
		campaigns = new List <Campaign>();
		opportuniutyProducts = new List <OpportunityProduct>();
		contracts = new List <Contract>();
		products = new List <Product>();
		pricebooks = new List <Pricebook>();
		events = new List <Event>();
		tasks = new List <Task>();
		orders = new List <Order>();
	
	}


	// Use this for initialization
	void Start () {

		setRingRotation();
	
	} 
	
	// Update is called once per frame
	void Update () {

		if (change == true) {
		
			setRingRotation();

			updateBlockColor();


		
		}
	
	}

	public void setRingRotation(){

		findTopPriority();

		processRotataion();
		
	}
	

	private void findTopPriority(){

		priorityZone = "Account";
		float currentTopPriorityRating = 1f;


		if (accounts.Count > 0 && currentTopPriorityRating < accounts[0].Priority) {
			currentTopPriorityRating = accounts[0].Priority;
			priorityZone = "Account";
		}

		if (campaigns.Count > 0 && currentTopPriorityRating < campaigns[0].Priority) {
			currentTopPriorityRating = campaigns[0].Priority;
			priorityZone = "Campaign";
		}

		if (opportuniutyProducts.Count > 0 && currentTopPriorityRating < opportuniutyProducts[0].Priority) {
			currentTopPriorityRating = opportuniutyProducts[0].Priority;
			priorityZone = "OpportunityProduct";
		}

		if (opportuniutyProducts.Count > 0){
			Debug.Log(opportuniutyProducts[0].Priority + " of opp product" );
		}
		if (campaigns.Count > 0){
			Debug.Log(campaigns[0].Priority + " of campains" );
		}

		if (accounts.Count > 0){
			Debug.Log(accounts[0].Priority + " of account");
		}
		Debug.Log(currentTopPriorityRating + " current rating");

	}

	void updateBlockColor(){

		List <Dictionary <Transform,BlockController>> subBlockTransformListOfDict = new List <Dictionary <Transform,BlockController>>();

		Dictionary <Transform,BlockController> oppProductionBlockToControllerMap = new Dictionary<Transform, BlockController>();
		// TODO add others as needed
	
		foreach(Transform child in transform){

			foreach(Transform subChild in child){

				Debug.Log(child.tag + " is the tag of the subBlock component");

				//Transform subBlockInstance = subBlock.GetComponent<Transform>();

				BlockController subBlockController = subChild.GetComponent<BlockController>();

				if(subBlockController.objectType == "Account"){
					subBlockController.speed = accounts[0].Priority;
				}else if(subBlockController.objectType == "Campaign"){
					subBlockController.speed = campaigns[0].Priority;
				}else if(subBlockController.objectType == "OpportunityProduct"){
					subBlockController.speed = opportuniutyProducts[subBlockController.order].Priority;
					oppProductionBlockToControllerMap.Add(subChild,subBlockController);
				}

			}
		
		}

		// reset the order of the subobjects 
		opportuniutyProducts = opportuniutyProducts.OrderByDescending(oppProduct => oppProduct.Priority).ToList();

		subBlockTransformListOfDict.Add(oppProductionBlockToControllerMap);

		//call the position update
		foreach(Dictionary <Transform,BlockController> blockTransformToControllerDict in subBlockTransformListOfDict){

			updateBlockPositionOrder(blockTransformToControllerDict);

		}


	}

	void updateBlockPositionOrder(Dictionary <Transform,BlockController> blockTransformToControllerDict){

		List <Transform> blockTransforms = blockTransformToControllerDict.OrderByDescending(x => x.Value.speed).Select(x => x.Key).ToList();


		// Step 1: Move the last in line to the left out of the main line
		int i = (blockTransforms.Count());
		float posX =  1.29f + ( i * (1.299f * 0.14f));
		float posY = 0;

		// TODO: Fix Argument Out Of Range error when there is only one block in ring
		Vector3 targetPostion = new Vector3(blockTransforms[blockTransforms.Count() - 1].localPosition.x, blockTransforms[blockTransforms.Count() - 1].localPosition.y, 0.5f);

		float shiftBlockLeftStartTime = Time.time;
		float fract = 0.5f;
		float journeyLength = Vector3.Distance(blockTransforms[blockTransforms.Count() - 1].localPosition,targetPostion);

		StartCoroutine(shiftBlockLeft(blockTransforms[blockTransforms.Count() - 1], targetPostion, shiftBlockLeftStartTime, journeyLength, fract, blockTransforms));

		// Step 2: Move all the rest forward;

		//int counter = 0;

//		foreach(Transform blockTransform in blockTransforms){
//
//			float iposX = ((counter + 1) <= 1) ? 1.29f : 1.29f + ((counter + 1) * (1.299f * 0.14f));
//
//
//			if(counter != (blockTransforms.Count() - 1)){
//
//				
//				targetPostion = new Vector3(iposX, 0.0f, blockTransform.localPosition.z);
//				
//				float startTime = Time.time;
//				float fraction = 0.5f;
//				float thisJourneyLength = Vector3.Distance(blockTransform.localPosition,targetPostion);
//
//				StartCoroutine(shiftBlockLeft(blockTransform, targetPostion, shiftBlockLeftStartTime, thisJourneyLength, fract, blockTransforms));
//
//			}
//			
//			++counter;
//		}

		// Step 3: Move the last to the back

		//blockTransforms[blockTransforms.Count() - 1].localPosition = new Vector3(posX, posY, 0.5f);

		// Step 4: Move the last to the right back in line;

		//blockTransforms[blockTransforms.Count() - 1].localPosition = new Vector3(posX, posY, 0f);

	}

	void processStep2(List <Transform> blockTransforms){

		int counter = 0;
		
		foreach(Transform blockTransform in blockTransforms){
			
			float iposX = ((counter + 1) <= 1) ? 1.29f : 1.29f + ((counter) * (1.299f * 0.14f));
			
			if(counter != (blockTransforms.Count() - 1)){
				
				
				Vector3 targetPostion = new Vector3(iposX, 0.0f, blockTransform.localPosition.z);
				
				float startTime2 = Time.time;
				float fraction = 0.5f;
				float thisJourneyLength = Vector3.Distance(blockTransform.localPosition,targetPostion);
				
				StartCoroutine(shiftBlocksForward(blockTransform, targetPostion, startTime2, thisJourneyLength, fraction ));
				
			}
			
			++counter;

		}




	}

	IEnumerator shiftBlockLeft(Transform blockTransform, Vector3 targetPostion, float startTime, float journeyLength, float fract, List <Transform> blockTransforms){

		Vector3 currentPosition = blockTransform.localPosition;

		float distCovered = (Time.time - startTime) * fract;
		float fracJourney = distCovered / journeyLength;

		while(distCovered < journeyLength){

			distCovered = (Time.time - startTime) * fract;
			fracJourney = distCovered / journeyLength;
			
			blockTransform.localPosition = Vector3.Lerp(currentPosition,targetPostion, fracJourney );

			yield return null;
			
		}

		Debug.Log("shift left is complete!");

		processStep2(blockTransforms);

		int i = (blockTransforms.Count());
		float posX =  1.29f + ( (i - 1) * (1.299f * 0.14f));
		
		targetPostion = new Vector3(posX, 0f, 0.5f);
		
		startTime = Time.time;
		fract = 0.5f;
		journeyLength = Vector3.Distance(blockTransforms[blockTransforms.Count() - 1].localPosition,targetPostion);

		yield return StartCoroutine(shiftBlockBack(blockTransforms[blockTransforms.Count() - 1], targetPostion, startTime, journeyLength, fract, blockTransforms));

	}

	IEnumerator shiftBlockBack(Transform blockTransform, Vector3 targetPostion, float startTime, float journeyLength, float fract, List <Transform> blockTransforms){
		
		Vector3 currentPosition = blockTransform.localPosition;
		
		float distCovered = (Time.time - startTime) * fract;
		float fracJourney = distCovered / journeyLength;
		
		while(distCovered < journeyLength){
			
			distCovered = (Time.time - startTime) * fract;
			fracJourney = distCovered / journeyLength;
			
			blockTransform.localPosition = Vector3.Lerp(currentPosition,targetPostion, fracJourney );
			
			yield return null;
			
		}
		
		Debug.Log("shift Back is complete!");

		int i = (blockTransforms.Count());
		float posX =  1.29f + ( i * (1.299f * 0.14f));
		float posY = 0;
		
		targetPostion = new Vector3(blockTransforms[blockTransforms.Count() - 1].localPosition.x, blockTransforms[blockTransforms.Count() - 1].localPosition.y, 0.0f);
		
		startTime = Time.time;
		fract = 0.5f;
		journeyLength = Vector3.Distance(blockTransforms[blockTransforms.Count() - 1].localPosition,targetPostion);
		
		yield return StartCoroutine(shiftBlockRight(blockTransforms[blockTransforms.Count() - 1], targetPostion, startTime, journeyLength, fract));
		
	}

	IEnumerator shiftBlockRight(Transform blockTransform, Vector3 targetPostion, float startTime, float journeyLength, float fract){
		
		Vector3 currentPosition = blockTransform.localPosition;
		
		float distCovered = (Time.time - startTime) * fract;
		float fracJourney = distCovered / journeyLength;
		
		while(distCovered < journeyLength){
			
			distCovered = (Time.time - startTime) * fract;
			fracJourney = distCovered / journeyLength;
			
			blockTransform.localPosition = Vector3.Lerp(currentPosition,targetPostion, fracJourney );
			
			yield return null;
			
		}
		
		Debug.Log("shift Right is complete!");
		
		yield return null;
		
	}

	IEnumerator shiftBlocksForward(Transform blockTransform, Vector3 targetPostion, float startTime, float journeyLength, float fract){
		
		Vector3 currentPosition = blockTransform.localPosition;
		
		float distCovered = (Time.time - startTime) * fract;
		float fracJourney = distCovered / journeyLength;
		
		while(distCovered < journeyLength){

			Debug.Log (journeyLength + "jl");
			Debug.Log (distCovered + "dc");
			Debug.Log (fracJourney + "fj");
			
			distCovered = (Time.time - startTime) * fract;
			fracJourney = distCovered / journeyLength;
			
			blockTransform.localPosition = Vector3.Lerp(currentPosition,targetPostion, fracJourney );
			
			yield return null;
			
		}
		
		Debug.Log("shift is complete!");
		
		//yield return null;
		
	}

	public void processRotataion(){


		calcOffset = -(lastRequestedOffset - nameToDegreeDict[priorityZone]);

		Debug.Log(calcOffset);
		Debug.Log(priorityZone);
		StartCoroutine(animateRotation(transform, calcOffset));
		lastRequestedOffset = nameToDegreeDict[priorityZone];

		change = false;

	}

	IEnumerator animateRotation(Transform disk, float targetOffset){

		while(offSetState < Mathf.Abs(targetOffset)){

			offSetState = offSetState + (smoothing * Time.deltaTime);

			if(targetOffset < 0f){
				transform.RotateAround(disk.position,Vector3.up, -(smoothing * Time.deltaTime));
			}else{
				transform.RotateAround(disk.position,Vector3.up, (smoothing * Time.deltaTime));
			}

			yield return null;

		}

		offSetState = 0f;

		//Debug.Log("rotation complete!");

		//yield return null;

	}

}
