using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RingController : MonoBehaviour {
	
	// ring stack vars
	public float speed; // defines the spin rate of each ring.
	public float vertOrder; // defines the vertical order of the ring in the stack
	private bool spin = true; // a flag used to determin if this ring should spin
	private bool focus = false; // a flag used to determine if this ring is the point of focus of the main camera view.

	// ring expand coroutine params
	private Vector3 startV3; // defines the starting location of a ring
	private Vector3 endV3; // defines the ending location of a ring
	private float fract; // a mulitplier used to influence transition speed
	private float startTime;
	private float journeyLength;

	// the vert order of the broadcasting ring
	private float currentSrcVertOrder;

	// ring close coroutine params
	public Vector3 originPosition;
	public Vector3 shiftedPosition;
	private float resetStartTime;
	private float resetJourneyLength;
	public float zone = 1000;
	private float resetFract;

	// title popup label
	private Popup oppPopup;
	private bool popupOn = false;
	private Vector3 fromPoint;
	private Vector3 hitPoint;
	private float popupStartTime;
	private float popdownStartTime;
	public float popupWaitTime = 1f;

	// default title text.
	public string titleTextValue = "Loading...";
	
	// ring aligment rotation vars
	private Quaternion startRotation, endRotation;
	private float period, time;
	private float transFract = 0.05f;

	// used for ring shifting
	private bool ringIsExpanded = false;
	private bool ringIsExpanding = false;
	private bool ringIsClosing = false;

	
	// triggered by the raycast hit event from the main view
	void HitByMainView (Vector3[] tempStorage) {

		fromPoint = tempStorage[0];
		hitPoint = tempStorage[1];
		focus = true;
		
	}
	 

	void Start(){

		fract = 10.0f;
		resetFract = 150.0f;
	
		originPosition = transform.position;
		shiftedPosition = transform.position;

		GameObject popObject = (GameObject)Instantiate(Resources.Load ("Label Holder"));
		oppPopup = popObject.GetComponent<Popup>();

	}

	//------------------------------------------------------------
	//----------------- Frame Update Methods ---------------------
	//------------------------------------------------------------

	// Update is called once per frame
	void Update () {

		Renderer rend = transform.GetComponent<Renderer>();

		// process the ring color base on the speed
		ColorUtil.processRingColor(speed, rend);
		
		// onfocus modify the visual appearence of the ring.
		setFocusHighlight();
		
		// spins ring for this frame if spin = true 
		spinRing();

		checkForMouseEvents();

		// update the shiftedPostion of the ring on every frame
		//*****-------Critical-------******//
		shiftedPosition = transform.position;

		showTitlePopup();
		
		focus = false; // must reset the focus to false
		
	}


	//------------------------------------------------------------
	//---------- Broadcast Event Listener Methods ----------------
	//------------------------------------------------------------


	// setup the ring shift animation and set shift to true.
	void shiftOtherRings(float srcRingVertOrder){

		currentSrcVertOrder = srcRingVertOrder;

		// set the start position of each ring
		startV3 = transform.position;

		// define the start time for the transition.
		float startTimeExpand = Time.time;

		// ignore broadcast messages from this ring
		if(vertOrder != srcRingVertOrder){

			if(vertOrder > srcRingVertOrder){

				endV3 = new Vector3(transform.position.x,(transform.position.y + 10f),transform.position.z);
				journeyLength = Vector3.Distance(startV3, endV3);
			
			}else if(vertOrder < srcRingVertOrder){

				endV3 = new Vector3(transform.position.x,(transform.position.y - 10f),transform.position.z);
				journeyLength = Vector3.Distance(startV3, endV3);

			}

		}else{

			// if this ring is the broadcaster freeze it position
			endV3 = transform.position;
			journeyLength = Vector3.Distance(startV3, endV3);
		}


	
		StartCoroutine(shiftTheRing(startTimeExpand, startV3, endV3));

	

	}

	void resetRingShift(){

		resetStartTime = Time.time;
		resetJourneyLength = Vector3.Distance(shiftedPosition,originPosition);

		StartCoroutine(restTheRing(resetStartTime, shiftedPosition, originPosition));

		ringIsClosing = true;
		
	}
	
	IEnumerator shiftTheRing(float startTimeExpand, Vector3 startV3, Vector3 endV3){

		float distCovered = (Time.time - startTimeExpand) * fract;
		float fracJourney = distCovered / journeyLength;


		if(float.IsNaN(fracJourney)){
			fracJourney = 0f;
		}

		ringIsExpanding = true;

		while(distCovered <= 10f ){

			distCovered = (Time.time - startTimeExpand) * fract;
			fracJourney = distCovered / journeyLength;

			if(float.IsNaN(fracJourney)){
				fracJourney = 0f;
			}
			
			transform.position = Vector3.Lerp(startV3, endV3, fracJourney);
			
			yield return null;
			
		}

	
		if(vertOrder == currentSrcVertOrder){
			transform.BroadcastMessage("showSubRings");
			ringIsExpanded = true;
	
		}

		ringIsExpanding = false;

		shiftedPosition = transform.position;


	}

	IEnumerator restTheRing(float resetStartTime, Vector3 currentPosition, Vector3 originPosition){

		float distCovered = (Time.time - resetStartTime) * resetFract;
		float fracResetJourney = distCovered / resetJourneyLength;

		ringIsClosing = true;

		while(distCovered <= resetJourneyLength){

			distCovered = (Time.time - resetStartTime) * resetFract;
			fracResetJourney = distCovered / resetJourneyLength;

			transform.position = Vector3.Lerp(currentPosition, originPosition, fracResetJourney);

			yield return null;

		}


		ringIsClosing = false;
		ringIsExpanded = false;

	}

	
	//------------------------------------------------------------
	//--------------------- Text and Popups ----------------------
	//------------------------------------------------------------

	void showTitlePopup() {

		if ((focus == true) && (popupOn == false)) { // && (Input.GetButton("Fire3"))) {

			if (popupStartTime == 0) {
				popupStartTime = Time.time;
			} else if ((Time.time - popupStartTime) > popupWaitTime) {
				popupOn = true;
				popdownStartTime = 0;
				showPopup(titleTextValue, fromPoint, hitPoint);
			}

		} else if ((focus == false) && (popupOn == true)) {

			if (popdownStartTime == 0) {
				popdownStartTime = Time.time;
			} else if ((Time.time - popdownStartTime) > popupWaitTime) {
				hidePopup();
				popupOn = false;
				popupStartTime = 0;
			}

		} else if ((focus == false) && (popupOn == false) && (popupStartTime > 0)) {

			popupStartTime = 0;

		} else if ((focus == true) && (popupOn == true) && (popdownStartTime > 0)) {

			popdownStartTime = 0;

		}

	}

	public void showPopup(string popupText, Vector3 playerPoint, Vector3 popupPoint) {

		oppPopup.transform.position = Vector3.MoveTowards(playerPoint, popupPoint, 10f); 
		oppPopup.transform.LookAt(playerPoint);
		oppPopup.transform.Rotate (new Vector3(0, oppPopup.transform.position.y + 180f, 0));
		oppPopup.doPopup (popupText);
		
	}
	
	public void hidePopup() {

		oppPopup.doPopdown ();

	}


	//------------------------------------------------------------
	//--------------- Controller/Gamepad Events ------------------
	//------------------------------------------------------------
	

	void checkForMouseEvents(){

		// onbutton up: input A or mouse button one
		// if both the focus and the click event occur then stop the spin
		if(Input.GetButtonUp("Fire1") && focus == true){
			
			//toggle the spin of this ring
			spin = ( spin == true ) ? false : true;

	
			// this sends a messages to all other rings to shift their vert postion.
			if(ringIsExpanding == false && ringIsExpanded == false && ringIsClosing == false ){
				transform.parent.BroadcastMessage("shiftOtherRings", vertOrder);
			}

			
			alignGapWithView();
			
		}

		// onbutton up: input A or mouse button one
		// if both the focus and the click event occur then stop the spin
		if(Input.GetButtonUp("Fire2") && ringIsClosing == false){

			transform.parent.BroadcastMessage("resetRingShift");
			transform.parent.BroadcastMessage("hideSubRings");
			//transform.BroadcastMessage("hideSubRings");
				 
		}
		

	}


	//------------------------------------------------------------
	//--------------- Set Ring Spin and Color --------------------
	//------------------------------------------------------------


	void setFocusHighlight(){
		
		if(focus == true){

			Renderer rend = GetComponent<Renderer>();
			ColorUtil.setFocusHighlightColor(rend);
			
		}else{
			
			Renderer rend = GetComponent<Renderer>();
			ColorUtil.removeFocusHighlightColor(rend);
			
		}
		
	}


	void spinRing(){
		
		if(spin == true && zone != 1000){
			transform.Rotate(Vector3.up, speed * Time.deltaTime);
		}
		
	}

	//------------------------------------------------------------
	//------------------ Align the ring gap ----------------------
	//------------------------------------------------------------
	
	// align ring gap with views direction
	void alignGapWithView(){

		float fromPointX = fromPoint.x;
		float fromPointZ = fromPoint.z;

		startRotation = transform.rotation;

		if(Mathf.Abs(fromPointX) < 21 && Mathf.Abs (fromPointZ) < 21){


			fromPoint = Vector3.MoveTowards(fromPoint, hitPoint, -110f);


		}


		endRotation = Quaternion.LookRotation((new Vector3(fromPointX,0f,fromPointZ)));

		Vector3 adjustment = endRotation.eulerAngles;
		adjustment.y = adjustment.y + 45f;

		endRotation = Quaternion.Euler(adjustment);

	

		float time = 0f;
		float period = 3f;  // it will take 3 sec to look

		float startTime = Time.time;

		StartCoroutine(rotateRingToView(startRotation,endRotation,time,period));


	}


	// coroutine that animates the ring rotation to align the ring gap with the view.
	IEnumerator rotateRingToView(Quaternion startRotation, Quaternion endRotation, float time, float period){

		while(transform.rotation != endRotation) {
			
			transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);

			time += 0.01f;

			yield return null;
			
		}

	}


}
