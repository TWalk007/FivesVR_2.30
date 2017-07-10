using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportTransition : MonoBehaviour {

	public Canvas teleportTransitionCanvas;
	public Image image;
	public float fadeDuration = 1f;
	public bool shouldIgnoreTimeScale;

	private float initialAlpha;
	private float targetAlpha;
	private float currentAlpha;
	private float startTime = 0.0f;
	private bool isTeleporting = false;
	private float timeToDestroyCanvas;

	void Start () {

		initialAlpha = image.color.a;
		currentAlpha = initialAlpha;

		timeToDestroyCanvas = fadeDuration + 0.01f;

		if (image == null) {
			Debug.LogWarning ("Please set the canvas image to be faded.  Image not found.");
		}
	}

	void Update (){
		
		if (isTeleporting) {
//			print ("teleporting true!");

			CanvasEnable ();
			startTime += Time.deltaTime;

			float timeRatio = startTime/fadeDuration;
			float newAlpha = Mathf.Lerp (currentAlpha, targetAlpha, timeRatio);
			Color newColor = new Color (1f, 1f, 1f, newAlpha);

			image.color = newColor;
			AlphaToggle ();
			StartCoroutine (DestroyCanvas(timeToDestroyCanvas));

			if (startTime >= fadeDuration) {
				isTeleporting = false;
			}
		}

		TeleportingTrue ();
	}


	private IEnumerator DestroyCanvas (float seconds){
//		print ("Destroying canvas in " + seconds + "seconds.");
		yield return new WaitForSeconds (seconds);
		Destroy (GameObject.Find ("Teleport Transition Canvas(Clone)"));
	}

	private void AlphaToggle (){
		if (currentAlpha == 0f) {
			targetAlpha = 1f;
		} else if (currentAlpha == 1f) {
			targetAlpha = 0f;
		}
	}

	public void TeleportingTrue(){		
		isTeleporting = true;
	}

	public void TeleportingFalse(){		
		isTeleporting = false;
	}

	public void CanvasDisable() {
		teleportTransitionCanvas.enabled = false;
		print ("Canvas disabled.");
	}

	public void CanvasEnable() {
		teleportTransitionCanvas.enabled = true;
//		print ("Canvas enabled.");
	}
}
