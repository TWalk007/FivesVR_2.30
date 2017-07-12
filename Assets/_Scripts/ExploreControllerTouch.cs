using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using GVR;

public class ExploreControllerTouch : MonoBehaviour {

	//  Use this script as the main controller of events that take place in the 'Explore RT130 Scene'.
	#region Initial Declarations

	public ExploreSceneAudio exploreSceneAudio;
	public MovieCanvasController movieCanvasController;
	public SpindleExtensionTriggers spindleExtensionTriggers;

	public CuttingTable cuttingTable;
	public FrontDoors frontDoors;
	public USFlag usFlag;

	public CaptionsCanvas captionsCanvas;
	public GameObject scene;
	public GameObject touchCameraController;
	public GameObject cameraController;
	public GameObject operatorDoor;
	public GameObject housingSide;
	public GameObject housingSideFade;
	public GameObject housingSideFadeGlass;
	public GameObject newTablePart;
	public GameObject spindleTool;
	public GameObject cuttingTableGObject;
	public GameObject operatorHotspot;
	public GameObject particleGlowTable;
    public GameObject mainCamera;

	public Transform operatorSequenceCamPos;
	public Transform spindleSequenceCamPos;

	public float doorOpenTime = 2.0f;
	public float moveSpeed = 3.0f;
	public float moveOutSpeed = 1f;
	public float movieCanvasFadeDuration = 2f;
	public float sideHousingFadeDuration = 1.5f;
	public float cuttingTableRotations = 1f;
	public float cuttingTableResetDelay = 2.0f;
	public float cuttingTableRotationsTime = 3f;
	public float operatorSceneAngleCorrection = 105f;

	public bool hasFrontLoadingPlayed = false;


	public enum States {wait, frontLoading, cuttingTable, spindleMovement, usFlag, toolChanger, controls};
	public States myState;

	public float stateResetDelay = 2.0f;
	public int arrayInt = 0;

	private Vector3 spindleCamStartPos;
	private Vector3 operatorCamStartPos;
	private Vector3 startRotation;
	private Vector3 operatorCamStartRotation = new Vector3 (10f, -140f, 0f);

	private bool stateLock = false;
	private bool frontDoorsOpen;
	private bool sideDoorOpened = false;

	#endregion

	void Start () {
		myState = States.wait;
	}
	
	void Update () {
		frontDoorsOpen = frontDoors.frontDoorsOpen;

		if (!stateLock) {
			if (myState == States.wait) {
				states_wait ();
                //Debug.Log("States.wait now active.");
			} else if (myState == States.frontLoading && !hasFrontLoadingPlayed) {
				states_frontLoading ();
			} else if (myState == States.cuttingTable && hasFrontLoadingPlayed) {
				states_cuttingTable ();
			} else if (myState == States.spindleMovement) {
				states_spindleMovement ();
			} else if (myState == States.usFlag) {
				states_usFlag ();
			} else if (myState == States.toolChanger) {
				states_toolChanger ();
			} else if (myState == States.controls) {
				states_controls ();
			}
		}
	}


	#region State Methods

	private void states_wait (){
    }

	private void states_frontLoading(){
		arrayInt = 0;
		stateLock = true;
		hasFrontLoadingPlayed = true;
		exploreSceneAudio.selectedClip = exploreSceneAudio.audioClip [arrayInt];
		captionsCanvas.FadeCaptionsPanelToggle (arrayInt + 2); // adding the "+2" is because the captions text array has all the VO captions in it so the front loading starts at array index 2.
		StartCoroutine (StateResetDelay ());
		if (hasFrontLoadingPlayed) {
			particleGlowTable.SetActive (true);
		}
	}

	private void states_cuttingTable(){
		arrayInt = 1;
		stateLock = true;
		exploreSceneAudio.selectedClip = exploreSceneAudio.audioClip [arrayInt];
		CuttingTableRotate ();
		particleGlowTable.SetActive (false);
		captionsCanvas.FadeCaptionsPanelToggle (arrayInt + 2);
		StartCoroutine (StateResetDelay ());
	}

	private void states_spindleMovement(){
		arrayInt = 2;
		stateLock = true;

		spindleCamStartPos = cameraController.transform.position;
		movieCanvasController.InitializeCanvas ();
		housingSide.SetActive (false);
		housingSideFade.SetActive (true);
		StartCoroutine (movieCanvasController.CanvasFadeIn ());
		StartCoroutine(HousingFadeTo (0.0f, sideHousingFadeDuration));
		SpindleSequenceMoveIn ();
		CreatePart ();
		captionsCanvas.FadeCaptionsPanelToggle (arrayInt + 2);
		StartCoroutine (SpindleAnimationDelay ());
    }

	private void states_usFlag(){
		arrayInt = 5;
		stateLock = true;
		captionsCanvas.FadeCaptionsPanelToggle (arrayInt + 2);
		StartCoroutine (StateResetDelay ());
	}

	private void states_controls(){
		arrayInt = 4;
		stateLock = true;

        operatorCamStartPos = cameraController.transform.position;
		OperatorMoveIn ();
		movieCanvasController.InitializeCanvas ();
		StartCoroutine (movieCanvasController.CanvasFadeIn ());
		OperatorDoorTrigger ();
		sideDoorOpened = true;

		CameraLock (operatorCamStartRotation);
		captionsCanvas.FadeCaptionsPanelToggle (arrayInt + 2);
		StartCoroutine (OperatorAnimationDelay ());
	}

	private void states_toolChanger(){
		arrayInt = 6;
		stateLock = true;
		captionsCanvas.FadeCaptionsPanelToggle (arrayInt + 2);
		StartCoroutine (ToolChangerResetDelay ());
	}

	#endregion

	#region Explore Controller Global Methods

	private void RecenterScene(float targetY, GameObject targetObject, float angleAdjustment){
		scene.transform.rotation = Quaternion.Euler (scene.transform.rotation.x, scene.transform.rotation.y + angleAdjustment, scene.transform.rotation.z);
		Vector3 scenePosition = scene.transform.position;
		scenePosition.x = scene.transform.position.x - 5.5f;
		scenePosition.z = -4.25f;
		scene.transform.position = scenePosition;

		Vector3 newPosition = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y + 2.25f, targetObject.transform.position.z);
		cameraController.transform.position = newPosition;
	}

	private void CameraLock (Vector3 targetRotation){
        startRotation = targetRotation;
		cameraController.transform.eulerAngles = targetRotation;
	}

    private IEnumerator StateResetDelay (){
		float audioLength = exploreSceneAudio.audioClip [arrayInt].length;
		yield return new WaitForSeconds (audioLength + stateResetDelay);
		captionsCanvas.FadeCaptionsPanelToggle (arrayInt + 2);
		yield return new WaitForSeconds (2);
		stateLock = false;
		myState = States.wait;
		if (hasFrontLoadingPlayed) {
			particleGlowTable.SetActive (true);
		}
    }

	#endregion

	#region Tool Changer Methods
	private IEnumerator ToolChangerResetDelay(){
		yield return new WaitForSeconds (stateResetDelay + 3.5f);
		captionsCanvas.FadeCaptionsPanelToggle (arrayInt + 2);
		yield return new WaitForSeconds (2);
		myState = States.wait;
		stateLock = false;
	}
	#endregion

	#region Cutting Table Animation Methods

	private IEnumerator TableColliderReset(){
		float audioLength = exploreSceneAudio.audioClip [arrayInt].length;
		Collider tableCollider = cuttingTableGObject.GetComponent<BoxCollider> ();
		tableCollider.enabled = false;
		yield return new WaitForSeconds (audioLength + cuttingTableResetDelay);
		tableCollider.enabled = true;
	}

	private void CuttingTableRotate(){
		iTween.RotateTo (cuttingTableGObject, iTween.Hash ("y", 360f + 360f, "easeType", "easeInOutQuad", "time", cuttingTableRotationsTime));
	}

	#endregion

	#region Spindle Animation Methods

	private IEnumerator SpindleAnimationDelay(){
		yield return new WaitForSeconds (2.25f);
		spindleTool.GetComponent<Animator> ().enabled = true;
        exploreSceneAudio.selectedClip = exploreSceneAudio.audioClip[2];
	}

	void StartNextAudio (){
		StartCoroutine(VODelay ());
	}

	public IEnumerator VODelay(){
		yield return new WaitForSeconds (1.0f);
		spindleTool.GetComponent<Animator> ().Play("SpindleExtension");
		exploreSceneAudio.selectedClip = exploreSceneAudio.audioClip[3];
		captionsCanvas.FadeCaptionsPanelToggle (arrayInt + 2);
		float runtime = exploreSceneAudio.audioClip[3].length;
		yield return new WaitForSeconds (runtime + 0.5f);
		captionsCanvas.FadeCaptionsPanelToggle (arrayInt + 3);
		StartCoroutine (SpindleStateResetDelay (3.0f));
	}

	private IEnumerator SpindleStateResetDelay(float time){
		yield return new WaitForSeconds (time);
		SpindleSequenceMoveOut ();
		StartCoroutine (movieCanvasController.CanvasFadeOut ());
		StartCoroutine(HousingFadeTo (1.0f, sideHousingFadeDuration));
		newTablePart.SetActive (false);
		housingSide.SetActive (true);
		housingSideFade.SetActive (false);
        spindleTool.GetComponent<Animator> ().enabled = false;
		stateLock = false;
		spindleExtensionTriggers.audioHasPlayed = false;
		myState = States.wait;
		captionsCanvas.FadeCaptionsPanelToggle (arrayInt + 3);
    }

	private IEnumerator HousingFadeTo (float aValue, float aTime){
		float alpha = housingSideFade.GetComponent<Renderer>().material.color.a;
		float glassAlpha = housingSideFadeGlass.GetComponent<Renderer> ().material.color.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime) {
			Color newColor = new Color (1,1,1, Mathf.Lerp(alpha, aValue, t));
			Color newGlassColor = new Color (1,1,1, Mathf.Lerp(glassAlpha, aValue, t));
			housingSideFade.GetComponent<Renderer>().material.color = newColor;
			housingSideFadeGlass.GetComponent<Renderer>().material.color = newGlassColor;
			yield return null;
		}
	}

	private void CreatePart(){
		newTablePart.SetActive (true);
	}

	private void SpindleSequenceMoveIn (){
		Vector3 position = spindleSequenceCamPos.position;
		iTween.MoveTo (cameraController, iTween.Hash("position", position, "easeType", "easeInOutQuad", "time", moveSpeed));
	}

	private void SpindleSequenceMoveOut (){
		iTween.MoveTo (cameraController, iTween.Hash("position", spindleCamStartPos, "easeType", "easeInOutQuad", "time", moveSpeed));
	}

	#endregion

	#region Controls State Animation Methods


	private void OperatorDoorTrigger(){
		Vector3 doorStartPos = new Vector3 (-0.1f, 0.1675f, 0.135f);
		if (!sideDoorOpened) {
			iTween.MoveTo (operatorDoor, iTween.Hash ("islocal", true, "x", -0.195f, "easeType", "easeInOutQuad", "time", doorOpenTime));
		} else if (sideDoorOpened) {
			iTween.MoveTo (operatorDoor, iTween.Hash ("islocal", true, "position", doorStartPos, "easeType", "easeInOutQuad", "time", doorOpenTime));
			sideDoorOpened = false;
		}
	}

	private void OperatorMoveIn (){
		Vector3 position = operatorSequenceCamPos.position;
		iTween.MoveTo (cameraController, iTween.Hash("position", position, "easeType", "easeInOutQuad", "time", moveSpeed));
	}

	private void OperatorMoveOut (){
		iTween.MoveTo (cameraController, iTween.Hash("position", operatorCamStartPos, "easeType", "easeInOutQuad", "time", moveSpeed));
	}

	private IEnumerator OperatorAnimationDelay(){
		yield return new WaitForSeconds (2.5f);
		exploreSceneAudio.selectedClip = exploreSceneAudio.audioClip[4];
		float runtime = exploreSceneAudio.audioClip [4].length;
		yield return new WaitForSeconds (runtime);
		OperatorFinished ();
	}

	private void OperatorFinished(){
		OperatorMoveOut ();
		StartCoroutine (movieCanvasController.CanvasFadeOut ());
		movieCanvasController.DeactivateCanvas ();
		OperatorDoorTrigger ();
		sideDoorOpened = false;
		CameraOperatorUnlock ();
		CameraLock(operatorCamStartRotation);
		stateLock = false;
		captionsCanvas.FadeCaptionsPanelToggle (arrayInt + 2);
		myState = States.wait;
	}

	private void CameraOperatorUnlock(){
		StartCoroutine (CameraOperatorUnlockTimer ());
	}

	private IEnumerator CameraOperatorUnlockTimer(){
		yield return new WaitForSeconds (3.0f);

        //Turning this method call off as I will need custom rotation numbers for every hotspot (viewing angle)
        //you could click on the operator hotspot from. THEN, I would need to do the same thing with all new numbers for
        //the other cutscene - spindle travel scene.
        //		RecenterScene (0, operatorHotspot, operatorSceneAngleCorrection);
    }

	#endregion
}
