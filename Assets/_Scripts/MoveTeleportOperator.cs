using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTeleportOperator : MonoBehaviour {

	public TeleportTransition teleportTransition;
	public ExploreController exploreController;

	public GameObject teleportTransitionCanvas;
	public GameObject teleportFadePanel;
	public GameObject gvrController;


	public float gazeActivationTime = 1.25f;



	private Transform teleportTargetPos;
	private Vector3 heightAdjustedPos;
	private float timeElapsed;
	private bool teleportToSpot = false;



	void Start () {
		heightAdjustedPos = new Vector3 (transform.position.x, transform.position.y + 2.25f, transform.position.z);
	}

	void Update () {

		if (teleportToSpot){
			timeElapsed += Time.deltaTime;

			if (timeElapsed >= gazeActivationTime) {
				timeElapsed = 0;
				Teleport ();
				teleportToSpot = false;

			}
		} else {
			timeElapsed = 0;
		}
	}

	private void TeleportTransition (){
		Instantiate (teleportTransitionCanvas);
		teleportFadePanel.GetComponent<TeleportTransition> ().TeleportingTrue ();
	}

	public void OnPointerEnter (){
		teleportToSpot = true;
	}

	public void OnPointerExit (){
		teleportToSpot = false;
		timeElapsed = 0;
	}

	private void Teleport () {		
		gvrController.transform.position = heightAdjustedPos;
		TeleportTransition ();
		exploreController.myState = ExploreController.States.controls;
		teleportFadePanel.GetComponent<TeleportTransition> ().TeleportingFalse ();
	}
}