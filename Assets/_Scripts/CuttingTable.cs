using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingTable : MonoBehaviour {

	public GameObject frontDoors;
	public ExploreController exploreController;

	public float gazeActivationTime = 1.25f;

	private float timeElapsed;
	private bool isEntered = false;

	void Update () {
		if (isEntered) {
			timeElapsed += Time.deltaTime;
			if (timeElapsed >= gazeActivationTime){
				timeElapsed = 0;
				exploreController.myState = ExploreController.States.cuttingTable;
			}
		}
	}

	public void OnPointerEnter (){
		isEntered = true;
	}

	public void OnPointerExit (){
		isEntered = false;
		timeElapsed = 0;
	}

}
