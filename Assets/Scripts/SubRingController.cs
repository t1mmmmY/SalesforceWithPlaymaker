using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SubRingController : MonoBehaviour {

	public float speed; // use to define color
	public  int order; // refernce to its order in the stack. zero is the closest to the inner ring
	
	private bool spin = true;
	private bool visible = false;

	private bool focus = false;

	private Material baseMaterial;
	private Material highlightMaterial;
	private Material greenMaterial;
	private Material yellowMaterial;
	private Material orangeMaterial;
	private Material redMaterial;
	private Material purpleMaterial;


	// title popup label
	private GameObject oppPopupDetail;
	private bool popupOn = false;
	private Vector3 fromPoint;
	private Vector3 hitPoint;
	private float popupStartTime;
	private float popdownStartTime;
	public float popupWaitTime = 1f;

	public SubRingDiskController parentSubRingDiskController;
	public string objectName;
	public string objectType;
	public string[] labels;
	public string[] fields;
	public string description;

	// Use this for initialization
	void Start () {

		Renderer rend = GetComponent<Renderer>();
		highlightMaterial = Resources.Load("blue") as Material;
		greenMaterial = Resources.Load("green") as Material;
		yellowMaterial = Resources.Load("yellow") as Material;
		orangeMaterial =  Resources.Load("orange") as Material;
		redMaterial = Resources.Load("red") as Material;
		purpleMaterial = Resources.Load("purple") as Material;

	}
	
	// Update is called once per frame
	void Update () {

		Renderer rend = transform.GetComponent<Renderer>();
		
		// process the ring color base on the speed
		ColorUtil.processRingColor(speed, rend);

		setFocusHighlight();

		spinRing();

		checkButtons();

		focus = false;
	
	}

	// triggered by the raycast hit event from the main view
	void HitByMainView (Vector3[] tempStorage) {
		fromPoint = tempStorage[0];
		hitPoint = tempStorage[1];
		focus = true;
		
	}



	void checkButtons() {

		/*if ((focus == true) && (Input.GetButtonUp ("Fire1"))) {

			GameObject windows = GameObject.FindGameObjectWithTag ("Window");
			WindowHandler windowHandler = windows.GetComponent<WindowHandler>();
			windowHandler.showWindows (objectName + " (" + objectType + ")", labels, fields, description);
			windowHandler.objectType = objectType;
			windowHandler.currentSubRingController = parentSubRingDiskController;



		}*/

	}



	void spinRing(){

		if(visible == true){
			GetComponent<Renderer>().enabled = true;
			GetComponent<Collider>().enabled = true;
		}

		if(visible == false){
			GetComponent<Renderer>().enabled = false;
			GetComponent<Collider>().enabled = false;
		}
		
	}

	void processRingColor(float speed){
		
		Renderer rend = GetComponent<Renderer>();
		
		if(speed > 0f && speed <= 15f ){
			baseMaterial = greenMaterial;
			rend.material = baseMaterial;
		} else if(speed >= 16f && speed <= 30f ){
			baseMaterial = yellowMaterial;
			rend.material = baseMaterial;
		} else if(speed >= 31f && speed <= 45f ){
			baseMaterial = orangeMaterial;
			rend.material = baseMaterial;
		} else if(speed >= 46f && speed <= 60f ){
			baseMaterial = redMaterial;
			rend.material = baseMaterial;
		} else {
			baseMaterial = purpleMaterial;
			rend.material = baseMaterial;
		}
		
		
	}

	void setFocusHighlight(){
		
		if(focus == true){
			
			Renderer rend = GetComponent<Renderer>();
			ColorUtil.setFocusHighlightColor(rend);
			
		}else{
			
			Renderer rend = GetComponent<Renderer>();
			ColorUtil.removeFocusHighlightColor(rend);
			
		}
		
	}

	void showSubRings(){
		visible = true;
	}

	void hideSubRings(){
		visible = false;
	}
}
