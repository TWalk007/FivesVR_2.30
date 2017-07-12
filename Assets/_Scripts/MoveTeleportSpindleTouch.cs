using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveTeleportSpindleTouch : MonoBehaviour {

	public ExploreControllerTouch exploreControllerTouch;
	public TeleportTransition teleportTransition;

	public GameObject teleportTransitionCanvas;
	public GameObject teleportFadePanel;
	public GameObject touchCameraController;

	private Transform teleportTargetPos;
	private Vector3 heightAdjustedPos;

	private bool teleportToSpot = false;
    
	void Start () {
		heightAdjustedPos = new Vector3 (transform.position.x, transform.position.y + 2.25f, transform.position.z);
	}

	private void TeleportTransition (){
		Instantiate (teleportTransitionCanvas);
		teleportFadePanel.GetComponent<TeleportTransition> ().TeleportingTrue ();
	}

	private void Teleport () {
        touchCameraController.transform.position = heightAdjustedPos;
		TeleportTransition ();
		exploreControllerTouch.myState = ExploreControllerTouch.States.spindleMovement;
		teleportFadePanel.GetComponent<TeleportTransition> ().TeleportingFalse ();
	}

    void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = new Color(0.75f, 0.75f, 0.75f);
    }

    void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnMouseDown()
    {
        Teleport();
    }

}
