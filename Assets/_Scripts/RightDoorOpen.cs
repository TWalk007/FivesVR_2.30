using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightDoorOpen : MonoBehaviour {
	
	public float gazeActivationTime = 1.25f;
	public float closedAngle = -180f;
	public float openedAngle = 70f;
	public float doorOpenTime = 1.5f;

	public bool isDoorOpened = false;

	private float timeElapsed;
	private bool isEntered = false;

	void Update () {
		if (isEntered) {
			DoorTrigger();
		} else {
			timeElapsed = 0;
		}
	}

	private void DoorTrigger(){
		timeElapsed += Time.deltaTime;

		if (timeElapsed >= gazeActivationTime) {
			timeElapsed = 0;

			if (!isDoorOpened) {
//				print ("Opening door.");
				iTween.RotateTo (gameObject, iTween.Hash ("y", openedAngle, "easeType", "easeInOutQuad", "time", doorOpenTime));
				isDoorOpened = true;
				isEntered = false;
			} else if (isDoorOpened) {
//				print ("Closing door.");
				iTween.RotateTo (gameObject, iTween.Hash ("y", closedAngle, "easeType", "easeInOutQuad", "time", doorOpenTime));
				isDoorOpened = false;
				isEntered = false;
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
