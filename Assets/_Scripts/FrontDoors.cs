using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDoors : MonoBehaviour {

	public bool frontDoorsOpen = false;

	public GameObject audioController;
	public GameObject leftDoor;
	public GameObject rightDoor;
	public GameObject cuttingTable;

	public ExploreController exploreController;

	private bool leftDoorOpen = false;
	private bool rightDoorOpen = false;
	private bool hasfrontLoadingPlayed = false;

	void Update () {
		FrontDoorStateUpdate ();
		CanAssemblyVOPlay ();
		AssemblyVOReset ();
	}

	private void CanAssemblyVOPlay(){
		if (frontDoorsOpen && !hasfrontLoadingPlayed) {
			hasfrontLoadingPlayed = true;
			exploreController.myState = ExploreController.States.frontLoading;
		}		
	}

	private bool FrontDoorStateUpdate(){
		leftDoorOpen = leftDoor.GetComponent<LeftDoorOpen>().isDoorOpened;
		rightDoorOpen = rightDoor.GetComponent<RightDoorOpen>().isDoorOpened;

		if (leftDoorOpen && rightDoorOpen) {
			return frontDoorsOpen = true;
		} else {
			return frontDoorsOpen = false;
		}
	}

	public void AssemblyVOReset (){
		if (!frontDoorsOpen && hasfrontLoadingPlayed) {
			exploreController.hasFrontLoadingPlayed = false;
			hasfrontLoadingPlayed = false;
		}
	}

}
