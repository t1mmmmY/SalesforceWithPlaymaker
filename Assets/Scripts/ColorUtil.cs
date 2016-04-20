using UnityEngine;
using System.Collections;

public class ColorUtil : MonoBehaviour {

	private static Material baseMaterial;
	private static Material highlightMaterial;
	private static Material greenMaterial;
	private static Material yellowMaterial;
	private static Material orangeMaterial;
	private static Material redMaterial;
	private static Material purpleMaterial;

	void Start() {

		highlightMaterial = Resources.Load("blue") as Material;
		greenMaterial = Resources.Load("green") as Material;
		yellowMaterial = Resources.Load("yellow") as Material;
		orangeMaterial =  Resources.Load("orange") as Material;
		redMaterial = Resources.Load("red") as Material;
		purpleMaterial = Resources.Load("purple") as Material;

	}

	public static void processRingColor(float speed, Renderer rend){

		//Debug.Log ("speed" + speed);
		//Debug.Log ("rend" + rend);
		
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

		//Debug.Log ("material" + rend.material.name);
	}

	public static void setFocusHighlightColor(Renderer rend){

		rend.material = highlightMaterial;

	}
		
	public static void removeFocusHighlightColor(Renderer rend){
		
		rend.material = baseMaterial;
		
	}		
			
		
}
