using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotspotMovementController : MonoBehaviour {

    //This class will be used to:
    // 		1. Determine if one hotspot can see another.
    //		2. Allow triggering hotspots or turning them off while a sequence is playing to prevent accidental moving.
    //		3. To give proper affordance to the user, make sure they get notified that movement is disabled temporarily with a warning. 
    //		   A closed caption perhaps?

    public Canvas movieModeCanvas;

	public bool movementPermitted;

	void Start (){
		LinkChecking ();
	}

	void Update () {
		if (!movementPermitted) {

		}
	}

	private void TurnOffGvrReticle(){

	}

	private void CanvasDisable() {
		movieModeCanvas.enabled = false;
	}


	private void LinkChecking(){
		if (movieModeCanvas == null) {
			Debug.LogError ("'HotSpotMovementController.cs' is missing external reference: 'movieModeCanvas'!");
		}
	}

}
