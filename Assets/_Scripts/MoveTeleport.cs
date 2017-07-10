using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveTeleport : MonoBehaviour {

	public float gazeActivationTime = 1.25f;
	public TeleportTransition teleportTransition;
	public GameObject teleportTransitionCanvas;
	public GameObject teleportFadePanel;
	public GameObject gvrController;
	public HotspotMovementController hotspotMovementController;

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
		teleportFadePanel.GetComponent<TeleportTransition> ().TeleportingFalse ();
	}

	private void LinkChecking(){
		if (teleportTransitionCanvas == null) {
			Debug.LogError ("'MoveTeleport.cs' is missing external reference: 'teleportTransitionCanvas'!");
		} else if (teleportTransition == null) {
			Debug.LogError ("'MoveTeleport.cs' is missing external reference: 'teleportTransition'!");
		} else if (teleportFadePanel == null) {
			Debug.LogError ("'MoveTeleport.cs' is missing external reference: 'teleportFadePanel'!");
		} else if (gvrController == null) {
			Debug.LogError ("'MoveTeleport.cs' is missing external reference: 'gvrController'!");
		} else if (hotspotMovementController == null) {
			Debug.LogError ("'MoveTeleport.cs' is missing external reference: 'hotspotMovementController'!");
		}
	}

}
